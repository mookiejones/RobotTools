using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace miRobotEditor.EditorControl.Languages
{
    public class PositionBase:IPosition
    {
        public PositionBase(string value)
        {           
            RawValue = value;
            ParseValues();
        }
       
        public override string ToString()
        {
            return RawValue;
        }

        public string Type { get; set; }
        public string RawValue { get; set; }
        public string Scope { get; set; }
        public string Name { get; set; }
        //  public List<PositionValue> PositionalValues { get; set; }

        ObservableCollection<PositionValue> _values = new ObservableCollection<PositionValue>();
        readonly ReadOnlyObservableCollection<PositionValue> _positionalValues = null;

        public ReadOnlyObservableCollection<PositionValue> PositionalValues { get { return _positionalValues ?? new ReadOnlyObservableCollection<PositionValue>(_values); } }


        public void ParseValues()
        {
            try
            {
                _values = new ObservableCollection<PositionValue>();
                var sp = RawValue.Split('=');
                var decl = sp[1].Substring(1, sp[1].Length - 2).Split(',');

                foreach (var ss in decl.Select(s => s.Split(' ')))
                {
                    _values.Add(new PositionValue { Name = ss[0], Value = ss[1] });
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch { }
// ReSharper restore EmptyGeneralCatchClause
        }

// ReSharper disable UnusedMember.Local
        private string ConvertFromHex(string value)
// ReSharper restore UnusedMember.Local
        {
            var d = double.Parse(value.Substring(1, value.Length - 2), NumberStyles.HexNumber);
            return Convert.ToString(d);
        }

// ReSharper disable UnusedMember.Local
        private bool IsNumeric(string value)
// ReSharper restore UnusedMember.Local
        {
            try
            {
#pragma warning disable 168
                var v = Convert.ToDouble(value);
#pragma warning restore 168
                return true;
            }
            catch
            {
                return false;
            }

        }

        [Localizable(false)]
        public string ExtractFromMatch()
        {
            var result = string.Empty;
            try
            {
                result = PositionalValues.Aggregate(result, (current, v) => current + String.Format("{0} {1},", v.Name, v.Value));

                return result.Substring(0, result.Length - 1);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}