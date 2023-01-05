namespace LeaveBox;

internal partial class Program
{
    private static string Source => $@"
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
            var resourceManager = new ResourceManager(""res"", typeof(Program).Assembly);
        
            var bytes = (byte[])resourceManager.GetObject(""Script"");

            var script = Assembly.Load(bytes);
        
            var obj = script.CreateInstance(""Script.ScriptPlayer"");

            var run = obj.GetType().GetMethod(""Run"");

            run.Invoke(obj, new object[]{{ @""{s_config}"" }});
        }}
        catch (Exception e)
        {{
            MessageBox.Show(e.ToString());
        }}    
    }}     
}}
";
}