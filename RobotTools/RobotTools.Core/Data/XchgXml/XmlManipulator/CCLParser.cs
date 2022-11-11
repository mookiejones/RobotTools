using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace RobotTools.Core.Data.XchgXml.XmlManipulator
{
    public class CCLParser : StringDictionary
    {
        private Regex splitArgsEx = new Regex("/[a-z]+[\\s=]|/[a-z]+$", RegexOptions.Compiled);

        private Regex getNameAndValueEx = new Regex("(^([/-])(?<name>[^=:]+)([=:](?<value>.+))?$)|(@?(?<name>.*))", RegexOptions.Compiled);

        public CCLParser(string argLine)
        {
            MatchCollection matchCollection = splitArgsEx.Matches(argLine);
            if (matchCollection.Count > 0)
            {
                Add("filename", TrimParameter(argLine.Substring(0, matchCollection[0].Index)));
                string[] array = new string[matchCollection.Count];
                for (int i = 0; i < matchCollection.Count - 1; i++)
                {
                    array[i] = argLine.Substring(matchCollection[i].Index, matchCollection[i + 1].Index - matchCollection[i].Index).Trim();
                }
                array[matchCollection.Count - 1] = argLine.Substring(matchCollection[matchCollection.Count - 1].Index).Trim();
                for (int j = 0; j < array.Length; j++)
                {
                    Match match = getNameAndValueEx.Match(array[j]);
                    if (match.Success)
                    {
                        string value = TrimParameter(match.Groups["value"].Value);
                        string key = TrimParameter(match.Groups["name"].Value);
                        Add(key, value);
                        continue;
                    }
                    throw new ArgumentException();
                }
            }
            else
            {
                string text = argLine.Substring(0, matchCollection[0].Index).Trim();
                if (text.Length == 0)
                {
                    throw new ArgumentException();
                }
                Add("filename", text);
            }
        }

        private string TrimParameter(string par)
        {
            return par.Trim(' ', '"', '\'');
        }
    }
}
