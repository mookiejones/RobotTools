using System;
using System.Collections.Generic;

namespace Plugin_Setup.Setup
{

    internal sealed class Language
    {
        private struct Languages
        {
            public string English;
            public string Deutsch;
        }
        public class LanguageString
        {
            private string _English;
            private string _Deutsch;
            public string English
            {
                get
                {
                    return this._English;
                }
                set
                {
                    this._English = value;
                }
            }
            public string Deutsch
            {
                get
                {
                    return this._Deutsch;
                }
                set
                {
                    this._Deutsch = value;
                }
            }
            public LanguageString()
            {
            }
            public LanguageString(string English, string Deutsch)
            {
                this.English = English;
                this.Deutsch = Deutsch;
            }
        }
        private static List<Language.LanguageString> lstStrings = new List<Language.LanguageString>();
        public static string LanguageAct = "en";
        public static void LangInit()
        {
            Language.lstStrings.Clear();
            Language.lstStrings.Add(new Language.LanguageString("Ok", "Ok"));
            Language.lstStrings.Add(new Language.LanguageString("I Accept", "Akzeptiere"));
            Language.lstStrings.Add(new Language.LanguageString("Cancel", "Abbrechen"));
            Language.lstStrings.Add(new Language.LanguageString("Continue", "Weiter"));
            Language.lstStrings.Add(new Language.LanguageString("License", "Lizenz"));
            Language.lstStrings.Add(new Language.LanguageString("not a KRC4 environment", "keine KRC4 Umgebung"));
            Language.lstStrings.Add(new Language.LanguageString("This is not a KRC4 environment! Setup canceled...", "Dies ist kein KRC4 System! Setup beendet..."));
            Language.lstStrings.Add(new Language.LanguageString("newer version already installed", "neuere Version bereits installiert"));
            Language.lstStrings.Add(new Language.LanguageString("Newer Version of {0} already installed", "Neuere Version von {0} bereits installiert"));
            Language.lstStrings.Add(new Language.LanguageString("same version already installed", "gleiche Version bereits installiert"));
            Language.lstStrings.Add(new Language.LanguageString("Same Version of {0} already installed", "Gleiche Version von {0} bereits installiert"));
            Language.lstStrings.Add(new Language.LanguageString("older version already installed, run update", "ältere Version bereits installiert, Update wird gestartet"));
            Language.lstStrings.Add(new Language.LanguageString("You must accept the terms of this License Agreement before continuing with the installation.", "Sie müssen die Lizenzvereinbarungen akzeptieren um mit der Installation fortzufahren."));
            Language.lstStrings.Add(new Language.LanguageString("Installation prepared! Please restart control PC.", "Installation vorbereitet! Bitte Steuerungs-PC neu starten"));
            Language.lstStrings.Add(new Language.LanguageString("{0} successfully installed", "{0} erfolgreich installiert"));
            Language.lstStrings.Add(new Language.LanguageString("Uninstallation prepared! Please restart control PC.", "Deinstallation vorbereitet! Bitte Steuerungs-PC neu starten"));
            Language.lstStrings.Add(new Language.LanguageString("{0} successfully uninstalled", "{0} erfolgreich deinstalliert"));
            Language.lstStrings.Add(new Language.LanguageString("execute {0}", "{0} wird ausgeführt"));
            Language.lstStrings.Add(new Language.LanguageString("stopping SmartHMI", "SmartHMI wird beendet"));
            Language.lstStrings.Add(new Language.LanguageString("starting SmartHMI", "SmartHMI wird gestartet"));
            Language.lstStrings.Add(new Language.LanguageString("updating registry information", "Registry wird aktualisiert"));
            Language.lstStrings.Add(new Language.LanguageString("current KRC version is {0}", "aktuelle KRC Version ist {0}"));
            Language.lstStrings.Add(new Language.LanguageString("delete file {0}", "entferne Datei {0}"));
            Language.lstStrings.Add(new Language.LanguageString("copy file {0}", "kopiere Datei {0}"));
            Language.lstStrings.Add(new Language.LanguageString("make directory {0}", "erstelle Verzeichnis {0}"));
            Language.lstStrings.Add(new Language.LanguageString("delete directory {0}", "entferne Verzeichnis {0}"));
            Language.lstStrings.Add(new Language.LanguageString("creating uninstall and reinstall information", "Uninstall und Reinstall informationen werden erstellt"));
            Language.lstStrings.Add(new Language.LanguageString("KRC Version not supported", "KRC Version nicht unterstützt"));
            Language.lstStrings.Add(new Language.LanguageString("KRC Version {0} is not supported", "KRC Version {0} wird nicht unterstützt"));
            Language.lstStrings.Add(new Language.LanguageString("set reload files", "setze Dateien neu einlesen"));
            Language.lstStrings.Add(new Language.LanguageString("version {0} already installed", "Version {0} bereits installiert"));
            Language.lstStrings.Add(new Language.LanguageString("set auto run for nex system start", "setzte Autostart für nächsten Systemstart"));
            Language.lstStrings.Add(new Language.LanguageString("installation prepared", "Installation vorbereitet"));
            Language.lstStrings.Add(new Language.LanguageString("uninstallation prepared", "Deinstallation vorbereitet"));
            Language.lstStrings.Add(new Language.LanguageString("Reboot control PC now", "Steuerungs-PC jetzt neu starten"));
            Language.lstStrings.Add(new Language.LanguageString("Later", "Später"));
            Language.lstStrings.Add(new Language.LanguageString("{0} warnings", "{0} warnungen"));
            Language.lstStrings.Add(new Language.LanguageString("{0} errors", "{0} fehler"));
            Language.lstStrings.Add(new Language.LanguageString("file {0} does not exist", "Datei {0} existiert nicht"));
            Language.lstStrings.Add(new Language.LanguageString("directory {0} exists already", "Verzeichnis {0} existiert bereits"));
            Language.lstStrings.Add(new Language.LanguageString("directory {0} does not exist", "Verzeichnis {0} existiert nicht"));
            Language.lstStrings.Add(new Language.LanguageString("see LOG file for more information", "Sehen sie in die LOG Datei für mehr inforamtionen"));
            Language.lstStrings.Add(new Language.LanguageString("copy files of directory {0}", "kopiere Dateien vom Verzeichnis {0}"));
            Language.lstStrings.Add(new Language.LanguageString("directory does not exist", "Verzeichnis existiert nicht"));
        }
        public static object s(string Text)
        {
            try
            {
                List<Language.LanguageString>.Enumerator enumerator = Language.lstStrings.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Language.LanguageString current = enumerator.Current;
                    if (Operators.CompareString(current.English, Text, false) == 0)
                    {
                        string languageAct = Language.LanguageAct;
                        if (Operators.CompareString(languageAct, "en", false) == 0)
                        {
                            object result = current.English;
                            return result;
                        }
                        if (Operators.CompareString(languageAct, "de", false) == 0)
                        {
                            object result = current.Deutsch;
                            return result;
                        }
                    }
                }
            }
            finally
            {
                List<Language.LanguageString>.Enumerator enumerator;
                ((IDisposable)enumerator).Dispose();
            }
            return Text;
        }
    }
}
