using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DiCon
{
    static class Program
    {

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            RemoteServer rs = new RemoteServer();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();

            Application.Run(new DiConForm(loginForm.userid, loginForm.passwd));
        }
    }
}
