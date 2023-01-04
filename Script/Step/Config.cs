using System.Xml.Serialization;

namespace Script;

[XmlInclude(typeof(BoxStep))]
public class Config
{
    [XmlAttribute]
    public string StartId { get; set; }

    [XmlArray]
    public Step[] Steps { get; set; }
}