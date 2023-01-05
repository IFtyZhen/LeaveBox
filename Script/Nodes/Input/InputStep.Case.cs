using System;
using System.Xml;
using System.Xml.Serialization;

namespace Script.Nodes;

public partial class InputStep
{
    [XmlType(Namespace = "InputStep")]
    public class Case
    {
        [XmlAttribute]
        public string Value { get; set; }

        [XmlAttribute]
        public string Next { get; set; }

        public static Case Parse(XmlNode node)
        {
            string value = node.Attributes!["value"]?.Value;

            string next = node.Attributes["next"]?.Value;

            if (value == default)
            {
                throw new Exception("input.Case: 每个Case标签必须要有text属性");
            }

            if (next == default)
            {
                throw new Exception("input.Case: 每个Case标签必须要有next属性");
            }

            return new Case
            {
                Value = value,

                Next = next
            };
        }
    }
}