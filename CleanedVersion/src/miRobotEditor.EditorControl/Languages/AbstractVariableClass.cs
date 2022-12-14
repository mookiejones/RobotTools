using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace miRobotEditor.EditorControl.Languages
{
    internal abstract class AbstractVariableClass 
    {
        #region IVariableType Members

        public abstract string Type { get; }

        internal  abstract  string Expression { get; }

        public abstract string Name { get; set; }

        public string Raw { get; set; }

        public string Scope { get; set; }

        public ToolTip ToolTip { get; set; }


        protected static GroupCollection GetMatchCollection(string text, string matchstring)
        {
            var r = new Regex(matchstring, RegexOptions.IgnoreCase);
            var m = r.Match(text);
            return m.Success ? m.Groups : null;
        }


        public  List<object> Items { get; set; } 

            /// <summary>
            /// Add _variables To List
            /// </summary>
            /// <param name="text"></param>
            /// <param name="vartype"></param>
        public void Add(string text, AbstractVariableClass vartype)
            {
                var r = new Regex(Expression, RegexOptions.IgnoreCase);
                var m = r.Match(text);
                while (m.Success)
                {
                    Raw = m.ToString();
                    Items.Add(vartype);
                    m = m.NextMatch();

                }

            }

        
        internal abstract void GetVariable(GroupCollection m);

        public string Key(string line)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IVariableType Members



        #endregion
    }
}
