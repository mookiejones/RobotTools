using System;
using System.Diagnostics.CodeAnalysis;
using miRobotEditor.Model;

namespace miRobotEditor.Design
{
    public class DesignDataService : IDataService
    {
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public void GetData(Action<DataItem, Exception> callback)
        {
            // Use this to create design time data

            var item = new DataItem("Welcome to MVVM Light [design]");
            callback(item, null);
        }
    }
}