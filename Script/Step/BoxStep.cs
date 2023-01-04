using System;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Script;

/// <summary>
/// 消息盒步骤，包含：
/// <list>
///     <item>1个Label</item>
///     <item>1~2个Button</item>
/// </list>
/// </summary>
public class BoxStep : Step
{
    [XmlAttribute]
    public string Content { get; set; }

    [XmlArray]
    public BoxButton[] Buttons { get; set; }

    public new static BoxStep Parse(XmlNode node)
    {
        string content = node.Attributes!["content"]?.Value;

        if (content == default)
        {
            throw new Exception("若Step标签的type=\"box\"，则必须拥有content属性");
        }

        var step = new BoxStep
        {
            Content = content
        };

        var buttonList = (from XmlNode childNode in node.ChildNodes
                where childNode.Name == "Button"
                select BoxButton.Parse(childNode))
            .ToList();

        if (buttonList.Count < 1)
        {
            throw new Exception("若Step标签的type=\"box\"，则必须拥有至少1个Button子标签");
        }

        step.Buttons = buttonList.ToArray();

        return step;
    }
}