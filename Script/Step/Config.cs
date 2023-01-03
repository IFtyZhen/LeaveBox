using System.Xml.Serialization;

namespace Script;

[XmlInclude(typeof(BoxStep))]
public class Config
{
    public string StartId { get; set; }

    public Step[] Steps { get; set; }
}