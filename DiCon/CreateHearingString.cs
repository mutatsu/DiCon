using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiCon
{
    public class CreateHearingString
    {
        private string xmlFile;
        public string XmlFile
        {
            get { return xmlFile; }
            set { xmlFile = value; }
        }
        private string txtFile;
        public string TxtFile
        {
            get { return txtFile; }
            set { txtFile = value; }
        }
        private string userid;
        public string Userid
        {
            get { return userid; }
            set { userid = value; }
        }
        private string passwd;
        public string Passwd
        {
            get { return passwd; }
            set { passwd = value; }
        }
        private string hearingSheetString;
        // ヒアリングシートの内容を格納する為に使用
        Dictionary<string, string> hsDic = new Dictionary<string, string>();
        public CreateHearingString()
        {
        }
        public string GetHearingFileName(string servicename, string version_str)
        {
            try
            {
                float f = float.Parse(version_str);
                return servicename + "_template_ver" + (Math.Floor(f)).ToString() + ".txt";
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public string Execute()
        {
            // 1.ヒアリング・シートのXMLファイルの内容を取得する
            try
            {
                ReadXML readXML = new ReadXML();
                readXML.XmlFile = xmlFile;
                readXML.setDics(hsDic);
            }
            catch (Exception e)
            {
                throw e;
            }
            // 1.1.ヒアリング・シートのフォーマット確認
            if (!hsDic.ContainsKey("service") || (!hsDic.ContainsKey("VERSION")))
            {
                throw new DiConXMLException("ヒアリング・シートのXMLファイルが正しくありません。");
            }
            // 1.2.beehiveからダウンロードするMS-Wordのテンプレートファイル名を作成
            txtFile = GetHearingFileName(hsDic["service"], hsDic["VERSION"]);

            // 2.beehiveからヒアリング・シート表示で利用する
            //   テンプレートファイルから内容のみ(string)を取得
            try
            {
                RemoteServer rs = new RemoteServer();
                rs.Userid = userid;
                rs.Passwd = passwd;
                rs.TxtFile = txtFile;
                hearingSheetString = rs.GetHearigString();
            }
            catch
            {
                throw new DiConNetworkException("beehiveに接続できません。後ほど再実行してください。");
            }
            // 3.取得した文字列を、ヒアリングシートの内容で置換する
            try
            {
                StringBuilder sb = new StringBuilder(hearingSheetString);
                foreach (KeyValuePair<string, string> kvp in hsDic)
                {
                    sb.Replace("$" + kvp.Key + "$", kvp.Value);
                    //Console.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
                }
                return sb.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
