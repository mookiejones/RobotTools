using System.Xml;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    public class CActionRemoveNode : CActionBase
    {
        public CActionRemoveNode(ref CActionParameters actionParameters, XmlDocument doc)
            : base(ref actionParameters, doc)
        {
        }

        public override bool DoIt()
        {
            if (ActionParameters.location != null)
            {
                FindLocNodes(false);
            }
            if (LocNodes.Count > 0)
            {
                RemoveLocNodes();
            }
            else if (ActionParameters.location == null)
            {
                RemoveMatchingNodes();
            }
            return true;
        }

        public override void LogStart()
        {
            string text = ".Starting action REMOVENODE";
            text = text + " path:" + ActionParameters.path;
            if (ActionParameters.identifier != null)
            {
                text = text + " identifier:" + ActionParameters.identifier;
            }
            if (ActionParameters.location != null)
            {
                text = text + " location:" + ActionParameters.location;
            }
            Messages.Add(text);
        }
    }
}
