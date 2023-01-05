using System;
using System.Linq;
using System.Runtime.Remoting.Metadata;
using System.Xml;
using System.Xml.Serialization;

namespace Script.Nodes;

public partial class InputStep
{
    [XmlType(Namespace = "InputStep")]
    public class Button
    {
        [XmlAttribute]
        public string Text { get; set; }

        [XmlAttribute]
        public string Next { get; set; }
        
        [XmlArray]
        public Case[] Cases { get; set; }

        public static Button Parse(XmlNode node)
        {
            string text = node.Attributes!["text"]?.Value;

            string next = node.Attributes["next"]?.Value;

            if (text == default)
            {
                throw new Exception("input.Button: 每个Button标签必须要有text属性");
            }

            var button =  new Button
            {
                Text = text,

                Next = next,
                
                Cases = (from XmlNode childNode in node.ChildNodes
                    where childNode.Name == "Case"
                    select Case.Parse(childNode)).ToArray()
            };

            return button;
        }
    }
}