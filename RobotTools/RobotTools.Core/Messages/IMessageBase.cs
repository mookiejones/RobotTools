using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotTools.Core.Messages
{
    public interface IMessageBase
    {
        string Title { get; }

        string Message { get; }


        DateTime Time { get; }

    }
}
