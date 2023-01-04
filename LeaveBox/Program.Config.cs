using System;
using System.Linq;
using System.Xml;
using Script;

namespace LeaveBox;

internal partial class Program
{
    private static string s_config;

    private static Config Config(XmlDocument document)
    {
        s_isDebug = true;

        var scriptNode = document.SelectSingleNode("Script");

        if (scriptNode == default)
        {
            throw new Exception("配置节点中没有找到Script标签");
        }

        s_isDebug = scriptNode.Attributes!["debug"]?.Value == "true";

        var startId = scriptNode.Attributes["start"]?.Value;

        if (startId == null)
        {
            throw new Exception("Script标签中没有找到start属性");
        }


        var config = new Config();

        var steps = document.SelectNodes("Script/Step");

        config.StartId = startId;

        config.Steps = (from XmlNode node in steps select Step.Parse(node)).ToArray();

        if (config.Steps.FirstOrDefault(step => step.Id == config.StartId) == default)
        {
            throw new Exception("没有找到id为" + startId + "的Step");
        }

        return config;
    }
}