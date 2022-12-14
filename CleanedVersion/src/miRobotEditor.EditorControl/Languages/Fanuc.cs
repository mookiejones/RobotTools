using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using miRobotEditor.EditorControl.Classes;
using miRobotEditor.EditorControl.Interfaces;

namespace miRobotEditor.EditorControl.Languages
{
    [Localizable(false)]
    public class Fanuc : AbstractLanguageClass
    {
    	public Fanuc(string file):base(file)
        {           
            FoldingStrategy = new RegionFoldingStrategy();
        }

        public Fanuc()
        {
            FoldingStrategy = new RegionFoldingStrategy();
        }

        /// <summary>
        /// Sets ComboBox Filter Items for searching
        /// </summary>
        /// <returns></returns>
        public override List<string> SearchFilters
        {
            get
            {
                return EXT;
            }
        }
        internal override bool IsFileValid(System.IO.FileInfo file)
        {
            return EXT.Any(e => file.Extension.ToLower() == e);
        }
        public static List<string> EXT{get{return new List<string> {".ls"};}}
        internal override string FoldTitle(FoldingSection section, TextDocument doc)
        {
            var s = Regex.Split(section.Title, "æ");

            var start = section.StartOffset + s[0].Length;
            var end = section.Length - (s[0].Length + s[1].Length);


            return doc.GetText(start, end);
        }
        internal override Typlanguage RobotType { get { return Typlanguage.Fanuc; } }

        internal override IList<ICompletionData> CodeCompletion
        {
            get
            {
                var codeCompletionList = new List<ICompletionData> {new CodeCompletion("Item1")};
                return codeCompletionList;
            }
        }

        protected override string ShiftRegex
        {
            get {throw new NotImplementedException(); }
        }

        internal override string SourceFile
        {
            get { throw new NotImplementedException(); }
        }



        internal override string FunctionItems
        {
            get { return   "(\\.Program [\\d\\w]*[\\(\\)\\w\\d_.]*)" ; }
        }

        internal override sealed AbstractFoldingStrategy FoldingStrategy { get; set; }

        public sealed class RegionFoldingStrategy : AbstractFoldingStrategy
        {
           

            /// <summary>
            /// Create <see cref="NewFolding"/>s for the specified document.
            /// </summary>
            public override IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
            {
                firstErrorOffset = -1;
                return CreateNewFoldings(document);
            }

            


            /// <summary>
            /// Create <see cref="NewFolding"/>s for the specified document.
            /// </summary>
            public IEnumerable<NewFolding> CreateNewFoldings(ITextSource document)
            {
                var newFoldings = new List<NewFolding>();

                newFoldings.AddRange(CreateFoldingHelper(document, ";fold", ";endfold", true));
  //              newFoldings.AddRange(LanguageBase.CreateFoldingHelper(document, "def", "end", false));

                newFoldings.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
                return newFoldings;
            }
        }

    	
        
        
         /// <summary>
        /// Strips Comment Character from string.
        /// <remarks> Used with the Comment/Uncomment Command</remarks>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public override string CommentReplaceString(string text)
        {
        	// Create Result Regex
        	Regex rgx=null;
        	// Is it a line Comment
        	const string linereg = @"^[\s]*\d*:\s*!";
        	const string blankReg = @"^[\s]*!";
        	if (Regex.IsMatch(text,linereg))
        		rgx = new Regex(@"^([\s\d]*:\s*)!([^\r\n]*)");
        		
        	if (Regex.IsMatch(text,blankReg))
        		rgx = new Regex(@"^([\s]*)!([^\r\n]*)");
        		
        	if (rgx!=null)
        	{
        		var m = rgx.Match(text);
        			if (m.Success)
        				return m.Groups[1]+ m.Groups[2].ToString();
        	}			
				return text;			
        }
    	
        public override int CommentOffset(string  text)
        {
        	// Create Result Regex
        	var rgx=new Regex(@"(^[\s\d:]+)");

            {
                var m = rgx.Match(text);
                if (m.Success)
                    return m.Groups[1].Length;
                //return m.Groups[1].ToString()+ m.Groups[2].ToString();
            }
            return 0;		
        }

        /// <summary>
        /// Trims Line and Then Returns if first Character is a comment Character
        /// </summary>
        /// <returns></returns>
        public override bool IsLineCommented(string text)
        {       
        		var linereg =  new Regex(@"^[\s]*\d*:\s*!");
        		var blankReg=new Regex(@"^[\s]*!");
			return ((linereg.IsMatch(text))||(blankReg.IsMatch(text)));
        }
        
        public override Regex MethodRegex {get{return new Regex( String.Empty);}}
    	
        public override Regex StructRegex {get{return new Regex( String.Empty);}}
    	
        public override Regex FieldRegex {get{return new Regex( String.Empty);}}
    	
        public override Regex EnumRegex {get{return new Regex( String.Empty);}}

        [Localizable(false)]
        public override string CommentChar {get { return "!";}}

        public override Regex SignalRegex { get { return new Regex(String.Empty); } }
        public override string ExtractXYZ(string positionstring)
        {
            Debugger.Break();
            var p = new PositionBase(positionstring);
            return p.ExtractFromMatch();

        }

        public override Regex XYZRegex { get { return new Regex(String.Empty); } }
        public override DocumentModel GetFile(string filepath)
        {
            return new DocumentModel(filepath);
        }
    }
}
