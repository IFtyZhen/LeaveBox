using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace Script.Nodes;

/// <summary>
/// 输入框步骤，包含：
/// <list>
///     <item>1个Label</item>
///     <item>1~2个Button</item>
/// </list>
/// </summary>
public partial class InputStep : Step
{
    [XmlAttribute]
    public string Content { get; set; }
    
    [XmlAttribute]
    public string Value { get; set; }

    [XmlArray]
    public Button[] Buttons { get; set; }
    
    public new static InputStep Parse(XmlNode node)
    {
        string content = node.Attributes!["content"]?.Value;
        
        string value = node.Attributes["value"]?.Value;

        if (content == default)
        {
            throw new Exception("若Step标签的type=\"input\"，则必须拥有content属性");
        }

        var step = new InputStep
        {
            Content = content,
            Value = value
        };

        List<Button> buttonList = (from XmlNode childNode in node.ChildNodes
                where childNode.Name == "Button"
                select Button.Parse(childNode)).ToList();

        switch (buttonList.Count)
        {
            case < 1:
                throw new Exception("若Step标签的type=\"input\"，则至少要有1个Button子标签");
            case > 2:
                throw new Exception("若Step标签的type=\"input\"，则最多拥有2个Button子标签");
            default:
                step.Buttons = buttonList.ToArray();

                return step;
        }
    }
}