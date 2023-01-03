using System;
using System.Xml;
using System.Xml.Serialization;

namespace Script;

public class Step
{
    [XmlAttribute]
    public string Id { get; set; }

    [XmlAttribute]
    public string Next { get; set; }

    public static Step Parse(XmlNode node)
    {
        var id = node.Attributes!["id"]?.Value;

        var type = node.Attributes["type"]?.Value;

        var next = node.Attributes["next"]?.Value;

        if (id == default)
        {
            throw new Exception("每个Step标签必须要有id属性");
        }

        if (type == default)
        {
            throw new Exception("每个Step标签必须要有type属性");
        }

        Step step = type switch
        {
            "box" => BoxStep.Parse(node),

            _ => throw new Exception($"Step标签的type属性不能为{type}")
        };

        step.Id = id;

        step.Next = next;

        return step;
    }
}