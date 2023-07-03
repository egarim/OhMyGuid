using System;
using System.Security.Cryptography;
using System.Threading;

namespace OhMyGuid
{
    public static class GuidService
    {
        /// <summary>
        /// This function uses a 6-byte (millisecond-precision) timestamp
        /// which means the generated GUIDs will remain unique and sortable for about 9000 years.
        /// It's important to keep in mind that this will generate a Guid which is not standard 
        /// and does not adhere to the typical structure of a Guid, but if you're only using it within your own system and its main purpose is to be sortable by creation time 
        /// then it should serve your needs well.
        /// </summary>
        /// <returns></returns>
        public static Guid NewTimeBasedGuid()
        {
            Thread.Sleep(200);
            byte[] randomBytes = new byte[10];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            long timestamp = DateTime.UtcNow.Ticks / 10000L;
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            byte[] guidBytes = new byte[16];
            Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
            Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

            return new Guid(guidBytes);
        }

        static void CreateGuid(bool CreateCombGuid)
        {
            if(CreateCombGuid)
            {
                GuidService.NewTimeBasedGuid();
            }
            else
            {
                GuidService.RegularGuid();
            }
        }
        public static Guid RegularGuid()
        {
            return Guid.NewGuid();
        }
        public static long ExtractTicksFromGuid(Guid guid)
        {
            var bytes = guid.ToByteArray();
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes, 0, 8);
            }

            return BitConverter.ToInt64(bytes, 0);
        }
    }
}