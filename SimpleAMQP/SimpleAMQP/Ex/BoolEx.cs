namespace SimpleAMQP.Ex
{
    internal static class BoolEx
    {
        public static byte ToByte(this bool value)
        {
            return value 
                ? (byte) 1 
                : (byte) 0;
        }
    }
}
