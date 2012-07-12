using System;

namespace Amazon.Infrastructure.General.Interfaces
{
    public interface ITimer
    {
        void RegisterCallBack(Action action);
    }
}
