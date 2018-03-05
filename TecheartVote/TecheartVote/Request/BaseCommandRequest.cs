using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TecheartVote.Response;
using TecheartVote.Verification;

namespace TecheartVote.Request
{
    /// <summary>
    /// 基础下发指令
    /// </summary>
    public abstract class BaseCommandRequest
    {
        public BaseCommandRequest(HandshakeResponse response, shareAction1 s1, shareAction2 s2)
        {
            handshakeSecretKey = response.SecretKey;
            machineAddress = response.Address;
            share1 = s1;
            share2 = s2;
        }

        public abstract Share1Enum GetShare1Enum();
        /// <summary>
        /// 标准头信息默认值DD 
        /// </summary>
        public int head { get { return 0xDD; } }

        /// <summary>
        /// 表示下发还是上传的标识 下发有两种 一种是配置型下发 使用0  TODO在做注释 1B
        /// </summary>
        public int dataBelong { get; set; }

        /// <summary>
        /// 握手返回的密钥~ 2B
        /// </summary>
        public int handshakeSecretKey { get; set; }

        /// <summary>
        /// 主机地址 或者子机地址 4B
        /// </summary>
        public long machineAddress { get; set; }

        /// <summary>
        /// 不同命令有不同的解释需要子类进行解释 (1B)
        /// </summary>
        public int number { get; set; }

        /// <summary>
        /// 密码个数 (1B)
        /// </summary>
        public int dotPwoer { get; set; }

        /// <summary>
        /// 主体请求(8B)
        /// </summary>
        public UInt64 request { get; set; }

        /// <summary>
        /// 1B
        /// </summary>
        public shareAction1 share1 { get; set; }

        /// <summary>
        /// 1B
        /// </summary>
        public shareAction2 share2 { get; set; }
        /// <summary>
        /// 检测bit位 1B
        /// </summary>
        public int checkBit { get; set; }

        public Byte[] GetFinalArray()
        {
            List<Byte> listFinalBody = new List<byte>();
            //listFunal.Add(Convert.ToByte(head));

            //listFunal.Add(Convert.ToByte(dataBelong));

            //listFunal.Add(Convert.ToByte(handshakeSecretKey&0xFF00));
            //listFunal.Add(Convert.ToByte(handshakeSecretKey&0xFF));

            listFinalBody.Add(Convert.ToByte((machineAddress & 0xFF000000)>>24));
            listFinalBody.Add(Convert.ToByte((machineAddress & 0xFF0000)>>16));
            listFinalBody.Add(Convert.ToByte((machineAddress & 0xFF00)>>8));
            listFinalBody.Add(Convert.ToByte(machineAddress & 0xFF));

            listFinalBody.Add(Convert.ToByte(number));

            listFinalBody.Add(Convert.ToByte(dotPwoer));

            listFinalBody.Add(Convert.ToByte((request & 0xFF00000000000000)>>56));
            listFinalBody.Add(Convert.ToByte((request & 0xFF000000000000)>>48));
            listFinalBody.Add(Convert.ToByte((request & 0xFF0000000000)>>40));
            listFinalBody.Add(Convert.ToByte((request & 0xFF00000000)>>32));
            listFinalBody.Add(Convert.ToByte((request & 0xFF000000)>>24));
            listFinalBody.Add(Convert.ToByte((request & 0xFF0000)>>16));
            listFinalBody.Add(Convert.ToByte((request & 0xFF00)>>8));
            listFinalBody.Add(Convert.ToByte(request & 0xFF));

            listFinalBody.Add(share1.GetShare1Byte(GetShare1Enum()));
            listFinalBody.Add(share2.GetShare2Byte());


            List<Byte> listFinal = new List<byte>();
            listFinal.Add(Convert.ToByte(head));

            listFinal.Add(Convert.ToByte(dataBelong));

            listFinal.Add(Convert.ToByte((handshakeSecretKey & 0xFF00)>>8));
            listFinal.Add(Convert.ToByte(handshakeSecretKey & 0xFF));
            listFinal.AddRange(Cryptogram.Encryption(listFinalBody, listFinal[3], listFinal[2]));

            listFinal.Add(Convert.ToByte(VerificationTools.HashCalc(listFinal)));

            return listFinal.ToArray();
        }
    }
}
