using System.Text;
using System.Xml;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    public class CActionChangeVal : CActionBase
    {
        public CActionChangeVal(ref CActionParameters actionParameters, XmlDocument Doc)
            : base(ref actionParameters, Doc)
        {
        }

        public override bool DoIt()
        {
            if (ActionParameters.location != null)
            {
                FindLocNodes(true);
            }
            if (MatchingNodes.Count > 0 && ActionParameters.val != null)
            {
                foreach (XmlNode locNode in LocNodes)
                {
                    locNode.InnerText = ActionParameters.val;
                }
            }
            return true;
        }

        public override void LogStart()
        {
            
            Messages.Add(".Starting action CHANGEVAL");
            Messages.Add(" path:" + ActionParameters.path);
            Messages.Add(" identifier:" + ActionParameters.identifier);
            Messages.Add(" location:" + ActionParameters.location);
            Messages.Add(" val:" + ActionParameters.val);
            

        }
    }
}
