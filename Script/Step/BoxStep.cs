using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace Script;

public class BoxStep : Step
{
    [XmlAttribute]
    public string Content { get; set; }

    [XmlArray]
    public BoxButton[] Buttons { get; set; }

    public new static BoxStep Parse(XmlNode node)
    {
        var content = node.Attributes!["content"]?.Value;

        if (content == default)
        {
            throw new Exception("若Step标签的type=\"box\"，则必须拥有content属性");
        }

        var step = new BoxStep
        {
            Content = content
        };
        
        var buttonList = new List<BoxButton>();

        foreach (XmlNode childNode in node.ChildNodes)
        {
            if (childNode.Name != "Button")
                continue;

            buttonList.Add(BoxButton.Parse(childNode));
        }

        if (buttonList.Count < 1)
        {
            throw new Exception("若Step标签的type=\"box\"，则必须拥有至少1个Button子标签");
        }

        step.Buttons = buttonList.ToArray();

        return step;
    }
}