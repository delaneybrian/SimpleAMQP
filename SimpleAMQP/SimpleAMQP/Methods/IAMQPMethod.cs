using System;

namespace SimpleAMQP
{
    public interface IAMQPMethod
    {
        short ClassId { get; }

        short MethodId { get; }

        bool IsFor(int classId, int methodId);

        byte[] Marshall();
    }
}
