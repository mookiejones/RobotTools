﻿using System;
using System.ComponentModel;
using ICSharpCode.AvalonEdit.Folding;
using RobotTools.Editor.TextEditor.ViewModels;

namespace RobotTools.Editor.TextEditor.Folding
{

    internal sealed class LanguageFold : NewFolding
    {
        [Localizable(false)]
        public LanguageFold(int start, int end, string text, string startfold, string endfold, bool closed)
            : base(start, end)
        {
            Name = string.Format("{0}æ{1}", startfold, endfold);
            StartFold = startfold;
            EndFold = endfold;
            DefaultClosed = closed;
            Start = start;
            End = end;
            Text = text;
            var text2 = text;
            var num = text2.IndexOf("\r\n", StringComparison.Ordinal);
            var num2 = text2.IndexOf('%');
            if (num2 > -1)
            {
                text2 = text2.Substring(0, num2);
            }
            else
            {
                if (num > -1)
                {
                    text2 = text2.Substring(0, num);
                }
            }
            ToolTip = new ToolTipViewModel
            {
                Title = text2,
                Message = text
            };
        }

        public string StartFold { get; private set; }
        internal string EndFold { get; set; }
        public string Text { get; private set; }
        public int Start { get; private set; }
        public int End { get; private set; }
        internal ToolTipViewModel ToolTip { get; private set; }
    }
}