using System;
using System.Xml;
using System.Xml.Serialization;

namespace Script.Nodes;

public partial class BoxStep
{
    [XmlType(Namespace = "BoxStep")]
    public class Button
    {
        [XmlAttribute]
        public string Text { get; set; }

        [XmlAttribute]
        public string Next { get; set; }

        public static Button Parse(XmlNode node)
        {
            string text = node.Attributes!["text"]?.Value;

            string next = node.Attributes["next"]?.Value;

            if (text == default)
            {
                throw new Exception("box.Button: 每个Button标签必须要有text属性");
            }

            return new Button
            {
                Text = text,

                Next = next
            };
        }
    }
}