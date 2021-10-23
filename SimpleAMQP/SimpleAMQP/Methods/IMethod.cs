namespace SimpleAMQP.Methods
{
    internal interface IMethod
    {
        short ClassId { get; }

        short MethodId { get; }
    }
}
