using System;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace DiCon
{
    public class RemoteAuth
    {
        private string userid;
        private string passwd;

        private string remote_file_url;

        //コンストラクタ
        public RemoteAuth()
        {
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
        public bool Connect()
        {
            try
            {
                System.Net.HttpWebRequest webreq = (System.Net.HttpWebRequest)
                    System.Net.WebRequest.Create(remote_file_url);
                webreq.Credentials = new System.Net.NetworkCredential(userid, passwd);
                System.Net.HttpWebResponse res = (System.Net.HttpWebResponse)webreq.GetResponse();
                // Console.WriteLine("status code:{0}", res.StatusCode);
                if (res.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
                // throw e;
            }
        }
    }
}
