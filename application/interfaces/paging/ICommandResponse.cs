using System;

namespace application.interfaces.paging
{
    public interface ICommandResponse<Tidentity> where Tidentity : struct, IEquatable<Tidentity>, IComparable<Tidentity>
    {
        Tidentity ID { get; }
        string Entity { get; }
    }
}
