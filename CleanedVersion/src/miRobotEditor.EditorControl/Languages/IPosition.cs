using System.Collections.ObjectModel;

namespace miRobotEditor.EditorControl.Languages
{
    public interface IPosition
    {
         string RawValue { get; set; }
         string Scope { get; set; }
         string Name { get; set; }
         string Type { get; set; }
         ReadOnlyObservableCollection<PositionValue> PositionalValues { get;  }
         void ParseValues();
         string ExtractFromMatch();
       
    }
}
