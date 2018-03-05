using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TecheartVote.Response;
using TecheartVote.Verification;

namespace TecheartVote
{
    public static class HandshakeTools
    {
        public static HandshakeResponse AnalysisHandshake(Int32[] text)
        {
            HandshakeResponse handshake = new HandshakeResponse();
            if (text == null || text.Count()!=82)
            {
                throw new Exception("握手异常");
            }
            //TODO 散列校验

            List<Int32> listtext=text.ToList();
            if(!VerificationTools.HandshakeHeadVerification(listtext) || !VerificationTools.HashCheck(listtext))
            {
                throw new Exception("握手异常-握手返回值异常");
            }
            handshake.Channel = listtext[2];
            int highSecretKey = listtext[22] << 8;
            int lowSecretKey = listtext[23];
            handshake.SecretKey = highSecretKey + lowSecretKey;
            long fisrtAddress=((long)listtext[24]) << 24;
            long secondAddress = ((long)listtext[25]) << 16;
            long thirdAddress= ((long)listtext[26]) << 8;
            long fourthAddress = ((long)listtext[27]);
            handshake.Address = fisrtAddress + secondAddress + thirdAddress + fourthAddress;
            List<Byte> remorklist = new List<Byte>();
            for(int i = 34; i < 79; i++)
            {
                remorklist.Add((Byte)listtext[i]);
            }
            handshake.Remark=Encoding.GetEncoding("GBK").GetString(remorklist.ToArray()).Replace("\0", "");
            return handshake;
        }
        
    }
}
