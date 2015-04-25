using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiCon
{
    class V2oCheck
    {
        Dictionary<String, Boolean> UPGRADE_CHECKLIST = new Dictionary<String, Boolean>
        {
            {"EqualPlatform",  false},
            {"EqualWordsize",  false},
            {"HPUXPA2HPUXIT",  false},
            {"DirectUpgrade",  false},
            {"ClntNeedUpgAS",  false},
            {"ClntNeedUpgClnt",false},
            {"ClntConnASES",   false}, // 2013/08/14 add
            {"ClntConnASWAS",  false}, // 2013/08/14 add
            {"ClntConnClntES", false}, // 2013/08/14 add
            {"ClntConnClntWAS",false}, // 2013/08/14 add
            {"PlatformFuzzy",  false}, // プラットフォームが'不明'や'一覧になし'の場合
            {"OraVerFuzzy",    false}, // Oracle Databaseのバージョンが'不明'や'一覧になし'の場合
            {"OraASVerFuzzy",  false}, // ASで利用しているOracle Clientのバージョンが'不明'や'一覧になし'の場合
            {"OraClntVerFussy",false}, // Clientで利用しているOracle Clientのバージョンが'不明'や'一覧になし'の場合
        };

        // WORD上の範囲指定のキーワード。
        // 実際のWORDのテンプレートには、'_START' '_END'で囲まれていることを前提とする

        public V2oCheck()
        {
        }
        public void UpgradeEnvCheck(Dictionary<String, String> HEARING_SHEET)
        {
            V2oCheckDB ckdb = new V2oCheckDB();
            // ------------------------------------
            // 移行前後でプラットフォームが同じであるか？
            // 移行前後でプラットフォームのWordsizeが同じであるか？
            // ------------------------------------
            UPGRADE_CHECKLIST["EqualPlatform"] = ckdb.CheckSameSystem(HEARING_SHEET["NewPlatform"], HEARING_SHEET["CurrentPlatform"]);
            if (UPGRADE_CHECKLIST["EqualPlatform"] == true)
                UPGRADE_CHECKLIST["EqualWordsize"] = ckdb.CheckSamePlatformSameWordsize(HEARING_SHEET["NewPlatform"], HEARING_SHEET["CurrentPlatform"]); 
            else
            {
                UPGRADE_CHECKLIST["PlatformFuzzy"] = true;
                UPGRADE_CHECKLIST["EqualPlatform"] = false;
            }
            // ------------------------------------
            // 移行前後でHP-UXであれば、PA-RISCからItaniumへの移行かどうか？
            // ------------------------------------
            UPGRADE_CHECKLIST["HPUXPA2HPUXIT"] = ckdb.CheckHPUX_PA2IT(HEARING_SHEET["NewPlatform"], HEARING_SHEET["CurrentPlatform"]);

            String NewOracleVersion = ckdb.GetVersionNumberString(HEARING_SHEET["NewOracleVersion"]);
            String CurrentOracleVersion = ckdb.GetVersionNumberString(HEARING_SHEET["CurrentOracleVersion"]);
            // UPGINF["NewOracleVersion"],UPGINF["CurrentOracleVersion"]が"不明"や"一覧にない"の場合は "" が入る
            if ((NewOracleVersion == "") || (CurrentOracleVersion == ""))
            {
                UPGRADE_CHECKLIST["OraVerFuzzy"] = true;
                UPGRADE_CHECKLIST["OraASVerFuzzy"] = true;
                UPGRADE_CHECKLIST["OraClntVerFussy"] = true;
            }
            else
            {
                // ------------------------------------
                // 移行前後で同一種類のプラットフォームであれば、直接Oracle Databaseのアップグレードが可能かどうか
                // ------------------------------------
                UPGRADE_CHECKLIST["DirectUpgrade"] = ckdb.CheckDirectUpgrade(NewOracleVersion, CurrentOracleVersion);
                // ------------------------------------
                // Oracle Databaseはアップグレードしても、Clientはそのまま利用しつづけられるか
                // Client側、AS側で両方ともTrueの場合、Trueにする。
                // ------------------------------------
                if (HEARING_SHEET["DevClntOracleVer"] == "不明" || HEARING_SHEET["DevClntOracleVer"] == "一覧になし" || HEARING_SHEET["DevClntOracleVer"] == "")
                {
                    UPGRADE_CHECKLIST["OraClntVerFussy"] = true;
                }
                else
                {
                    String DevClntOracleVer = ckdb.GetVersionNumberString(HEARING_SHEET["DevClntOracleVer"]);
                    UPGRADE_CHECKLIST["ClntNeedUpgClnt"] = ckdb.CheckConnect(NewOracleVersion, DevClntOracleVer);
                    UPGRADE_CHECKLIST["ClntConnClntES"] = ckdb.CheckConnectStatus(NewOracleVersion, DevClntOracleVer, "ES");
                    UPGRADE_CHECKLIST["ClntConnClntWAS"] = ckdb.CheckConnectStatus(NewOracleVersion, DevClntOracleVer, "WAS");
                }

                if (HEARING_SHEET["DevASOracleVer"] == "不明" || HEARING_SHEET["DevASOracleVer"] == "一覧になし" || HEARING_SHEET["DevASOracleVer"] == "")
                {
                    UPGRADE_CHECKLIST["OraASVerFuzzy"] = true;
                }
                else
                {
                    String DevASOracleVer = ckdb.GetVersionNumberString(HEARING_SHEET["DevASOracleVer"]);
                    UPGRADE_CHECKLIST["ClntNeedUpgAS"] = ckdb.CheckConnect(NewOracleVersion, DevASOracleVer);
                    UPGRADE_CHECKLIST["ClntConnASES"] = ckdb.CheckConnectStatus(NewOracleVersion, DevASOracleVer, "ES");
                    UPGRADE_CHECKLIST["ClntConnASWAS"] = ckdb.CheckConnectStatus(NewOracleVersion, DevASOracleVer, "WAS");
                }
            }
        }
        public void ReplaceKeyword_SetStr(Dictionary<String, String> HEARING_SHEET, Dictionary<String, String> REPLACE_KEYWORD)
        {
            foreach (KeyValuePair<string, string> kvp in HEARING_SHEET)
            {
                try
                {
                    REPLACE_KEYWORD.Add(kvp.Key, kvp.Value);
                                    }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public void ReplaceKeyword_CodeToStr(Dictionary<String, String> HEARING_SHEET, Dictionary<String, String> REPLACE_KEYWORD)
        {
            foreach (KeyValuePair<string, string> kvp in HEARING_SHEET)
            {
                try
                {
                    switch (kvp.Key)
                    {
                        case "CurrentRACUse":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"Real Application Clusters";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "NewRACUse":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"Real Application Clusters";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "AppCS":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"クライアント/サーバ";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "AppWeb":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"Webシステム";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "AppBT":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"サーバ側バッチ処理";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "AppTPM":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"TPモニターを利用した3階層システム";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "DevServLangPLSQL":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"PL/SQL";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "DevServLangProC":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"Pro*C";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "DevServLangProCOBOL":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"Pro*COBOL";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "DevClntLangVB":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"VB";
                                if (REPLACE_KEYWORD["DevClntLangVBVer"] != "")
                                {
                                    REPLACE_KEYWORD[kvp.Key] = REPLACE_KEYWORD[kvp.Key] + "(" + REPLACE_KEYWORD["DevClntLangVBVer"] + ")";
                                }
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "DevClntLangVC":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"VC++";
                                if (REPLACE_KEYWORD["DevClntLangVCVer"] != "")
                                {
                                    REPLACE_KEYWORD[kvp.Key] = REPLACE_KEYWORD[kvp.Key] + "(" + REPLACE_KEYWORD["DevClntLangVCVer"] + ")";
                                }
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "DevClntLangDNET":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"VB.NET,C.NET,C#";
                                if (REPLACE_KEYWORD["DevClntLangDNETVer"] != "")
                                {
                                    REPLACE_KEYWORD[kvp.Key] = REPLACE_KEYWORD[kvp.Key] + "(" + REPLACE_KEYWORD["DevClntLangDNETVer"] + ")";
                                }
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "DevClntLangForms":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"Oracle Developer (Forms/Reports)";
                                if (REPLACE_KEYWORD["DevClntLangFormsVer"] != "")
                                {
                                    REPLACE_KEYWORD[kvp.Key] = REPLACE_KEYWORD[kvp.Key] + "(" + REPLACE_KEYWORD["DevClntLangFormsVer"] + ")";
                                }
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "DevClntLangJava":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"Java";
                                if (REPLACE_KEYWORD["DevClntLangJavaVer"] != "")
                                {
                                    REPLACE_KEYWORD[kvp.Key] = REPLACE_KEYWORD[kvp.Key] + "(" + REPLACE_KEYWORD["DevClntLangJavaVer"] + ")";
                                }
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }

                            break;
                        case "DevClntLangAccess":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"MS Access";
                                if (REPLACE_KEYWORD["DevClntLangAccessVer"] != "")
                                {
                                    REPLACE_KEYWORD[kvp.Key] = REPLACE_KEYWORD[kvp.Key] + "(" + REPLACE_KEYWORD["DevClntLangAccessVer"] + ")";
                                }
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "DevClntLangPB":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"PowerBuilder";
                                if (REPLACE_KEYWORD["DevClntLangPBVer"] != "")
                                {
                                    REPLACE_KEYWORD[kvp.Key] = REPLACE_KEYWORD[kvp.Key] + "(" + REPLACE_KEYWORD["DevClntLangPBVer"] + ")";
                                }
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "DevClntLangDelphi":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"Delphi";
                                if (REPLACE_KEYWORD["DevClntLangDelphiVer"] != "")
                                {
                                    REPLACE_KEYWORD[kvp.Key] = REPLACE_KEYWORD[kvp.Key] + "(" + REPLACE_KEYWORD["DevClntLangDelphiVer"] + ")";
                                }
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "DevClntLangEtc":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"その他";
                                if (REPLACE_KEYWORD["DevClntLangEtcDetail"] != "")
                                {
                                    REPLACE_KEYWORD[kvp.Key] = REPLACE_KEYWORD[kvp.Key] + "(" + REPLACE_KEYWORD["DevClntLangEtcDetail"] + ")";
                                }
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "EnqueteSystemEnv1":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"安定運用（ダウンタイム削減）";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "EnqueteSystemEnv2":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"性能管理";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "EnqueteSystemEnv3":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"セキュリティ";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "EnqueteSystemEnv4":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"データ量の増加(ストレージ管理)";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "EnqueteSystemEnv5":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"運用コスト削減";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "EnqueteSystemEnv6":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"特に無し";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        case "EnqueteSystemEnv7":
                            if (kvp.Value == "1")
                            {
                                REPLACE_KEYWORD[kvp.Key] = @"その他";
                            }
                            else
                            {
                                REPLACE_KEYWORD[kvp.Key] = "";
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
        public string joinString(string baseStr, string addStr,string token)
        {
            if (addStr == "")
            {
                return baseStr;
            }
            if (baseStr == "")
            {
                return addStr;
            }

            return baseStr + token + addStr;
            
        }

        public void ReplaceKeywordGenerate(Dictionary<String, String> HEARING_SHEET, Dictionary<String, String> REPLACE_KEYWORD)
        {
            // 入力としてもらった情報のうち、加工しないといけない情報を加工する
            // 例えば、ヒアリングシート上でチェックボックスで入力してもらったアプリケーションの情報など
            //
            // 引数でもらうHEARING_SHEETは参照のみ
            // REPLACE_KEYWORDは値を設定する
            //

            try
            {
                // ヒアリングシートに記入されている文字列（上記のコード以外）を転記
                ReplaceKeyword_SetStr(HEARING_SHEET, REPLACE_KEYWORD);

                // ヒアリングシートにコード"0","1"で入力されているもののうち、
                // "1"のものに適切な文字列を設定する
                ReplaceKeyword_CodeToStr(HEARING_SHEET, REPLACE_KEYWORD);

                // ------------------------------------
                // 以下、ヒアリングシートには項目がないものを加工して作成
                // ------------------------------------
                REPLACE_KEYWORD["VerUp_Date"] = REPLACE_KEYWORD["VerUp_Year"] + @"年" + REPLACE_KEYWORD["VerUp_Month"] + @"月頃";

                // ------------------------------------
                // 1.既存 Oracle Database のご利用環境
                // ------------------------------------
                if (REPLACE_KEYWORD["CurrentPlatformEtc"] != "")
                {
                    if (REPLACE_KEYWORD["CurrentPlatform"] == @"不明" || REPLACE_KEYWORD["CurrentPlatform"] == @"一覧になし")
                    {
                        REPLACE_KEYWORD["CurrentPlatform"] = REPLACE_KEYWORD["CurrentPlatformEtc"];
                    }
                    else
                    {
                        REPLACE_KEYWORD["CurrentPlatform"] = REPLACE_KEYWORD["CurrentPlatform"] + " " + REPLACE_KEYWORD["CurrentPlatformEtc"];
                    }
                }

                if (REPLACE_KEYWORD["CurrentOracleVersionEtc"] != "")
                {
                    if (REPLACE_KEYWORD["CurrentOracleVersion"] == @"不明" || REPLACE_KEYWORD["CurrentOracleVersion"] == @"一覧になし")
                    {
                        REPLACE_KEYWORD["CurrentOracleVersion"] = REPLACE_KEYWORD["CurrentOracleVersionEtc"];
                    }
                    else
                    {
                        REPLACE_KEYWORD["CurrentOracleVersion"] = REPLACE_KEYWORD["CurrentOracleVersion"] + " " + REPLACE_KEYWORD["CurrentOracleVersionEtc"];
                    }
                }


                if (REPLACE_KEYWORD["CurrentRACUse"] != "")
                {
                    if (REPLACE_KEYWORD["CurrentOracleOptions"] != "")
                    {
                        REPLACE_KEYWORD["CurrentOracleOptions"] = REPLACE_KEYWORD["CurrentOracleOptions"] + @"、" + REPLACE_KEYWORD["CurrentRACUse"];
                    }
                    else
                    {
                        REPLACE_KEYWORD["CurrentOracleOptions"] = REPLACE_KEYWORD["CurrentRACUse"];
                    }
                }

                // ------------------------------------
                // 2.新規 Oracle Database のご利用予定環境
                // ------------------------------------


                if (REPLACE_KEYWORD["NewPlatformEtc"] != "")
                {
                    if (REPLACE_KEYWORD["NewPlatform"] == @"不明" || REPLACE_KEYWORD["NewPlatform"] == @"一覧になし")
                    {
                        REPLACE_KEYWORD["NewPlatform"] = REPLACE_KEYWORD["NewPlatformEtc"];
                    }
                    else
                    {
                        REPLACE_KEYWORD["NewPlatform"] = REPLACE_KEYWORD["NewPlatform"] + " " + REPLACE_KEYWORD["NewPlatformEtc"];
                    }
                }

                if (REPLACE_KEYWORD["NewOracleVersionEtc"] != "")
                {
                    if (REPLACE_KEYWORD["NewOracleVersion"] == @"不明" || REPLACE_KEYWORD["NewOracleVersion"] == @"一覧になし")
                    {
                        REPLACE_KEYWORD["NewOracleVersion"] = REPLACE_KEYWORD["NewOracleVersionEtc"];
                    }
                    else
                    {
                        REPLACE_KEYWORD["NewOracleVersion"] = REPLACE_KEYWORD["NewOracleVersion"] + " " + REPLACE_KEYWORD["NewOracleVersionEtc"];
                    }
                }

                if (REPLACE_KEYWORD["NewRACUse"] != "")
                {
                    if (REPLACE_KEYWORD["NewOracleOptions"] == "")
                    {
                        REPLACE_KEYWORD["NewOracleOptions"] = HEARING_SHEET["NewOracleOptions"] + @"、" + REPLACE_KEYWORD["NewRACUse"];
                    }
                    else
                    {
                        REPLACE_KEYWORD["NewOracleOptions"] = REPLACE_KEYWORD["NewRACUse"];
                    }
                }

                // ------------------------------------
                // 3.アプリケーション
                // ------------------------------------
                REPLACE_KEYWORD["AppForm"] = "";
                REPLACE_KEYWORD["AppForm"] = joinString(REPLACE_KEYWORD["AppForm"], REPLACE_KEYWORD["AppCS"], @"、");
                REPLACE_KEYWORD["AppForm"] = joinString(REPLACE_KEYWORD["AppForm"], REPLACE_KEYWORD["AppCS"], @"、");
                REPLACE_KEYWORD["AppForm"] = joinString(REPLACE_KEYWORD["AppForm"], REPLACE_KEYWORD["AppWeb"], @"、");
                REPLACE_KEYWORD["AppForm"] = joinString(REPLACE_KEYWORD["AppForm"], REPLACE_KEYWORD["AppBT"], @"、");
                REPLACE_KEYWORD["AppForm"] = joinString(REPLACE_KEYWORD["AppForm"], REPLACE_KEYWORD["AppTPM"], @"、");

                // ------------------------------------
                // 4.既存アプリケーション開発環境（サーバ側）
                // ------------------------------------
                REPLACE_KEYWORD["DevServLang"] = "";
                REPLACE_KEYWORD["DevServLang"] = joinString(REPLACE_KEYWORD["DevServLang"], REPLACE_KEYWORD["DevServLangPLSQL"], @"、");
                REPLACE_KEYWORD["DevServLang"] = joinString(REPLACE_KEYWORD["DevServLang"], REPLACE_KEYWORD["DevServLangProC"], @"、");
                REPLACE_KEYWORD["DevServLang"] = joinString(REPLACE_KEYWORD["DevServLang"], REPLACE_KEYWORD["DevServLangProCOBOL"], @"、");
                REPLACE_KEYWORD["DevServLang"] = joinString(REPLACE_KEYWORD["DevServLang"], REPLACE_KEYWORD["AppDevServLangOracle"], @"、");
                if (REPLACE_KEYWORD["AppDevServUseTPM"] != @"利用せず")
                {
                    REPLACE_KEYWORD["DevServLang"] = joinString(REPLACE_KEYWORD["DevServLang"], REPLACE_KEYWORD["AppDevServUseTPM"], @"、");
                }
                // ------------------------------------
                // 5.既存アプリケーション開発環境（クライアント側）
                // ------------------------------------
                REPLACE_KEYWORD["DevClntLang"] = "";
                REPLACE_KEYWORD["DevClntLang"] = joinString(REPLACE_KEYWORD["DevClntLang"], REPLACE_KEYWORD["DevClntLangVB"], @"、");
                REPLACE_KEYWORD["DevClntLang"] = joinString(REPLACE_KEYWORD["DevClntLang"], REPLACE_KEYWORD["DevClntLangVC"], @"、");
                REPLACE_KEYWORD["DevClntLang"] = joinString(REPLACE_KEYWORD["DevClntLang"], REPLACE_KEYWORD["DevClntLangDNET"], @"、");
                REPLACE_KEYWORD["DevClntLang"] = joinString(REPLACE_KEYWORD["DevClntLang"], REPLACE_KEYWORD["DevClntLangForms"], @"、");
                REPLACE_KEYWORD["DevClntLang"] = joinString(REPLACE_KEYWORD["DevClntLang"], REPLACE_KEYWORD["DevClntLangJava"], @"、");
                REPLACE_KEYWORD["DevClntLang"] = joinString(REPLACE_KEYWORD["DevClntLang"], REPLACE_KEYWORD["DevClntLangAccess"], @"、");
                REPLACE_KEYWORD["DevClntLang"] = joinString(REPLACE_KEYWORD["DevClntLang"], REPLACE_KEYWORD["DevClntLangPB"], @"、");
                REPLACE_KEYWORD["DevClntLang"] = joinString(REPLACE_KEYWORD["DevClntLang"], REPLACE_KEYWORD["DevClntLangDelphi"], @"、");
                REPLACE_KEYWORD["DevClntLang"] = joinString(REPLACE_KEYWORD["DevClntLang"], REPLACE_KEYWORD["DevClntLangEtc"], @"、");

                // ------------------------------------
                if (REPLACE_KEYWORD["DevClntMW"] == @"その他")
                {
                    if (REPLACE_KEYWORD["DevClntMWEtc"] != "")
                    {
                        REPLACE_KEYWORD["DevClntMW"] = REPLACE_KEYWORD["DevClntMWEtc"];
                    }
                }
                else
                {
                    if (REPLACE_KEYWORD["DevClntMWVer"] != "")
                    {
                        REPLACE_KEYWORD["DevClntMW"] = REPLACE_KEYWORD["DevClntMW"] + "(" + REPLACE_KEYWORD["DevClntMWVer"] + ")";
                    }
                    if (REPLACE_KEYWORD["DevClntMWEtc"] != "")
                    {
                        REPLACE_KEYWORD["DevClntMW"] = REPLACE_KEYWORD["DevClntMW"] + "、" + REPLACE_KEYWORD["DevClntMWEtc"];
                    }
                }

                // ------------------------------------
                // 6.既存アプリケーション・サーバ環境（Web アプリケーション）
                // ------------------------------------
                if (REPLACE_KEYWORD["DevAS"] == @"その他")
                {
                    if (REPLACE_KEYWORD["DevASEtc"] != "")
                    {
                        REPLACE_KEYWORD["DevAS"] = REPLACE_KEYWORD["DevASEtc"];
                    }
                }
                else
                {
                    if (REPLACE_KEYWORD["DevASVer"] != "")
                    {
                        REPLACE_KEYWORD["DevAS"] = REPLACE_KEYWORD["DevAS"] + "(" + REPLACE_KEYWORD["DevASVer"] + ")";
                    }

                }    
                // ------------------------------------
                if (HEARING_SHEET["DevASLangVer"] != "")
                {
                    REPLACE_KEYWORD["DevASLang"] = REPLACE_KEYWORD["DevASLang"] + "(" + REPLACE_KEYWORD["DevASLangVer"] + ")";
                }
                // ------------------------------------
                if (REPLACE_KEYWORD["DevASMWVer"] != "")
                {
                    REPLACE_KEYWORD["DevASMW"] = REPLACE_KEYWORD["DevASMW"] + "(" + REPLACE_KEYWORD["DevASMWVer"] + ")";
                }
                // ------------------------------------
                // 7.バージョンアップの動機
                // 8.バージョンアップ時の懸念点
                // ------------------------------------
                // 9.現状のデータベース運用環境について
                // ------------------------------------
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void AreacutKeywordGenerate(Dictionary<String, String> HEARING_SHEET, Dictionary<String, Boolean> AREACUT_KEYWORD)
        {
            // 
            // AREACUT_KEYWORD は WORD上の範囲指定のキーワード。
            // 実際のWORDのテンプレートには、"_START" "_END"で囲まれていることを前提とする
            //
            // 引数でもらうHEARING_SHEETは参照のみ
            // AREACUT_KEYWORDは値を設定する
            //
            // Dictionary<String, Boolean> AREACUT_KEYWORD_templ = new Dictionary<String, Boolean>
            String[] AREACUT_KEYWORD_LIST = {
                "UPGRADE_12CR1",                 // 2013/08/13 add 12c対応
                "WINDOWS2012",                   // 2013/08/13 add windows 2012対応
                "WINDOWS2012R2",                 // 2014/09/12 add windows 2012 R2対応
                "UPGRADE_11GR2",               
                "WINDOWS2008R2",               
                "WINDOWS_32BIT",                 // 2010/04/20 add プラットフォームがWindowsかつ32bit
                "UPGRADE_11GR1",               
                "UPGRADE_11GR1_WINDOWS2008",   
                "UPGRADE_10GR2",               
                "UPGRADE_9IR2",                
                "DIRECT_UPGRADE",              
                "DATA_MIGRATE",                
                "EE_TO_SE",                    
                "NOT_EE_TO_SE",            
                // "CURRENT_VER_AND_UNDER_8I",      // 2010/04/20 add 二段階Upgrade 8i以下
                // "CURRENT_VER_AND_UNDER_9IR1",    // 2010/04/20 add 二段階Upgrade 9iR1以下
                // "CURRENT_VER_AND_UNDER_9IR2",    // 2010/04/20 add 二段階Upgrade 9iR2以下
                // "CURRENT_VER_8I",                // 2010/04/20 add 二段階Upgrade
                // "CURRENT_VER_9IR1",              // 2010/04/20 add 二段階Upgrade
                // "CURRENT_VER_9IR2",              // 2010/04/20 add 二段階Upgrade
                // "CURRENT_VER_10GR1",             // 2010/04/20 add 二段階Upgrade
                // "CURRENT_VER_10GR2",             // 2010/04/20 add 二段階Upgrade
                // "CURRENT_VER_11GR1",             // 2010/04/20 add 二段階Upgrade
                "CURRENT_9I_UPPER",              // 2010/04/20 add TTS利用可否
                // "CURRENT_NOT_9I_UPPER",          // 2010/04/20 add TTS利用可否
                "CURRENT_ENTERPRISE",            // 2010/04/20 add TTS利用可否
                // "CURRENT_NOT_ENTERPRISE",        // 2010/04/20 add TTS利用不可能
                "PLATFORM_CHANGE",             
                "PLATFORM_NOT_CHANGE",         
                "WORDSIZE_CHANGE",             
                "WORDSIZE_NOT_CHANGE",         
                "HP_PA2IT",                    
                "HARDWARE_REPLACE",            
                "HARDWARE_NOT_REPLACE",        
                "CLIENT_CONNECT_OK",           
                "CLIENT_CONNECT_NG",           
                "CLIENT_CONNECT_AS_OK",        
                "CLIENT_CONNECT_AS_NG",
                "CLIENT_CONNECT_ES",              // 2013.08.14 Add
                "CLIENT_CONNECT_WAS",             // 2013.08.14 Add
                "CLIENT_CONNECT_AS_ES",           // 2013.08.14 Add
                "CLIENT_CONNECT_AS_WAS",          // 2013.08.14 Add
              //  "CLIENT10G_SERVER11G_CLNT",     // Note 4511371.8,Note//207303.1,KROWN//56903 2013.08.14 Del
              //  "CLIENT_NOT10G_SERVER11G_CLNT", // Note 4511371.8,Note//207303.1,KROWN//56903 2013.08.14 Del
              //  "CLIENT10G_SERVER11G_AS",       // Note 4511371.8,Note//207303.1,KROWN//56903 2013.08.14 Del
              //  "CLIENT_NOT10G_SERVER11G_AS",   // Note 4511371.8,Note//207303.1,KROWN//56903 2013.08.14 Del
                "APP_USE_PRECOMP",             
                "APP_USE_VB_C",                
                "APP_USE_DOTNET",              
                "APP_USE_FORMS",               
                "APP_USE_JAVA",                
                "APP_USE_ACCESS",              
                "APP_USE_PB",                  
                "APP_USE_DELPHI",              
                "APP_USE_ODBC",                 
                "Fuzzy",                        // -- True の時にWordのキーワードを残す 
            };
            foreach (String str in AREACUT_KEYWORD_LIST)
            {
                AREACUT_KEYWORD[str] = false;
            }

            V2oCheckDB ckdb = new V2oCheckDB();

            if (UPGRADE_CHECKLIST["PlatformFuzzy"])   AREACUT_KEYWORD["Fuzzy"] = true;
            if (UPGRADE_CHECKLIST["OraVerFuzzy"])     AREACUT_KEYWORD["Fuzzy"] = true;
            if (UPGRADE_CHECKLIST["OraASVerFuzzy"])   AREACUT_KEYWORD["Fuzzy"] = true;
            if (UPGRADE_CHECKLIST["OraClntVerFussy"]) AREACUT_KEYWORD["Fuzzy"] = true;

            // ------------------------------------
            // Oracle の移行前、移行後の少なくともどちらか片方のバージョンが不明
            // ------------------------------------
            if (UPGRADE_CHECKLIST["OraVerFuzzy"] == true)
                if (ckdb.GetVersionNumberString(HEARING_SHEET["NewOracleVersion"]) == "")
                {
                    AREACUT_KEYWORD["UPGRADE_11GR2"] = true;
                    AREACUT_KEYWORD["UPGRADE_11GR1"] = true;
                    AREACUT_KEYWORD["UPGRADE_10GR2"] = true;
                    AREACUT_KEYWORD["UPGRADE_9IR2"] = true;
                }
            // ------------------------------------
            // 移行先は12cである
            // ------------------------------------
            if (HEARING_SHEET["NewOracleVersion"].IndexOf("12.1.0") == 0) // 文字列の先頭とマッチした
                AREACUT_KEYWORD["UPGRADE_12CR1"] = true;
            
            // ------------------------------------
            // 移行先は11gである
            // ------------------------------------
            if (HEARING_SHEET["NewOracleVersion"].IndexOf("11.2.0") == 0) // 文字列の先頭とマッチした
                AREACUT_KEYWORD["UPGRADE_11GR2"] = true;

            if (HEARING_SHEET["NewOracleVersion"].IndexOf("11.1.0") == 0) // 文字列の先頭とマッチした
                AREACUT_KEYWORD["UPGRADE_11GR1"] = true;
            // ------------------------------------
            // 移行先で利用するプラットフォームは Windows Server 2012である
            // ------------------------------------
            if (HEARING_SHEET["NewPlatform"].IndexOf("Microsoft Windows 2012") == 0)
            {
                // ------------------------------------
                // 移行先で利用するプラットフォームは Windows Server 2012 R2である
                // ------------------------------------
                if (HEARING_SHEET["NewPlatform"].IndexOf("Microsoft Windows 2012 R2") == 0)
                    AREACUT_KEYWORD["WINDOWS2012R2"] = true;
                else
                    AREACUT_KEYWORD["WINDOWS2012"] = true;
            }
            // ------------------------------------
            // 移行先で利用するプラットフォームは Windows Server 2008 R2である
            // ------------------------------------
            if (HEARING_SHEET["NewPlatform"].IndexOf("Microsoft Windows 2008 R2") == 0)
                AREACUT_KEYWORD["WINDOWS2008R2"] = true;
            // ------------------------------------
            // 移行先で利用するプラットフォームは Windowsプラットフォームだが、Windows Server 2008 R2ではない
            // ------------------------------------
            if (HEARING_SHEET["NewPlatform"].IndexOf("Microsoft Windows") == 0)
                if (HEARING_SHEET["NewPlatform"].IndexOf("x86") != -1) // マッチした
                    AREACUT_KEYWORD["WINDOWS_32BIT"] = true;
            // ------------------------------------
            // 移行先で利用するプラットフォームは Windows Server 2008であり、利用するDBバージョンは11gである
            // ------------------------------------
            if (HEARING_SHEET["NewPlatform"] == "Microsoft Windows 2008")
                if (AREACUT_KEYWORD["UPGRADE_11GR1"])   // true
                    AREACUT_KEYWORD["UPGRADE_11GR1_WINDOWS2008"] = true;
            // ------------------------------------
            // 移行先は10g R2である
            // ------------------------------------
            if (HEARING_SHEET["NewOracleVersion"].IndexOf("10.2.0") == 0)
                AREACUT_KEYWORD["UPGRADE_10GR2"] = true;
            // ------------------------------------
            // 移行先は9i R2である
            // ------------------------------------
            if (HEARING_SHEET["NewOracleVersion"].IndexOf("9.2.0") == 0)
                AREACUT_KEYWORD["UPGRADE_9IR2"] = true;
            // ------------------------------------
            // 移行先のエディションはSEであり、かつ現在のエディションがEEもしくはEEの可能性がある
            // (移行先がSEであり、現在のエディションがSE || SE One以外である)
            // ------------------------------------
            AREACUT_KEYWORD["NOT_EE_TO_SE"] = true;
            if (HEARING_SHEET["NewOracleEdition"].IndexOf("Standard") == 0)
                if (HEARING_SHEET["CurrentOracleEdition"].IndexOf("Standard") != 0)
                {
                    AREACUT_KEYWORD["EE_TO_SE"] = true;
                    AREACUT_KEYWORD["NOT_EE_TO_SE"] = false;
                }

            // ------------------------------------
            // 直接のアップグレード・パスがあるのは、同種のプラットフォームで、かつOracle Databaseがサポートしているもの
            // ------------------------------------
            if (UPGRADE_CHECKLIST["PlatformFuzzy"] == true)
            {
                AREACUT_KEYWORD["DIRECT_UPGRADE"] = true;
                AREACUT_KEYWORD["DATA_MIGRATE"] = true;
                AREACUT_KEYWORD["PLATFORM_CHANGE"] = true;
                AREACUT_KEYWORD["PLATFORM_NOT_CHANGE"] = true;
                AREACUT_KEYWORD["WORDSIZE_CHANGE"] = true;
                AREACUT_KEYWORD["WORDSIZE_NOT_CHANGE"] = true;
            }
            else
            {
                if (UPGRADE_CHECKLIST["DirectUpgrade"] && UPGRADE_CHECKLIST["EqualPlatform"])
                    // 直接のアップグレード・パスがあるのは、同種のプラットフォームで
                    // かつOracle Databaseがサポートしているもの
                    AREACUT_KEYWORD["DIRECT_UPGRADE"] = true;
                else
                    // 同種のプラットフォームでも、Oracle Databaseが
                    // サポートしていないバージョンはデータ移行で実施
                    AREACUT_KEYWORD["DATA_MIGRATE"] = true;
                // ------------------------------------
                // 移行元、移行先でプラットフォームが変わってしまうもの
                // ------------------------------------
                if (UPGRADE_CHECKLIST["EqualPlatform"])
                    AREACUT_KEYWORD["PLATFORM_NOT_CHANGE"] = true;
                else
                    AREACUT_KEYWORD["PLATFORM_CHANGE"] = true;
                // ------------------------------------
                // 直接のアップグレード・パスがあるもののうち、ワードサイズが変わってしまうもの
                // ------------------------------------
                if (UPGRADE_CHECKLIST["DirectUpgrade"])
                    if (UPGRADE_CHECKLIST["EqualWordsize"])
                        AREACUT_KEYWORD["WORDSIZE_NOT_CHANGE"] = true;
                    else
                        AREACUT_KEYWORD["WORDSIZE_CHANGE"] = true;
            }
            // ------------------------------------
            // HP-UX PA RISC から Itaniumへの移行
            // ------------------------------------
            if (UPGRADE_CHECKLIST["HPUXPA2HPUXIT"])
                AREACUT_KEYWORD["HP_PA2IT"] = true;
            // ------------------------------------
            // ハードウェア・リプレースをおこなう場合
            // ------------------------------------
            if (HEARING_SHEET["NewHardWare"] == "H/Wリプレース予定")
                AREACUT_KEYWORD["HARDWARE_REPLACE"] = true;
            else   
            AREACUT_KEYWORD["HARDWARE_NOT_REPLACE"] = true;
            // ------------------------------------
            // 移行でTTS利用可否を判断する
            //　　既存EditionがEnterprise かつ 既存DBバージョンが9i以上
            // ------------------------------------
            if (ckdb.Is9iUpper(HEARING_SHEET["CurrentOracleVersion"]))
                AREACUT_KEYWORD["CURRENT_9I_UPPER"] = true;
            // else
            //     AREACUT_KEYWORD["CURRENT_NOT_9I_UPPER"] = true;
            if (ckdb.IsEE(HEARING_SHEET["CurrentOracleEdition"]))
                AREACUT_KEYWORD["CURRENT_ENTERPRISE"] = true;
            // else
            //     AREACUT_KEYWORD["CURRENT_NOT_ENTERPRISE"] = true;
            // ------------------------------------
            // 既存環境のバージョン(メジャー・リリース)を設定
            // 2013.08.14 DEL
            // ------------------------------------
            // String curVer = ckdb.ReturnMajorRelease(HEARING_SHEET["CurrentOracleVersion"]);
            // if (curVer == "8.0")
            // {
            //     AREACUT_KEYWORD["CURRENT_VER_AND_UNDER_8I"]= true;
            //     AREACUT_KEYWORD["CURRENT_VER_AND_UNDER_9IR1"] = true;
            //     AREACUT_KEYWORD["CURRENT_VER_AND_UNDER_9IR2"] = true;
            // }
            // if (curVer == "8.1")
            // {
            //     AREACUT_KEYWORD["CURRENT_VER_8I"] = true;
            //     AREACUT_KEYWORD["CURRENT_VER_AND_UNDER_8I"]= true;
            //     AREACUT_KEYWORD["CURRENT_VER_AND_UNDER_9IR1"] = true;
            //     AREACUT_KEYWORD["CURRENT_VER_AND_UNDER_9IR2"] = true;
            // }
            // if (curVer == "9.0")
            // {
            //     AREACUT_KEYWORD["CURRENT_VER_9IR1"] = true;
            //     AREACUT_KEYWORD["CURRENT_VER_AND_UNDER_9IR1"] = true;
            //     AREACUT_KEYWORD["CURRENT_VER_AND_UNDER_9IR2"] = true;
            // }
            // if (curVer == "9.2")
            // {
            //     AREACUT_KEYWORD["CURRENT_VER_9IR2"] = true;
            //     AREACUT_KEYWORD["CURRENT_VER_AND_UNDER_9IR2"] = true;
            // }
            // if (curVer == "10.1")
            //     AREACUT_KEYWORD["CURRENT_VER_10GR1"] = true;
            // if (curVer == "10.2")
            //     AREACUT_KEYWORD["CURRENT_VER_10GR2"] = true;
            // if (curVer == "11.1")
            //     AREACUT_KEYWORD["CURRENT_VER_11GR1"] = true;

            // ------------------------------------
            // 既存のOracle ClientからOracle Databaseへの接続可否
            // ------------------------------------
            if (UPGRADE_CHECKLIST["OraClntVerFussy"])
            {
                AREACUT_KEYWORD["CLIENT_CONNECT_OK"] = true;
                AREACUT_KEYWORD["CLIENT_CONNECT_NG"] = true;
                AREACUT_KEYWORD["CLIENT_CONNECT_ES"] = true;
                AREACUT_KEYWORD["CLIENT_CONNECT_WAS"] = true;
            }
            else
            {
                if (UPGRADE_CHECKLIST["ClntNeedUpgClnt"])
                    AREACUT_KEYWORD["CLIENT_CONNECT_OK"] = true;
                else
                    AREACUT_KEYWORD["CLIENT_CONNECT_NG"] = true;

                if (AREACUT_KEYWORD["CLIENT_CONNECT_OK"])
                {
                    AREACUT_KEYWORD["CLIENT_CONNECT_ES"] = UPGRADE_CHECKLIST["ClntConnClntES"];
                    AREACUT_KEYWORD["CLIENT_CONNECT_WAS"] = UPGRADE_CHECKLIST["ClntConnClntWAS"];
                }
            }

            if (UPGRADE_CHECKLIST["OraASVerFuzzy"])
            {
                AREACUT_KEYWORD["CLIENT_CONNECT_AS_OK"] = true;
                AREACUT_KEYWORD["CLIENT_CONNECT_AS_NG"] = true;
                AREACUT_KEYWORD["CLIENT_CONNECT_AS_ES"] = true;
                AREACUT_KEYWORD["CLIENT_CONNECT_AS_WAS"] = true;
            }
            else
            {
                if (UPGRADE_CHECKLIST["ClntNeedUpgAS"])
                    AREACUT_KEYWORD["CLIENT_CONNECT_AS_OK"] = true;
                else
                    AREACUT_KEYWORD["CLIENT_CONNECT_AS_NG"] = true;

                // 2013.08.14 ADD
                if (AREACUT_KEYWORD["CLIENT_CONNECT_AS_OK"])
                {
                    AREACUT_KEYWORD["CLIENT_CONNECT_AS_ES"] = UPGRADE_CHECKLIST["ClntConnASES"];
                    AREACUT_KEYWORD["CLIENT_CONNECT_AS_WAS"] = UPGRADE_CHECKLIST["ClntConnASWAS"];
                }
            }
            // ------------------------------------
            // Note 4511371.8,Note//207303.1,KROWN//56903 の//6対応
            // Client からの接続をチェック 2013.08.14 DEL
            // ------------------------------------
            // String NewOraVer = ckdb.GetVersionNumberString(HEARING_SHEET["NewOracleVersion"]);
            // String CurOraVer = ckdb.GetVersionNumberString(HEARING_SHEET["CurrentOracleVersion"]);
            // String CurClntVer = ckdb.GetVersionNumberString(HEARING_SHEET["DevClntMWVer"]);
            // String CurASVer = ckdb.GetVersionNumberString(HEARING_SHEET["DevASMWVer"]);
            // AREACUT_KEYWORD["CLIENT_NOT10G_SERVER11G_CLNT"] = true;
            // if (CurClntVer != "" && NewOraVer != "")
            //     if (NewOraVer == "11.2.0.1" || NewOraVer == "11.1.0.7")
            //         if (CurClntVer == "10.1.0.2" || CurClntVer == "10.2.0.1")
            //         {
            //             AREACUT_KEYWORD["CLIENT10G_SERVER11G_CLNT"] = true;
            //             AREACUT_KEYWORD["CLIENT_NOT10G_SERVER11G_CLNT"] = false;
            //         }
            // else
            //     if (CurOraVer != "" && NewOraVer != "") 
            //         if (NewOraVer == "11.2.0.1" || NewOraVer == "11.1.0.7")
            //             if (CurOraVer == "10.1.0.2" || CurOraVer == "10.2.0.1")
            //             {
            //                 AREACUT_KEYWORD["CLIENT10G_SERVER11G_CLNT"] = true;
            //                 AREACUT_KEYWORD["CLIENT_NOT10G_SERVER11G_CLNT"] = false;
            //             }
            // ASからの接続をチェック 2013.08.14 DEL
            // AREACUT_KEYWORD["CLIENT_NOT10G_SERVER11G_AS"] = true;
            // if (CurASVer != "" && NewOraVer != "")
            //     if (NewOraVer == "11.2.0.1" || NewOraVer == "11.1.0.7")
            //         if (CurASVer == "10.1.0.2" || CurASVer == "10.2.0.1")
            //         {
            //             AREACUT_KEYWORD["CLIENT10G_SERVER11G_AS"] = true;
            //             AREACUT_KEYWORD["CLIENT_NOT10G_SERVER11G_AS"] = false;
            //         }
            // else
            //     if (CurOraVer != "" && NewOraVer != "") 
            //         if (NewOraVer == "11.2.0.1" || NewOraVer == "11.1.0.7")
            //             if (CurOraVer == "10.1.0.2" || CurOraVer == "10.2.0.1")
            //             {
            //                 AREACUT_KEYWORD["CLIENT10G_SERVER11G_AS"] = true;
            //                 AREACUT_KEYWORD["CLIENT_NOT10G_SERVER11G_AS"] = false;
            //             }
            // ------------------------------------
            // プリコンパイラ製品を利用しているかどうか
            // ------------------------------------
            if (HEARING_SHEET["DevServLangProCOBOL"] == "1") 
                AREACUT_KEYWORD["APP_USE_PRECOMP"] = true;
            if (HEARING_SHEET["DevServLangProC"] == "1")
                AREACUT_KEYWORD["APP_USE_PRECOMP"] = true;
            // ------------------------------------
            // VBもしくはVCを利用しているかどうか
            // ------------------------------------
            if (HEARING_SHEET["DevClntLangVB"] == "1" || HEARING_SHEET["DevClntLangVBVer"] != "")
                AREACUT_KEYWORD["APP_USE_VB_C"] =true;
            if (HEARING_SHEET["DevClntLangVC"] == "1" || HEARING_SHEET["DevClntLangVCVer"] != "")
                AREACUT_KEYWORD["APP_USE_VB_C"] =true;
            // ------------------------------------
            // .NETを利用しているかどうか
            // ------------------------------------
            if (HEARING_SHEET["DevClntLangDNET"] == "1" || HEARING_SHEET["DevClntLangDNETVer"] != "")
                AREACUT_KEYWORD["APP_USE_DOTNET"] = true;
            // ------------------------------------
            // Forms, Reports を利用しているかどうか
            // ------------------------------------
            if (HEARING_SHEET["DevClntLangForms"] == "1" || HEARING_SHEET["DevClntLangFormsVer"] != "")
                AREACUT_KEYWORD["APP_USE_FORMS"] = true;
            // ------------------------------------
            // Java を利用しているかどうか
            // ------------------------------------
            if (HEARING_SHEET["DevClntLangJava"] == "1" || HEARING_SHEET["DevClntLangJavaVer"] != "")
                AREACUT_KEYWORD["APP_USE_JAVA"] = true;
            if (HEARING_SHEET["DevASLang"].IndexOf("Java") == 0)
                AREACUT_KEYWORD["APP_USE_JAVA"] = true;
            // ------------------------------------
            // MS Access から利用しているかどうか
            // ------------------------------------
            if (HEARING_SHEET["DevClntLangAccess"] == "1" || HEARING_SHEET["DevClntLangAccessVer"] != "")
                AREACUT_KEYWORD["APP_USE_ACCESS"] = true;
            // ------------------------------------
            // PowerBuilderを利用しているかどうか
            // ------------------------------------
            if (HEARING_SHEET["DevClntLangPB"] == "1" || HEARING_SHEET["DevClntLangPBVer"] != "")
                AREACUT_KEYWORD["APP_USE_PB"] = true;
            // ------------------------------------
            // Delphi を利用しているかどうか
            // ------------------------------------
            if (HEARING_SHEET["DevClntLangDelphi"] == "1" || HEARING_SHEET["DevClntLangDelphiVer"] != "")
                AREACUT_KEYWORD["APP_USE_DELPHI"] = true;
            // ------------------------------------
            // ODBCドライバを利用しているかどうか
            // ------------------------------------
            if (HEARING_SHEET["DevClntMW"].IndexOf("ODBC") == 0)
                AREACUT_KEYWORD["APP_USE_ODBC"] = true;
        }
    }
}
