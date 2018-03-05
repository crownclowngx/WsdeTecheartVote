using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote
{
    public class Cryptogram
    {
        static Byte[] OriginalCipher = new Byte[] { 0x11, 0xab, 0x22, 0x7a, 0xf9, 0x7b, 0xb2, 0x94, 0xee, 0x9c, 0x0c, 0xbb, 0xf0, 0x0d, 0xea, 0x24 };
        public static Byte[] Encryption(List<Byte> list, Byte lowByte, Byte highByte)
        {
            Byte[] EncryptionData = new Byte[16];
            var dynamicCipher = GetDynamicCipher( lowByte, highByte);
            for (int i = 0; i < list.Count; i++)
            {
                EncryptionData[i] = Convert.ToByte(list[i] ^ dynamicCipher[i]);
            }
            return EncryptionData;
        }

        public static Byte[] GetDynamicCipher( Byte lowByte, Byte highByte)
        {
            Byte[] newDynamic = new Byte[16];
            for (int i = 0; i < OriginalCipher.Count(); i++)
            {
                if (i % 2 == 0)
                {
                    newDynamic[i] = Convert.ToByte((OriginalCipher[i] + lowByte) % 256);
                }
                else
                {
                    newDynamic[i] = Convert.ToByte((OriginalCipher[i] + highByte) % 256);
                }
            }
            return newDynamic;
        }
    }
}
