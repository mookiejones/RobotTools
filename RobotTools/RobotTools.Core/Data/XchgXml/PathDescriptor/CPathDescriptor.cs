using System;
using System.Collections.Specialized;
namespace RobotTools.Core.Data.XchgXml.PathDescriptor
{
    public class CPathDescriptor
    {
        public struct PathElement
        {
            public string nodeName;

            public StringDictionary attributes;
        }

        public int Entries;

        public string PathName = "";

        public PathElement[] PathElements;

        public bool HasAttributes;

        public CPathDescriptor(string inputPath)
        {
            string text = inputPath;
            if (inputPath.Length == 0)
            {
                return;
            }
            text = text.Trim(null);
            if (text.StartsWith("//"))
            {
                text = text.TrimStart('/');
                PathName = "//";
            }
            string[] array = text.Split('/');
            Entries = array.Length;
            PathElements = new PathElement[Entries];
            for (int i = 0; i < Entries; i++)
            {
                string[] array2 = array[i].Split(' ');
                PathElements[i].nodeName = array2[0];
                PathName += array2[0];
                if (i != Entries - 1)
                {
                    PathName += "/";
                }
                if (array2.Length <= 0)
                {
                    continue;
                }
                PathElements[i].attributes = new StringDictionary();
                for (int j = 1; j < array2.Length; j++)
                {
                    if (!HasAttributes)
                    {
                        HasAttributes = true;
                    }
                    string[] array3 = array2[j].Split(new char[1] { '=' }, 2);
                    if (array3.Length > 1)
                    {
                        PathElements[i].attributes.Add(array3[0], array3[1]);
                    }
                    else
                    {
                        Console.WriteLine("Illegal attribute syntax: {0}", array2[j]);
                    }
                }
            }
        }
    }
}
