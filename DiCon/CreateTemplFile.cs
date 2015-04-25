using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiCon
{
    class CreateTemplFile
    {
        private Boolean templDebug = false;
        private string xmlFile; 
        public string XmlFile
        {
            get { return xmlFile; }
            set { xmlFile = value; }
        }
        private string docFile; // ローカルに保存するWordファイル
        public string DocFile
        {
            get { return docFile; }
            set { docFile = value; }
        }
        private string tmpFile; // テンプレート用Wordファイル
        public string TmpFile
        {
            get { return tmpFile; }
            set { tmpFile = value; }
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
        // ヒアリングシートの内容を格納する為に使用
        Dictionary<string, string> hsDic = new Dictionary<string, string>();
        // 文字列を置換する為に使用
        Dictionary<string, string> rkDic = new Dictionary<string, string>();
        // 不要な説明範囲を削除する為に使用
        Dictionary<string, bool> akDic = new Dictionary<string, bool>();

        public CreateTemplFile()
        {
        }
        public string GetTemplFileName(string servicename, string version_str)
        {
            try
            {
                float f = float.Parse(version_str);
                return servicename + "_template_ver" +  (Math.Floor(f)).ToString() + ".doc";
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public void Execute()
        {
            // 0.テンプレートのデバッグをおこなうかどうかを判断(beehiveから取得しない)
            if (tmpFile != "")
            {
                this.templDebug = true;
            }
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
            if (!hsDic.ContainsKey("service")||(!hsDic.ContainsKey("VERSION")))
            {
                throw new DiConXMLException("ヒアリング・シートのXMLファイルが正しくありません。");
            }
            // 1.2.beehiveからダウンロードするMS-Wordのテンプレートファイル名を作成
            if (this.templDebug == false)
            {
                tmpFile = GetTemplFileName(hsDic["service"], hsDic["VERSION"]);
            }
            docFile = @"Report.doc"; // デフォルトのレポート出力名

            // 2.ヒアリング・シートのXMLファイルを元にMS-Wordを操作するロジック作成
            try
            {
                switch (hsDic["service"])
                {
                    case "v2o":
                        V2oDB v2oDB = new V2oDB();
                        v2oDB.setDics(hsDic, rkDic, akDic);
                        break;
                    case "sizing_db":
                        SizingDB sizingDB = new SizingDB();
                        sizingDB.setDics(hsDic, rkDic, akDic);
                        break;
                    case "sizing_wls":
                        SizingWLS sizingWLS = new SizingWLS();
                        sizingWLS.setDics(hsDic, rkDic, akDic);
                        break;
                    case "perf":
                        Perf perf = new Perf();
                        perf.setDics(hsDic, rkDic, akDic);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            // 3.beehiveからMS-Wordのテンプレートファイルを取得
            if (this.templDebug == false)
            {
                try
                {
                    RemoteServer rs = new RemoteServer();
                    rs.Userid = userid;
                    rs.Passwd = passwd;
                    rs.DocFile = docFile; // local file
                    rs.TmplFile = tmpFile; // MS-Wordのテンプレートファイル名
                    rs.GetTemplFile();
                }
                catch
                {
                    throw new DiConNetworkException("beehiveに接続できません。後ほど再実行してください。");
                }
            }
            // 4.MS-Word処理
            try
            {
                String CurrentDirStr = System.IO.Directory.GetCurrentDirectory();
                Console.Out.WriteLine(CurrentDirStr);
                EditWord editWord = new EditWord();
                editWord.DocFile = CurrentDirStr + "\\" + docFile;
                if (this.templDebug)
                    editWord.TmpFile = tmpFile; // テンプレートのデバッグ
                else
                    editWord.TmpFile = CurrentDirStr + "\\" + docFile;
                editWord.Edit(rkDic, akDic);
            }
            catch(Exception e)
            {
                throw e;
                //throw new DiConMSWrodException("MS-Wordの処理に失敗しました。ツール・メンテナンス担当者に連絡してください。");
            }
        }

    }
}
