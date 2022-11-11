using System.Xml;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    public class CActionPrepUpgradeVal : CActionBase
    {
        public CActionPrepUpgradeVal(ref CActionParameters actionParameters, XmlDocument doc)
            : base(ref actionParameters, doc)
        {
        }

        public override bool DoIt()
        {
            if (ActionParameters.location != null)
            {
                FindLocNodes(true);
            }
            if (LocNodes.Count > 0)
            {
                ActionParameters.val = ((XmlNode)LocNodes[0]).InnerText;
            }
            else
            {
                ActionParameters.val = null;
            }
            return true;
        }

        public override void LogStart()
        {
            string line = ".Starting action PREPUPGRADE";
            Messages.Add(line);

        }
    }
}
