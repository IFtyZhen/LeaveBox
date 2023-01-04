using System;
using System.Xml;
using System.Xml.Serialization;

namespace Script;

public class BoxButton
{
    [XmlAttribute]
    public string Text { get; set; }

    [XmlAttribute]
    public string Next { get; set; }

    public static BoxButton Parse(XmlNode node)
    {
        var text = node.Attributes!["text"]?.Value;

        var next = node.Attributes["next"]?.Value;

        if (text == default)
        {
            throw new Exception("每个Button标签必须要有text属性");
        }

        return new BoxButton
        {
            Text = text,

            Next = next
        };
    }
}