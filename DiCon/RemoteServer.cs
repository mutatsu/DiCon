using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DiCon
{
    public class RemoteServer
    {
        static string RemoteFileURL =
            @"https://stbeehive.oracle.com/content/dav/st/OrD-TSG_JP/Public%20Documents/DiCon/tools/";
            // @"https://stbeehive.oracle.com/content/dav/st/OrD-TSG_JP/Documents/FY11/Dicon/tool";
        static string VersionStringFile = @"DiCon.version";
        static string NewVersionExeFileString = @"DiCon.exe.new";
        static string CurrentVersionExeFileString = @"DiCon.exe";
        private string userid;
        private string passwd;

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
        private string tmplFile; // Wordのテンプレートファイル
        public string TmplFile
        {
            get { return tmplFile; }
            set { tmplFile = value; }
        }
        private string docFile; // ローカルに保存するWordファイル用
        public string DocFile
        {
            get { return docFile; }
            set { docFile = value; }
        }
        private string txtFile; // textのテンプレートファイル
        public string TxtFile
        {
            get { return txtFile; }
            set { txtFile = value; }
        }

        public RemoteServer()
        {
        }

        public void GetNewExeFile()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Credentials = new NetworkCredential(userid, passwd);
                wc.DownloadFile(RemoteFileURL + '/' + CurrentVersionExeFileString, NewVersionExeFileString);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void GetTemplFile()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Credentials = new NetworkCredential(userid, passwd);
                //Console.WriteLine("userid:{0}, passwd:{1}, remote_file_url:{2}, docFile:{3}", userid, passwd, remote_file_url, docFile);
                wc.DownloadFile(RemoteFileURL +'/'+ tmplFile, docFile);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public string GetHearigString()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Credentials = new NetworkCredential(userid, passwd);
                // Console.WriteLine("userid:{0}, passwd:{1}, remote_file_url:{2}, txtFile:{3}", userid, passwd, RemoteFileURL, txtFile);
                byte[] buf = wc.DownloadData(RemoteFileURL + '/' + txtFile);

                return Encoding.UTF8.GetString(buf);
                
                //return Encoding.GetEncoding(932).GetString(buf);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string GetWebVersionString()
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Credentials = new NetworkCredential(userid, passwd);

                byte[] buf = wc.DownloadData(RemoteFileURL + '/' + VersionStringFile);

                string verStr = Encoding.UTF8.GetString(buf);
                verStr = verStr.Replace(Environment.NewLine, ""); // 改行を削除
                //             
                Regex reg = new Regex(@"\d+\.\d+"); // 1.0 とかのバージョン番号とマッチさせる
                Match m = reg.Match(verStr);
                if (m.Success == true)
                    return m.Value;

                return "";
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool VersionCheck(string versionStr)
        {
            try
            {
                WebClient wc = new WebClient();
                wc.Credentials = new NetworkCredential(userid, passwd);

                byte[] buf = wc.DownloadData(RemoteFileURL + '/' + VersionStringFile);

                string verStr =  Encoding.UTF8.GetString(buf);
                verStr = verStr.Replace(Environment.NewLine, ""); // 改行を削除
                //             
                Regex reg = new Regex(@"\d+\.\d+"); // 1.0 とかのバージョン番号とマッチさせる
                Match m = reg.Match(verStr);
                if (m.Success == true)
                {
                    try
                    {
                        float thisProgVerNum = float.Parse(versionStr);
                        float webProgVerNum = float.Parse(m.Value);
                        if (thisProgVerNum >= webProgVerNum)
                        {
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool Authorize()
        {
            try
            {
                System.Net.HttpWebRequest webreq = (System.Net.HttpWebRequest)
                    System.Net.WebRequest.Create(RemoteFileURL);
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
                //return false;
                throw e;
            }
        }
    }
}
