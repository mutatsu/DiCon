using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DiCon
{
    public partial class DiConForm : Form
    {
        string versionString = @"1.7"; // 2014/09/12 Update
        string userid;
        string passwd;
        string templFile = "";
        public DiConForm()
        {
            InitializeComponent();
        }
        public DiConForm(string userid, string passwd)
        {
            this.userid = userid;
            this.passwd = passwd;
            InitializeComponent();

            // メニュー
            this.SetTemplFileToolStripMenuItem.Click += new EventHandler(SetTemplFileToolStripMenuItem_Click);
            this.ClearTemplFileToolStripMenuItem.Click += new EventHandler(ClearTemplFileToolStripMenuItem_Click);
            this.CopyHSToolStripMenuItem.Click += new EventHandler(CopyHSToolStripMenuItem_Click);
            this.CopyWLSSizingToolStripMenuItem.Click += new EventHandler(CopyWLSSizingToolStripMenuItem_Click);
            this.ClearHSToolStripMenuItem.Click += new EventHandler(ClearHSToolStripMenuItem_Click);
            this.ClearWLSSizingToolStripMenuItem.Click += new EventHandler(ClearWLSSizingToolStripMenuItem_Click);
            this.VersionInfoToolStripMenuItem.Click += new EventHandler(VersionInfoToolStripMenuItem_Click);
            // テキスト・エリア
            this.rep_text.DragEnter += new DragEventHandler(rep_text_DragEnter);
            this.rep_text.DragDrop += new DragEventHandler(rep_text_DragDrop);
            this.rep_text.DragDrop += new DragEventHandler(hs_text_DragDrop); // レポート作成と同時にヒアリング・シートのテキスト化も実施

            this.szwls_text.DragEnter += new DragEventHandler(szwls_text_DragEnter);
            this.szwls_text.DragDrop += new DragEventHandler(szwls_text_DragDrop);

            this.hs_text.DragEnter += new DragEventHandler(hs_text_DragEnter);
            this.hs_text.DragDrop += new DragEventHandler(hs_text_DragDrop);

            // 初期値
            this.tb_szwls_base_cint.Text = "29.088";

            try
            {
                RemoteServer rs = new RemoteServer();
                rs.Userid = userid;
                rs.Passwd = passwd;
                string webVerStr = rs.GetWebVersionString();
                if (webVerStr == "")
                {
                    MessageBox.Show("beehive上のDiCon.exeプログラムのバージョン番号が取得できませんでした。管理者に連絡してください。",
                        "注意",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);

                }
                try
                {
                    float thisProgVerNum = float.Parse(versionString);
                    float webProgVerNum = float.Parse(webVerStr);
                    if (thisProgVerNum < webProgVerNum)
                    {
                        MessageBox.Show("DiCon.exeプログラムのバージョンが新しくなりました。再ダウンロードしてください。\n"
                            + "(バージョン番号： " + webVerStr + ")",
                                 "注意",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Exclamation);

                        DialogResult result = MessageBox.Show("新しいDiCon.exeプログラムをダウンロードしますか？\n"
                            + "DiCon.exe.new としてダウンロードしますのでファイル名をリネームしてください",
                                 "質問",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            rs.GetNewExeFile();
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(),
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("beehiveに接続できません。後ほど再実行してください。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                // throw new DiConNetworkException("beehiveに接続できません。後ほど再実行してください。");
            }
        }
        // メニュークリック
        private void SetTemplFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Word 97-2003文書(*.doc)|*.doc|すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.templFile = ofd.FileName;
            }
        }
        private void ClearTemplFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.templFile = ""; // Clear
        }
        private void CopyHSToolStripMenuItem_Click(object s, EventArgs e)
        {
            Clipboard.SetText(this.hs_text.Text);
        }
        private void CopyWLSSizingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.szwls_text.Text);
        }
        private void ClearHSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.hs_text.Text = ""; // clear
        }
        private void ClearWLSSizingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.szwls_text.Text = ""; // clear
        }
        private void VersionInfoToolStripMenuItem_Click(object s, EventArgs e)
        {
            MessageBox.Show(
                "バージョン情報： " + this.versionString,
                "バージョン情報",
                MessageBoxButtons.OK,
                MessageBoxIcon.None
                );
        }

        // テキスト・エリア操作
        private void rep_text_DragEnter(object s, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void rep_text_DragDrop(object s, DragEventArgs e)
        {
            rep_text.Text = ""; // clear
            foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                // rep_text.Text = rep_text.Text + fileName;
                try
                {
                    CreateTemplFile ctf = new CreateTemplFile();
                    ctf.Userid = userid;
                    ctf.Passwd = passwd;
                    ctf.XmlFile = fileName;
                    ctf.TmpFile = this.templFile;
                    ctf.Execute();
                    rep_text.Text = rep_text.Text + "DiConレポートのテンプレートが作成されました。";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(),
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void szwls_text_DragEnter(object s, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void szwls_text_DragDrop(object s, DragEventArgs e)
        {
            foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                rep_text.Text = rep_text.Text + fileName;
            }
        }

        private void hs_text_DragEnter(object s, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void hs_text_DragDrop(object s, DragEventArgs e)
        {
            hs_text.Text = ""; // clear
            foreach (string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
            {
                // hs_text.Text = hs_text.Text + fileName;
                try
                {
                    CreateHearingString chs = new CreateHearingString();
                    chs.Userid = userid;
                    chs.Passwd = passwd;
                    chs.XmlFile = fileName;
                    string tmpstr = chs.Execute();
                    hs_text.Text = hs_text.Text + tmpstr;
                    // Console.WriteLine(tmpstr);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(),
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void szwls_btn_Click(object sender, EventArgs e)
        {
            // 関連textboxの入力チェック
            if (
                (tb_szwls_base_cint.Text == "") ||
                (tb_szwls_target_cint_peak.Text == "") ||
                (tb_szwls_target_cint_base.Text == "") ||
                (tb_szwls_cores_per_cpu.Text == "") ||
                (tb_szwls_peak_tpm.Text == "") ||
                (tb_szwls_complexity.Text == "") ||
                (tb_szwls_cpu_utilization.Text == "")
                )
            {
                MessageBox.Show("全てのテキスト・ボックスに入力してください。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                double base_cint = 0;
                double target_cint_peak = 0;
                double target_cint_base = 0;
                double cores_per_cpu = 0;
                double peak_tpm = 0;
                double complexity = 0;
                double cpu_utilization = 0;
                try
                {
                    base_cint = double.Parse(tb_szwls_base_cint.Text);
                    target_cint_peak = double.Parse(tb_szwls_target_cint_peak.Text);
                    target_cint_base = double.Parse(tb_szwls_target_cint_base.Text);
                    cores_per_cpu = double.Parse(tb_szwls_cores_per_cpu.Text);
                    peak_tpm = double.Parse(tb_szwls_peak_tpm.Text);
                    complexity = double.Parse(tb_szwls_complexity.Text);
                    cpu_utilization = double.Parse(tb_szwls_cpu_utilization.Text);
                }
                catch
                {
                    MessageBox.Show("数字、小数点以外の文字が入力されています",
                        "エラー",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                double cpus;
                double cores;
                SizingWLS szwls = new SizingWLS();
                cpus = szwls.CalcCPUs(
                    base_cint,
                    target_cint_peak,
                    target_cint_base,
                    peak_tpm,
                    complexity,
                    cpu_utilization
                    );
                cores = cpus * cores_per_cpu;
                string str =
                    "・基準サーバのCINTの値：                     " + base_cint.ToString() + "\r\n" +
                    "・利用予定サーバのCINTの値(Peak,Base,平均)：  " + 
                        target_cint_peak.ToString() + " " +
                        target_cint_base.ToString() + " " +
                        ((target_cint_peak + target_cint_base) / 2).ToString() + "\r\n" +
                    "・ピーク時のトランザクション数（1分間あたり）：" + peak_tpm.ToString() + "\r\n" +
                    "・アプリケーションの複雑度（10-100）：        " + complexity.ToString() + "\r\n" +
                    "・ピーク時のCPU使用率：                      " + cpu_utilization.ToString() + "% \r\n" +
                    "・利用予定サーバのCPUあたりのCore数：         " + cores_per_cpu.ToString() + "\r\n" +
                    "  - 必要CPU数：            " + cpus.ToString() + " " + "\r\n" +
                    "  - 必要Core数(切り上げ)： " + (Math.Ceiling(cores)).ToString() + " " + "\r\n" +
                    "  - 必要CPU数(切り上げ)：  " + (Math.Ceiling(cores / cores_per_cpu)).ToString() + "\r\n" +
                    "\r\n";
                szwls_text.Text = szwls_text.Text + str;

            }
        }
    }
}
