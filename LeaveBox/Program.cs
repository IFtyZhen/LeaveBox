using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Script.Nodes;

namespace LeaveBox;

internal static partial class Program
{
    private static bool s_isDebug = true;

    public static void Main()
    {
        string[] s_ConfigNames = { "config.txt", "config.xml", "config" };

        string input = s_ConfigNames.FirstOrDefault(File.Exists);

        if (input == default)
        {
            MessageBox.Show("没有找到配置文件");

            Environment.Exit(-1);
        }

        var document = new XmlDocument();

        try
        {
            document.Load(input);
        }
        catch (Exception e)
        {
            ShowException(e, "配置格式出错啦");

            Environment.Exit(-2);
        }

        try
        {
            Config config = Config(document);

            s_isDebug = true;

            var serializer = new XmlSerializer(config.GetType());

            var stream = new MemoryStream();

            serializer.Serialize(stream, config);

            s_config = Encoding.UTF8.GetString(stream.ToArray());
        }
        catch (Exception e)
        {
            ShowException(e, "配置内容出错啦");

            Environment.Exit(-3);
        }

        s_config = s_config.Replace("\"", "\"\"");

        Assembly assembly = Build();

#if DEBUG
        MethodInfo main = assembly.EntryPoint;

        main.Invoke(null, new object[] { });
#endif
    }

    private static void ShowException(Exception exception, string title)
    {
        string text = s_isDebug ? exception.ToString() : exception.Message;

        MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}