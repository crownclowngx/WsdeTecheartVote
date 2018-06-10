using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TecheartVote;
using TecheartVote.UsbManager;

namespace Demo
{
    public partial class Form1 : Form
    {
        bool completion = false;
        Dictionary<long, int> sources = new Dictionary<long, int>();
        static WsdePort post = null;
        public Form1()
        {
            InitializeComponent();
            WsdeUsbManager manager = new WsdeUsbManager();
            manager.OnWsdeUsbComed += new WsdeUsbManager.OnWsdeUsbHandler(OnWsdeUsbComed);
            manager.OnWsdeUsbExited += new WsdeUsbManager.OnWsdeUsbHandler(OnWsdeUsbExitHandler);
        }
        public void OnWsdeUsbExitHandler(WsdePort wsdePort)
        {
            label2.Invoke(new Action(() => { label2.Text = ""; }));
            MessageBox.Show("主机已被拔出 名称："+wsdePort.wsdeName);
        }
        public  void OnWsdeUsbComed(WsdePort wsdePort)
        {
            post = wsdePort;
            label2.Invoke(new Action(()=> { label2.Text = post.wsdeName; }));
            post.OnDataCome += new WsdePort.OnDataComeHandler(OnDataComeHandler2);
        }
        private void OnDataComeHandler2(WsdePort handshake, SubSelect subselect)
        {
            if (!sources.ContainsKey(subselect.address))
            {
                sources.Add(subselect.address, 0);
            }
            bool isright = post.subAnswerDic.IsRight(subselect.subjectNumber, subselect.selectData);
            if (isright)
            {
                sources[subselect.address] += post.subAnswerDic.GetScore(subselect.subjectNumber);
            }
            listViewMonitor.Invoke(new Action(() => { listViewMonitor.Items.Add(String.Format("子机编号:{0}，题号:{1},选择答案:{2},答案是否正确{3}", subselect.address, subselect.subjectNumber, subselect.selectData, isright)); }));
        }

        /// <summary>
        /// 设置初始化配置，即设置信道等基本配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var channelselect = channel.SelectedItem.ToString();
            var frequencyselect = FrequencyEnum.dBM0;
            if (frequency.SelectedItem.ToString() == "dBM1")
            {
                frequencyselect = FrequencyEnum.dBM1;
            }
            post.InitConf(new ConfAction() { channel = Convert.ToInt32(channelselect), frequency = frequencyselect });
        }

        /// <summary>
        /// 设置密码表 即子机可以通过该密码与主机通讯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            var secret = SecretText.Text.Replace("\r","").Split('\n').ToList();
            post.SetAccessPasswords(secret.ConvertAll(k => Convert.ToUInt64(k)));
        }

        /// <summary>
        /// 设置动态配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (persistenceConfiguration.Checked)
                post.shareAction1P.persistenceConfiguration = true;
            else
                post.shareAction1P.persistenceConfiguration = false;

            if (clientCanClearSoon.Checked)
                post.shareAction1P.clientCanClearSoon = true;
            else
                post.shareAction1P.clientCanClearSoon = false;

            if (clientCanAnswer.Checked)
                post.shareAction1P.clientCanAnswer = true;
            else
                post.shareAction1P.clientCanAnswer = false;

            if (eraseClientMemory.Checked)
                post.shareAction1P.eraseClientMemory = true;
            else
                post.shareAction1P.eraseClientMemory = false;

            if (clientCanSeeSolution.Checked)
                post.shareAction1P.clientCanSeeSolution = true;
            else
                post.shareAction1P.clientCanSeeSolution = false;

            if (clientCanWriteNumber.Checked)
                post.shareAction2P.clientCanWriteNumber = true;
            else
                post.shareAction2P.clientCanWriteNumber = false;

            if (clientCanWriteABC.Checked)
                post.shareAction2P.clientCanWriteABC = true;
            else
                post.shareAction2P.clientCanWriteABC = false;

            if (clientCanChangeChannel.Checked)
                post.shareAction2P.clientCanChangeChannel = true;
            else
                post.shareAction2P.clientCanChangeChannel = false;

            if (clientCanErase.Checked)
                post.shareAction2P.clientCanErase = true;
            else
                post.shareAction2P.clientCanErase = false;

            if (clientCanChangeDate.Checked)
            {
                post.shareAction2P.clientCanChangeDate = true;
                post.shareAction2P.clientCanCnahgeTime=true;
            }
            else
            {
                post.shareAction2P.clientCanChangeDate = false;
                post.shareAction2P.clientCanCnahgeTime = false;
            }
             
            if (clinetCanSeeAnswer.Checked)
                post.shareAction2P.clinetCanSeeAnswer = true;
            else
                post.shareAction2P.clinetCanSeeAnswer = false;

            post.UpdateDynamicConf();

        }

        /// <summary>
        /// 设置题号以及答案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            var listpro=TitleEditor.Text.Replace("\r", "").Split('\n').ToList();
            if (listpro == null)
            {
                return;
            }
            listpro.ForEach(k => 
            {
                var arr =k.Split(':');
                if (Convert.ToInt32(arr[0]) <= 119)
                {
                    post.subAnswerDic.SetAnswer(Convert.ToInt32(arr[0]), arr[1], Convert.ToInt32(arr[2]));
                }
            });
            post.PushAnswer();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            completion = true;
            listSorceMonitor.Items.Clear();
            foreach (var v in sources)
            {
                listSorceMonitor.Items.Add(String.Format("用户id:{0} , 分数:{1}",v.Key,v.Value));
                post.SetScore(v.Key, v.Value.ToString());
            }
        }

        /// <summary>
        /// 下发分数，但是在下发前 终端擦除选项必须是非勾选状态否则将有异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            post.PushScore();
        }
    }
}
