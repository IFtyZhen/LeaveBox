using System;
using System.Collections.Generic;
using System.Xml;

namespace Script;

public class BoxStep : Step
{
    public string Content { get; set; }

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

public class BoxButton
{
    public string Text { get; set; }

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