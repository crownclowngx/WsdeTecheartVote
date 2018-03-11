using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Ports;
using System.Collections.Generic;
using System.Text;
using TecheartVote;
using TecheartVote.Verification;
using System.Linq;
using System.IO;

namespace WsdeUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

        }
        [TestMethod]
        public void SerialPortTest()
        {
            SerialPort serialPort = new SerialPort();
            serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.OnDataReceived);
            serialPort.BaudRate = 115200;
            serialPort.PortName = "COM3";
            serialPort.DataBits = 8;
            serialPort.Open();
            Byte[] TxData = {};
            List<Byte> l = new List<byte>();
            l.Add(0xAA);
            l.Add(0x10);
            for (int i = 0; i < 79; i++)
            {
                l.Add(0x00);
            }
            l.Add(0xBA);
            TxData=l.ToArray();
            serialPort.Write(TxData, 0, 82);
            Console.Read();
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            Console.ReadKey();
        }
        [TestMethod]
        public void Uncode()
        {
            SubVoteDisplayAction.GetDisplayData("ABCDE");
        }
        [TestMethod]
        public void AnalysisHandshakeProtocol()
        {
            int[] TxData = { 170, 17, 1, 1, 1, 1, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 2, 9, 0, 0, 0, 2, 0, 255, 0, 255, 0, 255, 177, 177, 190, 169, 201, 207, 206, 170, 190, 253, 179, 207, 191, 198, 188, 188, 211, 208, 207, 222, 185, 171, 203, 190, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 193};
            //var k=HandshakeTools.AnalysisHandshake(TxData);
            var kk = VerificationTools.HashCheck(TxData.ToList());
        }
    }
}
