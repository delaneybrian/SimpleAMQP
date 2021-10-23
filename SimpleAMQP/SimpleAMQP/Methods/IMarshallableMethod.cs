namespace SimpleAMQP.Methods
{
    internal interface IMarshallableMethod : IMethod
    {
        bool IsFor(short classId, short methodId);

        byte[] Marshall();
    }
}
