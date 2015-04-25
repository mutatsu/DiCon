using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DiCon
{
    public class ReadXML
    {
        // メンバ
        private string xmlFile;
        private string key;
        private string val;
        // コンストラクタ
        public ReadXML()
        {
        }

        // プロパティ

        // 読み込むXmlファイル用
        public string XmlFile
        {
            get { return xmlFile; }
            set { xmlFile = value; }
        }

        public void setDics(Dictionary<string, string> dict)
        {
            XmlReader reader = null;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;

            try
            {
                reader = XmlReader.Create(XmlFile, settings);
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            key = reader.Name;
                            switch (key)
                            {
                                case "F": break;
                                case "P1": break;
                                case "P2": break;
                                case "P3": break;
                                default:
                                    dict.Add(key, "");
                                    break;
                            }
                            break;
                        case XmlNodeType.Text:
                            val = reader.Value;
                            switch (key)
                            {
                                case "F": break;
                                case "P1": break;
                                case "P2": break;
                                case "P3": break;
                                default:
                                    dict[key] = val;
                                    break;
                            }
                            break;
                        case XmlNodeType.EndElement:
                            break;
                    }
                }
            }
            catch
            {
                // エラーを表示
                // Console.WriteLine("Error : {0}", e.Message);
                throw new DiConXMLException("XMLファイルが不正です");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
    }
}
