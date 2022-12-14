// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1014:MarkAssembliesWithClsCompliant")]
[assembly: SuppressMessage("Microsoft.Usage", "CA2243:AttributeStringLiteralsShouldParseCorrectly")]
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "type", Target = "miRobotEditor.Images.resources_icons_xaml")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "miRobotEditor.ViewModel.ViewModelLocator.#Messages")]
[assembly: SuppressMessage("Microsoft.Usage", "CA2241:Provide correct arguments to formatting methods", Scope = "member", Target = "miRobotEditor.App.#Application_DispatcherUnhandledException(System.Object,System.Windows.Threading.DispatcherUnhandledExceptionEventArgs)")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "miRobotEditor.App.#HandleError(System.Object,System.Windows.Threading.DispatcherUnhandledExceptionEventArgs)")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String,System.Object)", Scope = "member", Target = "miRobotEditor.App.#Application_DispatcherUnhandledException(System.Object,System.Windows.Threading.DispatcherUnhandledExceptionEventArgs)")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "miRobotEditor.LayoutItemSelector.#SelectTemplate(System.Object,System.Windows.DependencyObject)")]
[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SelectTemplate", Scope = "member", Target = "miRobotEditor.LayoutItemSelector.#SelectTemplate(System.Object,System.Windows.DependencyObject)")]
[assembly: SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String,System.Object)", Scope = "member", Target = "miRobotEditor.LayoutItemSelector.#SelectTemplate(System.Object,System.Windows.DependencyObject)")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "member", Target = "miRobotEditor.ViewModel.ViewModelLocator.#GetProvider()")]
