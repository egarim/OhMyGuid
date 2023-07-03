using System;
using System.Linq;

namespace OhMyGuid
{
    public static class GuidExtensions
    {
        public static long ExtractTimestamp(this Guid guid)
        {
            byte[] bytes = guid.ToByteArray();
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, 0, 8);
            }

            long timestamp = BitConverter.ToInt64(bytes, 0);
            return timestamp;
        }

        public static int CompareToByTimestamp(this Guid guid1, Guid guid2)
        {
            long timestamp1 = guid1.ExtractTimestamp();
            long timestamp2 = guid2.ExtractTimestamp();

            return timestamp1.CompareTo(timestamp2);
        }
    }
}