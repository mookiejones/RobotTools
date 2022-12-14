/*
 * Created by SharpDevelop.
 * User: cberman
 * Date: 9/23/2012
 * Time: 1:27 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace miRobotEditor.EditorControl.Interfaces
{
	
	
	/// <summary>
	/// Interface for text editors.
	/// </summary>
	public interface ITextEditor : IServiceProvider
	{
		/// <summary>
		/// Gets the primary view if split-view is active.
		/// If split-view is disabled, the current ITextEditor instance is returned.
		/// This property never returns null.
		/// </summary>
		/// <example>bool isSecondaryView = (editor != editor.PrimaryView);</example>
		ITextEditor PrimaryView { get; }
		
		/// <summary>
		/// Gets the document that is being edited.
		/// </summary>
		IEditor Document { get; }
		
		/// <summary>
		/// Gets an object that represents the caret inside this text editor.
		/// This property never returns null.
		/// </summary>
		ITextEditorCaret Caret { get; }
		
		/// <summary>
		/// Gets the set of options used in the text editor.
		/// This property never returns null.
		/// </summary>
		ITextEditorOptions Options { get; }
		
		
		/// <summary>
		/// Gets the start offset of the selection.
		/// </summary>
		int SelectionStart { get; }
		
		/// <summary>
		/// Gets the length of the selection.
		/// </summary>
		int SelectionLength { get; }
		
		/// <summary>
		/// Gets/Sets the selected text.
		/// </summary>
		string SelectedText { get; set; }
		
		/// <summary>
		/// Sets the selection.
		/// </summary>
		/// <param name="selectionStart">Start offset of the selection</param>
		/// <param name="selectionLength">Length of the selection</param>
		void Select(int selectionStart, int selectionLength);
		
		/// <summary>
		/// Is raised when the selection changes.
		/// </summary>
		event EventHandler SelectionChanged;
		
		/// <summary>
		/// Is raised before a key is pressed.
		/// </summary>
		event KeyEventHandler KeyPress;
		
		/// <summary>
		/// Sets the caret to the specified line/column and brings the caret into view.
		/// </summary>
		void JumpTo(int line, int column);
		
		//Filename Filename { get; }
		
		ICompletionListWindow ShowCompletionWindow(ICompletionItemList data);
		
		/// <summary>
		/// Gets the completion window that is currently open.
		/// </summary>
		ICompletionListWindow ActiveCompletionWindow { get; }
		
		/// <summary>
		/// Open a new insight window showing the specified insight items.
		/// </summary>
		/// <param name="items">The insight items to show in the window.
		/// If this property is null or an empty list, the insight window will not be shown.</param>
		/// <returns>The insight window; or null if no insight window was opened.</returns>
		IInsightWindow ShowInsightWindow(IEnumerable<IInsightItem> items);
		
		/// <summary>
		/// Gets the insight window that is currently open.
		/// </summary>
		IInsightWindow ActiveInsightWindow { get; }
		
		/// <summary>
		/// Gets the list of available code snippets.
		/// </summary>
		IEnumerable<ICompletionItem> GetSnippets();
	}
}
