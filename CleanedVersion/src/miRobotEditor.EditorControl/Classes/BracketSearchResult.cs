﻿namespace miRobotEditor.EditorControl.Classes
{
    /// <summary>
    /// Describes a pair of matching brackets found by IBracketSearcher.
    /// </summary>
    public sealed class BracketSearchResult
    {
        public int OpeningBracketOffset { get; private set; }
		
        public int OpeningBracketLength { get; private set; }
		
        public int ClosingBracketOffset { get; private set; }

        public int ClosingBracketLength { get; private set; }
		
        public BracketSearchResult(int openingBracketOffset, int openingBracketLength,
                                   int closingBracketOffset, int closingBracketLength)
        {
            OpeningBracketOffset = openingBracketOffset;
            OpeningBracketLength = openingBracketLength;
            ClosingBracketOffset = closingBracketOffset;
            ClosingBracketLength = closingBracketLength;
        }
    }
}