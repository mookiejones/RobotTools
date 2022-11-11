using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotTools.UI.Editor.Snippets
{
    public class Declaration
    {
        private static Dictionary<string, Declaration> _defaults;
        public static Dictionary<string, Declaration> Defaults
        {
            get
            {
                if (_defaults == null)
                {
                    _defaults = new Dictionary<string, Declaration>
                    {
                        {
                            "$end$", new Declaration
                            {
                                Id = "$end$"
                            }
                        },
                        {
                            "$selection$", new Declaration
                            {
                                Id = "$selection$"
                            }
                        }
                    };
                }
                return _defaults;
            }
        }
        public string Default
        {
            get;
            set;
        }
        public string Id
        {
            get;
            set;
        }
        public object ToolTip
        {
            get;
            set;
        }
    }

    
}
