using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Script.Nodes;

namespace Script;

public partial class ScriptPlayer
{
    private Dictionary<string, Step> m_dict;

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
        string next = step switch
        {
            BoxStep boxStep => PlayBoxStep(boxStep),
            InputStep inputStep => PlayInputStep(inputStep),
            _ => step.Next
        };

        if (next != null && m_dict.ContainsKey(next))
        {
            PlayStep(m_dict[next]);
        }
    }
}