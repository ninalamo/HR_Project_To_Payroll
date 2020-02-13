using System;

namespace application.interfaces.paging
{
    public interface ICommandResponse
    {
        object ID { get; }
        string Entity { get; }
    }
}
