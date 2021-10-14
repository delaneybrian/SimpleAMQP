using System.Drawing;

namespace SimpleAMQP
{
    public interface IAMQPMethod
    {
        short ClassId { get; }

        short MethodId { get; }

        bool IsFor(int classId, int methodId);

        byte[] Marshall();

        void Construct(byte[] bytes);
    }
}
