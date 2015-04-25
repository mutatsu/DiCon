using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiCon
{
    public class SizingDB
    {
        public SizingDB()
        {
        }
        public void setDics(
            Dictionary<string, string> hsDic,
            Dictionary<string, string> rkDic,
            Dictionary<string, bool> akDic
            )
        {
            // rkDicのセットのみ
            // akDicにはなにもセットしない
            foreach(KeyValuePair<string, string> pair in hsDic)
            {
                rkDic.Add(pair.Key, pair.Value);
            }

            float versionNumber = float.Parse(rkDic["VERSION"]);

            if (rkDic["OPT_RAC"] == "1")
                rkDic["OPT_RAC"] = "Real Application Clusters";
            else
                rkDic["OPT_RAC"] = "";


            if (rkDic["OPT_PARTITIONING"] == "1")
                rkDic["OPT_PARTITIONING"] = "Partitioning Option";
            else
                rkDic["OPT_PARTITIONING"] = "";

            if (rkDic["OPT_DIAG"] == "1")
                rkDic["OPT_DIAG"] = "Diagnostics Pack";
            else
                rkDic["OPT_DIAG"] = "";

            if (rkDic["OPT_TUNING"] == "1")
                rkDic["OPT_TUNING"] = "Tuning Pack";
            else
                rkDic["OPT_TUNING"] = "";

            if (rkDic["OPT_COMPRESSION"] == "1")
                rkDic["OPT_COMPRESSION"] = "Advanced Compression";
            else
                rkDic["OPT_COMPRESSION"] = "";

            if (versionNumber >= 2.4) // Hearing Sheetに追加
            {
                if (rkDic["OPT_RACOne"] == "1")
                    rkDic["OPT_RACOne"] = "RAC One Node";
                else
                    rkDic["OPT_RACOne"] = "";

                if (rkDic["OPT_SECURITY"] == "1")
                    rkDic["OPT_SECURITY"] = "Advanced Security";
                else
                    rkDic["OPT_SECURITY"] = "";

            }
            
            if (rkDic["OPT_ETC"] == "1")
                if (rkDic["OPT_BIKOU"] == "")
                    rkDic["OPT_ETC"] = "その他";
                else
                    rkDic["OPT_ETC"] = "その他(" + rkDic["OPT_BIKOU"] + ")";
            else
                rkDic["OPT_ETC"] = "";


            if (rkDic["EnqueteSystemEnv1"] == "1")
                rkDic["EnqueteSystemEnv1"] = "安定運用（ダウンタイム削減）";
            else
                rkDic["EnqueteSystemEnv1"] = "";

            if (rkDic["EnqueteSystemEnv2"] == "1")
                rkDic["EnqueteSystemEnv2"] = "性能管理";
            else
                rkDic["EnqueteSystemEnv2"] = "";

            if (rkDic["EnqueteSystemEnv3"] == "1")
                rkDic["EnqueteSystemEnv3"] = "セキュリティ";
            else
                rkDic["EnqueteSystemEnv3"] = "";

            if (rkDic["EnqueteSystemEnv4"] == "1")
                rkDic["EnqueteSystemEnv4"] = "データ量の増加(ストレージ管理)";
            else
                rkDic["EnqueteSystemEnv4"] = "";

            if (rkDic["EnqueteSystemEnv5"] == "1")
                rkDic["EnqueteSystemEnv5"] = "運用コスト削減";
            else
                rkDic["EnqueteSystemEnv5"] = "";

            if (rkDic["EnqueteSystemEnv6"] == "1")
                rkDic["EnqueteSystemEnv6"] = "特に無し";
            else
                rkDic["EnqueteSystemEnv6"] = "";

            if (rkDic["EnqueteSystemEnv7"] == "1")
                if (rkDic["EnquateSystemEtc"] == "")
                    rkDic["EnqueteSystemEnv7"] = "その他";
                else
                    rkDic["EnqueteSystemEnv7"] = "その他(" + rkDic["EnquateSystemEtc"] + ")";
            else
                rkDic["EnqueteSystemEnv7"] = "";
        }
    }
}
