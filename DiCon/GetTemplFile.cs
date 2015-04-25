using System;
using System.Net;

namespace DiCon
{
    public class GetTemplFile
    {
        //メンバ
        private string docFile;

        private string userid;
        private string passwd;

        private string remote_file_url;

        //コンストラクタ
        public GetTemplFile()
        {
        }

        // ローカルに保存するWordファイル用
        public string DocFile
        {
            get { return docFile; }
            set { docFile = value; }
        }
        public string Userid
        {
            get { return userid; }
            set { userid = value; }
        }
        public string Passwd
        {
            get { return passwd; }
            set { passwd = value; }
        }
        public string Remote_file_url
        {
            get { return remote_file_url; }
            set { remote_file_url = value; }
        }


        public void Download()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Credentials = new NetworkCredential(userid, passwd);
                //Console.WriteLine("userid:{0}, passwd:{1}, remote_file_url:{2}, docFile:{3}", userid, passwd, remote_file_url, docFile);
                wc.DownloadFile(remote_file_url, docFile);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
