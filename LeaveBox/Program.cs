using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace LeaveBox;

internal static partial class Program
{
    private static bool s_isDebug = true;

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
            ShowException(e, "配置格式出错啦");

            Environment.Exit(-1);
        }

        var json = default(string);

        try
        {
            var config = Config(document);

            s_isDebug = true;

            var serializer = new XmlSerializer(config.GetType());

            var stream = new MemoryStream();

            serializer.Serialize(stream, config);

            json = Encoding.UTF8.GetString(stream.ToArray());
        }
        catch (Exception e)
        {
            ShowException(e, "配置内容出错啦");

            Environment.Exit(-2);
        }

        s_config = json.Replace("\"", "\"\"");

        MessageBox.Show(s_config);

        Build();
    }
    
    private static void ShowException(Exception exception, string title)
    {
        var text = s_isDebug ? exception.ToString() : exception.Message;

        MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}