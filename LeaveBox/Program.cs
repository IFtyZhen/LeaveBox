using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.CSharp;
using Script;

namespace LeaveBox;

internal static class Program
{
    private static bool s_debug = true;

    private static void ThrowException(Exception exception, string title)
    {
        var text = s_debug ? exception.ToString() : exception.Message;
        
        MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        Environment.Exit(-1);
    }

    public static void Main()
    {
        var input = "./config.xml";
#if DEBUG
        input = @"../../input.xml";
#endif
            
        var document = new XmlDocument();

        try
        {
            document.Load(input);
        }
        catch (Exception e)
        {
            ThrowException(e, "配置格式出错啦");
        }

        var json = default(string);

        try
        {
            var config = ReadConfig(document);

            s_debug = true;

            var serializer = new XmlSerializer(config.GetType());
            
            var stream = new MemoryStream();
            
            serializer.Serialize(stream, config);

            json = Encoding.UTF8.GetString(stream.ToArray());
        }
        catch (Exception e)
        {
            ThrowException(e, "配置内容出错啦");
        }

        Build(json);
    }

    private static Config ReadConfig(XmlDocument document)
    {
        s_debug = true;

        var scriptNode = document.SelectSingleNode("Script");

        if (scriptNode == default)
        {
            throw new Exception("配置节点中没有找到Script标签");
        }

        s_debug = scriptNode.Attributes!["debug"]?.Value == "true";

        var startId = scriptNode.Attributes["start"]?.Value;
            
        if (startId == null)
        {
            throw new Exception("Script标签中没有找到start属性");
        }
            
            
        var config = new Config();

        var steps = document.SelectNodes("Script/Step");

        config.StartId = startId;
            
        config.Steps = (from XmlNode node in steps select Step.Parse(node)).ToArray();

        if (config.Steps.FirstOrDefault(step => step.Id == config.StartId) == default)
        {
            throw new Exception("没有找到id为" + startId + "的Step");
        }

        return config;
    }

    private static void Build(string json)
    {
        json = json.Replace("\"", "\"\"");
            
        var source = $@"
using System;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;
using Script;

internal static class Program
{{
    public static void Main()
    {{
        try
        {{
            var resourceManager = new ResourceManager(""Data/res"", typeof(Program).Assembly);
        
            var bytes = (byte[])resourceManager.GetObject(""Script"");

            var script = Assembly.Load(bytes);
        
            var obj = script.CreateInstance(""Script.Script"");

            var run = obj.GetType().GetMethod(""Run"");

            run.Invoke(obj, new object[]{{ @""{json}"" }});
        }}
        catch (Exception e)
        {{
            MessageBox.Show(e.ToString());
        }}    
    }}     
}}
";

        var parameters = new CompilerParameters
        {
            GenerateExecutable = true,
            OutputAssembly = "test.exe",
            CompilerOptions = "/target:winexe /optimize /win32icon:Data/logo.ico",
            EmbeddedResources = { "Data/res.resources" },
        };

        parameters.ReferencedAssemblies.Add("System.dll");
        parameters.ReferencedAssemblies.Add("System.Resources.ResourceManager.dll");
        parameters.ReferencedAssemblies.Add("System.Reflection.dll");
        parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
        parameters.ReferencedAssemblies.Add("Data/Script.dll");

        var provider = new CSharpCodeProvider();

        var results = provider.CompileAssemblyFromSource(parameters, source);

        if (results.Errors.Count > 0)
        {
            var errorMessage = new StringBuilder();
            foreach (CompilerError resultsError in results.Errors)
            {
                errorMessage.AppendLine(resultsError.ToString());
            }

            MessageBox.Show(errorMessage.ToString());
        }
        else
        {
            MessageBox.Show(@"编译成功啦~");
        }
    }
}