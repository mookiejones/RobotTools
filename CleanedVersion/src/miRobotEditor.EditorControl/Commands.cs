using System.Windows.Input;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Search;

namespace miRobotEditor.EditorControl
{
    public static class Commands
    {


        // Summary:
        //     Converts leading spaces to tabs in the selected lines (or the whole document
        //     if the selection is empty).
        public static RoutedCommand ConvertLeadingSpacesToTabs { get { return AvalonEditCommands.ConvertLeadingSpacesToTabs; } }

        //
        // Summary:
        //     Converts leading tabs to spaces in the selected lines (or the whole document
        //     if the selection is empty).
        public static  RoutedCommand ConvertLeadingTabsToSpaces { get { return AvalonEditCommands.ConvertLeadingTabsToSpaces; } }
        //
        // Summary:
        //     Converts spaces to tabs in the selected text.
        public static RoutedCommand ConvertSpacesToTabs { get { return AvalonEditCommands.ConvertSpacesToTabs; } }

        //
        // Summary:
        //     Converts tabs to spaces in the selected text.
        public static  RoutedCommand ConvertTabsToSpaces { get { return AvalonEditCommands.ConvertTabsToSpaces; } }
        //
        // Summary:
        //     Converts the selected text to lower case.
        public static RoutedCommand ConvertToLowercase { get { return AvalonEditCommands.ConvertToLowercase; } }
        //
        // Summary:
        //     Converts the selected text to title case.
        public static RoutedCommand ConvertToTitleCase { get { return AvalonEditCommands.ConvertToTitleCase; } }
        //
        // Summary:
        //     Converts the selected text to upper case.
        public static RoutedCommand ConvertToUppercase { get { return AvalonEditCommands.ConvertToUppercase; } }
        //
        // Summary:
        //     Deletes the current line.  The default shortcut is Ctrl+D.
        public static  RoutedCommand DeleteLine { get { return AvalonEditCommands.DeleteLine; } }
        //
        // Summary:
        //     Runs the IIndentationStrategy on the selected lines (or the whole document
        //     if the selection is empty).
        public static RoutedCommand IndentSelection { get { return AvalonEditCommands.IndentSelection; } }
        //
        // Summary:
        //     Inverts the case of the selected text.
        public static RoutedCommand InvertCase { get { return AvalonEditCommands.InvertCase; } }
        //
        // Summary:
        //     Removes leading whitespace from the selected lines (or the whole document
        //     if the selection is empty).
        public static RoutedCommand RemoveLeadingWhitespace { get { return AvalonEditCommands.RemoveLeadingWhitespace; } }
        //
        // Summary:
        //     Removes trailing whitespace from the selected lines (or the whole document
        //     if the selection is empty).
        public static RoutedCommand RemoveTrailingWhitespace { get { return AvalonEditCommands.RemoveTrailingWhitespace; } }


        public static RoutedCommand FindNext { get { return SearchCommands.FindNext; } }
        public static RoutedCommand FindPrevious { get { return SearchCommands.FindPrevious; } }
        public static RoutedCommand CloseSearchPanel { get { return SearchCommands.CloseSearchPanel; } }
    }
}
