using System.Xml.Serialization;

namespace Script.Nodes;

[XmlInclude(typeof(BoxStep))]
[XmlInclude(typeof(InputStep))]
public class Config
{
    [XmlAttribute]
    public string StartId { get; set; }

    [XmlArray]
    public Step[] Steps { get; set; }
}