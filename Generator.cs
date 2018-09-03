using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
/*Reversed Engineered a program to overall convert/download videos From Youtube -
 * This allows you to generate a token and use it on there API.
*/
namespace TokenGenerator
{

    internal static class Authorization
    {

        public static bool CheckToken(string hash)
        {
            string text;
            using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.Key = Encoding.ASCII.GetBytes("x1(V2%yt+A!,GJq,");
                rijndaelManaged.IV = Encoding.ASCII.GetBytes("gez5spe8wa$@spep");
                ICryptoTransform transform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                using (MemoryStream memoryStream = new MemoryStream(Authorization.StringToByteArray(hash)))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            text = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return text.StartsWith("DLKJFNI&W347JDH");
        }

        public static string GenerateToken()
        {
            DateTime dateTime;
            try
            {
                dateTime = Authorization.GetNetworkTime(true);
            }
            catch
            {
                dateTime = DateTime.UtcNow;
            }
            int num = (int)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string value = "DLKJFNI&W347JDH" + ":" + num;
            byte[] bytes;
            using (RijndaelManaged rijndaelManaged = new RijndaelManaged())
            {
                rijndaelManaged.IV = Encoding.ASCII.GetBytes("gez5spe8wa$@spep");
                rijndaelManaged.KeySize = 128;
                rijndaelManaged.Key = Encoding.ASCII.GetBytes("x1(V2%yt+A!,GJq,");
                ICryptoTransform transform = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(value);
                        }
                        bytes = memoryStream.ToArray();
                    }
                }
            }
            return Authorization.ByteArrayString(bytes);
        }

        private static string ByteArrayString(byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder(bytes.Length);
            foreach (byte b in bytes)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }

        private static byte[] StringToByteArray(string hex)
        {
            return (from x in Enumerable.Range(0, hex.Length)
                    where x % 2 == 0
                    select Convert.ToByte(hex.Substring(x, 2), 16)).ToArray<byte>();
        }

        public static DateTime GetNetworkTime(bool utc)
        {
            byte[] array = new byte[48];
            array[0] = 27;
            IPEndPoint remoteEP = new IPEndPoint(Dns.GetHostEntry("time.windows.com").AddressList[0], 123);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Connect(remoteEP);
            socket.ReceiveTimeout = 3000;
            socket.Send(array);
            socket.Receive(array);
            socket.Close();
            ulong x = (ulong)BitConverter.ToUInt32(array, 40);
            ulong num = (ulong)BitConverter.ToUInt32(array, 44);
            ulong num2 = (ulong)Authorization.SwapEndianness(x);
            num = (ulong)Authorization.SwapEndianness(num);
            ulong num3 = num2 * 1000UL + num * 1000UL / 4294967296UL;
            DateTime result = new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((double)num3);
            if (!utc)
            {
                return result.ToLocalTime();
            }
            return result;
        }

        private static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 255UL) << 24) + ((x & 65280UL) << 8) + ((x & 16711680UL) >> 8) + ((x) >> 24));
        }

        private const string Key = "x1(V2%yt+A!,GJq,";

        private const string Passphrase = "gez5spe8wa$@spep";

        private const string TokenBase = "DLKJFNI&W347JDH";
    }
}
