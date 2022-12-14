using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using miRobotEditor.EditorControl.Classes;
using miRobotEditor.EditorControl.Interfaces;

namespace miRobotEditor.EditorControl.Languages
{
    [Localizable(false)]
    public class VBA : AbstractLanguageClass
    {

        /// <summary>
        /// Sets ComboBox Filter Items for searching
        /// </summary>
        /// <returns></returns>


       
        public override List<string> SearchFilters
        {
            get { return new List<string> { "*.*", "*.dat", "*.src", "*.ini", "*.sub", "*.zip", "*.kfd" }; }
        }

        public VBA(string file):base(file)
        {           
            FoldingStrategy = new RegionFoldingStrategy();
        }

        internal override Typlanguage RobotType { get { return Typlanguage.VBA; } }

        internal override string SourceFile
        {
            get { return String.Empty; }
        }


        //Was XmlFoldingStrategy
        internal override sealed AbstractFoldingStrategy FoldingStrategy { get; set; }

        protected override string ShiftRegex
        {
            get { return @"((RobTarget\s*[\w]*\s*:=\s*\[\[)([\d.-]*),([\d.-]*),([-.\d]*))"; }
        }

        internal override bool IsFileValid(System.IO.FileInfo file)
        {
            return false;
        }

        internal override string FunctionItems
        {
            get { return  @"((?<!END)()()PROC\s([\d\w]*)[\(\)\w\d_. ]*)" ; }
        }

        #region Folding Section


        internal override string FoldTitle(FoldingSection section, TextDocument doc)
        {
            var s = Regex.Split(section.Title, "æ");

            var start = section.StartOffset + s[0].Length;
            var end = section.Length - (s[0].Length + s[1].Length);


            return doc.GetText(start, end);
        }
        /// <summary>
        /// The class to generate the foldings, it implements ICSharpCode.TextEditor.Document.IFoldingStrategy
        /// </summary>
        public class RegionFoldingStrategy : AbstractFoldingStrategy
        {
          

         

            /// <summary>
            /// Create <see cref="NewFolding"/>s for the specified document.
            /// </summary>
            public virtual IEnumerable<NewFolding> CreateNewFoldings(ITextSource document)
            {
                var newFoldings = new List<NewFolding>();

                newFoldings.AddRange(CreateFoldingHelper(document, "public function", "end function", true));
                newFoldings.AddRange(CreateFoldingHelper(document, "private function", "end function", true));
                newFoldings.AddRange(CreateFoldingHelper(document, "public sub", "end sub", true));
                newFoldings.AddRange(CreateFoldingHelper(document, "private sub", "end sub", true));
                newFoldings.AddRange(CreateFoldingHelper(document, "property", "end property", true));
                newFoldings.AddRange(CreateFoldingHelper(document, "If", "End If", false));
                newFoldings.AddRange(CreateFoldingHelper(document, "Select Case", "End Select", false));
                //                /Open Fold Strings = "Do While" "If" "ElseIf" "Function" "Sub" "With" "For" "Select Case" "Case Else" "Case" "Else"
                //Close Fold Strings = "ElseIf" "End If" "End Function" "End Sub" "End With" "Loop" "Next" "Wend" "End Select" "Case Else" "Case" "Else"

                newFoldings.Sort((a, b) => a.StartOffset.CompareTo(b.StartOffset));
                return newFoldings;
            }
               /// <summary>
            /// Create <see cref="NewFolding"/>s for the specified document.
            /// </summary>
            public override IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset)
            {
                firstErrorOffset = -1;
                return CreateNewFoldings(document);
            }

          
        }

        #endregion

        #region Code Completion Section

        internal override IList<ICompletionData> CodeCompletion
        {
            get
            {
                var codeCompletionList = new List<ICompletionData> {new CodeCompletion("Item1")};
                return codeCompletionList;
            }
        }
        #endregion

        public override Regex MethodRegex { get { return new Regex("( sub )", RegexOptions.IgnoreCase); } }

        public override Regex StructRegex { get { return new Regex("( struc )", RegexOptions.IgnoreCase); } }

        public override Regex FieldRegex { get { return new Regex("( boolean )", RegexOptions.IgnoreCase); } }

        public override Regex EnumRegex { get { return new Regex("( enum )", RegexOptions.IgnoreCase); } }

        public override string CommentChar { get { return "'"; } }

        public override Regex SignalRegex { get { return new Regex(String.Empty); } }
        public override string ExtractXYZ(string positionstring)
        {
            return String.Empty;
        }

        public override Regex XYZRegex { get { return new Regex(String.Empty); } }

        public override DocumentModel GetFile(string filepath)
        {
            return new DocumentModel(filepath);
        }
    }
    }

