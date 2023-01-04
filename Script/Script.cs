using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Script;

public class Script
{
    private readonly BoxForm1 m_boxForm1;

    private readonly BoxForm2 m_boxForm2;

    private Dictionary<string, Step> m_dict;

    public Script()
    {
        m_boxForm1 = new BoxForm1();

        m_boxForm2 = new BoxForm2();
    }

    public void Run(string json)
    {
        var serializer = new XmlSerializer(typeof(Config));

        var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));

        var config = (Config)serializer.Deserialize(stream);

        m_dict = config.Steps.ToDictionary(s => s.Id);

        PlayStep(m_dict[config.StartId]);
    }

    private void PlayStep(Step step)
    {
        if (step is BoxStep boxStep)
        {
            if (boxStep.Buttons.Length >= 2)
            {
                var b1 = boxStep.Buttons[0];

                var b2 = boxStep.Buttons[1];

                var next = m_boxForm2.Show(boxStep.Content, b1.Text, b1.Next, b2.Text, b2.Next) ?? boxStep.Next;

                if (next != null && m_dict.ContainsKey(next))
                {
                    PlayStep(m_dict[next]);
                }
            }
            else if (boxStep.Buttons.Length >= 1)
            {
                var b = boxStep.Buttons[0];

                m_boxForm1.Show(boxStep.Content, b.Text);

                var next = b.Next ?? boxStep.Next;

                if (next != null && m_dict.ContainsKey(next))
                {
                    PlayStep(m_dict[next]);
                }
            }
        }
    }
}