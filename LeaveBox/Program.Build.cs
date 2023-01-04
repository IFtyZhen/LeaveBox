using System.CodeDom.Compiler;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp;

namespace LeaveBox;

internal partial class Program
{
    private static void Build()
    {
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

        var results = provider.CompileAssemblyFromSource(parameters, Source);

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