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
    public partial class LoginForm : Form
    {
        public string userid;
        public string passwd;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((tx_username.Text == "") || (tx_password.Text == ""))
            {
                MessageBox.Show("ユーザー名もしくはパスワードが入力されていません。",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                RemoteServer rs = new RemoteServer();
                rs.Userid = tx_username.Text;
                rs.Passwd = tx_password.Text;
                try
                {
                    if (rs.Authorize())
                    {
                        this.userid = tx_username.Text;
                        this.passwd = tx_password.Text;
                        this.Hide();
                        //this.Close(); // 認証OK。Formを閉じる。
                    }
                    else
                    {
                        MessageBox.Show("認証に失敗しました。ユーザー名およびパスワードを確認してください。",
                            "エラー",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString(),
                        "エラー",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
}
