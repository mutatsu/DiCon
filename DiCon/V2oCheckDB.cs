using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DiCon
{
    public class V2oCheckDB
    {
        // Upgrade対象のバージョン -------- ここから
        String[] For12cR1 = {
            "10.2.0.5", // Terminal
            "11.1.0.7", // Terminal
            "11.2.0.2",
            "11.2.0.3",
            "11.2.0.4", // Terminal
        };
        String[] For11gR2 = {
            "9.2.0.8",  // Terminal
            "10.1.0.5", // Terminal
            "10.2.0.2",
            "10.2.0.3",
            "10.2.0.4",
            "10.2.0.5", // Terminal
            "11.1.0.6",
            "11.1.0.7",
        };
        String[] For11gR1 = {
            "9.2.0.4",  // Terminal
            "9.2.0.5",
            "9.2.0.6",
            "9.2.0.7",
            "9.2.0.8",  // Terminal
            "10.1.0.2",
            "10.1.0.3",
            "10.1.0.4",
            "10.1.0.5", // Terminal
            "10.2.0.1",
            "10.2.0.2",
            "10.2.0.3",
            "10.2.0.4",
            "10.2.0.5", //Terminal
        };
        String[] For10gR2 = {
            "8.1.7.4",  // Terminal
            "9.0.1.4",  // Terminal
            "9.2.0.4",
            "9.2.0.5",
            "9.2.0.6",
            "9.2.0.7",
            "9.2.0.8",  // Terminal
            "10.1.0.2",
            "10.1.0.3",
            "10.1.0.4",
            "10.1.0.5", // Terminal
        };
        String[] For10gR1 = {
            "8.0.6.0",
            "8.0.6.1",
            "8.0.6.2",
            "8.0.6.3",
            "8.1.7.0",
            "8.1.7.1",
            "8.1.7.2",
            "8.1.7.3",
            "8.1.7.4",  // Terminal
            "9.0.1.0",
            "9.0.1.1",
            "9.0.1.2",
            "9.0.1.3",
            "9.0.1.4",  // Terminal
            "9.2.0.1",
            "9.2.0.2",
            "9.2.0.3",
            "9.2.0.4",
            "9.2.0.5",
            "9.2.0.6",
            "9.2.0.7",
            "9.2.0.8",  // Terminal
        };
        String[] For9iR2 ={
            "7.3.4.0",
            "7.3.4.1",
            "7.3.4.2",
            "7.3.4.3",
            "7.3.4.4",
            "7.3.4.5",
            "8.0.6.0",
            "8.0.6.1",
            "8.0.6.2",
            "8.0.6.3",
            "8.1.7.0",
            "8.1.7.1",
            "8.1.7.2",
            "8.1.7.3",
            "8.1.7.4",  // Terminal
            "9.0.1.0",
            "9.0.1.1",
            "9.0.1.2",
            "9.0.1.3",
            "9.0.1.4",  // Terminal
        };
        // Upgrade対象のバージョン -------- ここまで
        // 接続可否 ---------------------- ここから
        // Client から12c R1への接続 （Note 207303.1）
        Dictionary<String, String> ConnTo12cR1 = new Dictionary<string, string> {
            {"10.2.0.1","ES"}, // Note 4511371.8,Note//207303.1,KROWN//56903 対象外2010/04/12
            {"10.2.0.2","ES"},
            {"10.2.0.3","ES"},
            {"10.2.0.4","ES"},
            {"10.2.0.5","ES"}, // Terminal
            {"11.1.0.6","ES"},
            {"11.1.0.7","ES"}, // Terminal
            {"11.2.0.1","YES"},
            {"11.2.0.2","YES"},
            {"11.2.0.3","YES"},
            {"11.2.0.4","YES"}, // Terminal
            {"12.1.0.1","YES"},
            {"12.1.0.2","YES"}, // Terminal
        };
        // Client から11g R2への接続 （Note 207303.1）
        Dictionary<String, String> ConnTo11gR2 = new Dictionary<string, string> {
            {"9.2.0.4", "WAS"},  // Terminal
            {"9.2.0.5", "WAS"},
            {"9.2.0.6", "WAS"},
            {"9.2.0.7", "WAS"},
            {"9.2.0.8", "WAS"},  // Terminal
            {"10.1.0.2","WAS"}, // Note 4511371.8,Note//207303.1,KROWN//56903 対象外2010/04/12
            {"10.1.0.3","WAS"}, // Note 4511371.8,Note//207303.1,KROWN//56903 対象外2010/04/12
            {"10.1.0.4","WAS"}, // Note 4511371.8,Note//207303.1,KROWN//56903 対象外2010/04/12
            {"10.1.0.5","WAS"}, // Terminal
            {"10.2.0.1","ES"}, // Note 4511371.8,Note//207303.1,KROWN//56903 対象外2010/04/12
            {"10.2.0.2","ES"},
            {"10.2.0.3","ES"},
            {"10.2.0.4","ES"},
            {"10.2.0.5","ES"}, // Terminal
            {"11.1.0.6","ES"},
            {"11.1.0.7","ES"}, // Terminal
            {"11.2.0.1","YES"},
            {"11.2.0.2","YES"},
            {"11.2.0.3","YES"},
            {"11.2.0.4","YES"}, // Terminal
            {"12.1.0.1","YES"},
            {"12.1.0.2","YES"}, // Terminal
        };
        // Client から11gR1への接続
        Dictionary<String, String> ConnTo11gR1 = new Dictionary<string, string> {
            {"9.2.0.4", "WAS"},  // Terminal
            {"9.2.0.5", "WAS"},
            {"9.2.0.6", "WAS"},
            {"9.2.0.7", "WAS"},
            {"9.2.0.8", "WAS"},  // Terminal
            {"10.1.0.2","WAS"}, // Note 4511371.8,Note//207303.1,KROWN//56903 対象外2010/04/12
            {"10.1.0.3","WAS"}, // Note 4511371.8,Note//207303.1,KROWN//56903 対象外2010/04/12
            {"10.1.0.4","WAS"}, // Note 4511371.8,Note//207303.1,KROWN//56903 対象外2010/04/12
            {"10.1.0.5","WAS"}, // Terminal
            {"10.2.0.1","ES"}, // Note 4511371.8,Note//207303.1,KROWN//56903 対象外2010/04/12
            {"10.2.0.2","ES"},
            {"10.2.0.3","ES"},
            {"10.2.0.4","ES"},
            {"10.2.0.5","ES"}, // Terminal
            {"11.1.0.6","ES"},
            {"11.1.0.7","ES"},
            {"11.2.0.1","ES"},
            {"11.2.0.2","ES"},
            {"11.2.0.3","ES"},
            {"11.2.0.4","ES"},
            {"12.1.0.1","ES"},
        };
        // Client から10gR2への接続
        Dictionary<String, String> ConnTo10gR2 = new Dictionary<string, string> {
            {"8.1.7.0", "WAS"},
            {"8.1.7.1", "WAS"},
            {"8.1.7.2", "WAS"},
            {"8.1.7.3", "WAS"},
            {"8.1.7.4", "WAS"}, // Terminal
            {"9.2.0.4", "WAS"},  // Terminal
            {"9.2.0.5", "WAS"},
            {"9.2.0.6", "WAS"},
            {"9.2.0.7", "WAS"},
            {"9.2.0.8", "WAS"},  // Terminal
            {"10.1.0.2","WAS"},
            {"10.1.0.3","WAS"},
            {"10.1.0.4","WAS"},
            {"10.1.0.5","WAS"}, // Terminal
            {"10.2.0.1","ES"},
            {"10.2.0.2","ES"},
            {"10.2.0.3","ES"},
            {"10.2.0.4","ES"},
            {"10.2.0.5","ES"}, // Terminal
            {"11.1.0.6","ES"}, // Note 4511371.8,Note//207303.1,KROWN//56903
            {"11.1.0.7","ES"}, // Note 4511371.8,Note//207303.1,KROWN//56903
            {"11.2.0.1","ES"}, // Note 4511371.8,Note//207303.1,KROWN//56903
            {"11.2.0.2","ES"}, // Note 4511371.8,Note//207303.1,KROWN//56903
            {"11.2.0.3","ES"},
            {"11.2.0.4","ES"},
            {"12.1.0.1","ES"},
            {"12.1.0.2","ES"},
        };
        // Client から10gR1への接続
        Dictionary<String, String> ConnTo10gR1 = new Dictionary<string, string> {
            {"8.1.7.0", "WAS"},
            {"8.1.7.1", "WAS"},
            {"8.1.7.2", "WAS"},
            {"8.1.7.3", "WAS"},
            {"8.1.7.4", "WAS"}, // Terminal
            {"9.0.1.0", "WAS"},
            {"9.0.1.1", "WAS"},
            {"9.0.1.2", "WAS"},
            {"9.0.1.3", "WAS"},
            {"9.0.1.4", "WAS"}, // Terminal
            {"9.2.0.1", "WAS"},
            {"9.2.0.2", "WAS"},
            {"9.2.0.3", "WAS"},
            {"9.2.0.4", "WAS"},  // Terminal
            {"9.2.0.5", "WAS"},
            {"9.2.0.6", "WAS"},
            {"9.2.0.7", "WAS"},
            {"9.2.0.8", "WAS"},  // Terminal
            {"10.1.0.2","WAS"},
            {"10.1.0.3","WAS"},
            {"10.1.0.4","WAS"},
            {"10.1.0.5","WAS"}, // Terminal
            {"10.2.0.1","WAS"},
            {"10.2.0.2","WAS"},
            {"10.2.0.3","WAS"},
            {"10.2.0.4","WAS"},
            {"10.2.0.5","WAS"}, // Terminal
            {"11.1.0.6","WAS"}, // Note 4511371.8,Note//207303.1,KROWN//56903
            {"11.1.0.7","WAS"}, // Note 4511371.8,Note//207303.1,KROWN//56903
        //  {"11.2.0.1",""}, // Has never been supported
        };
        // Client から9iR2への接続
        Dictionary<String, String> ConnTo9iR2 = new Dictionary<string, string> {
            {"7.3.4.0", "WAS"},
            {"7.3.4.1", "WAS"},
            {"7.3.4.2", "WAS"},
            {"7.3.4.3", "WAS"},
            {"7.3.4.4", "WAS"},
            {"7.3.4.5", "WAS"},
            {"8.0.6.0", "WAS"},
            {"8.0.6.1", "WAS"},
            {"8.0.6.2", "WAS"},
            {"8.0.6.3", "WAS"},
            {"8.1.7.0", "WAS"},
            {"8.1.7.1", "WAS"},
            {"8.1.7.2", "WAS"},
            {"8.1.7.3", "WAS"},
            {"8.1.7.4", "WAS"}, // Terminal
            {"9.0.1.0", "WAS"},
            {"9.0.1.1", "WAS"},
            {"9.0.1.2", "WAS"},
            {"9.0.1.3", "WAS"},
            {"9.0.1.4", "WAS"}, // Terminal
            {"9.2.0.1", "WAS"},
            {"9.2.0.2", "WAS"},
            {"9.2.0.3", "WAS"},
            {"9.2.0.4", "WAS"},  // Terminal
            {"9.2.0.5", "WAS"},
            {"9.2.0.6", "WAS"},
            {"9.2.0.7", "WAS"},
            {"9.2.0.8", "WAS"},  // Terminal
            {"10.1.0.2","WAS"},
            {"10.1.0.3","WAS"},
            {"10.1.0.4","WAS"},
            {"10.1.0.5","WAS"}, // Terminal
            {"10.2.0.1","WAS"},
            {"10.2.0.2","WAS"},
            {"10.2.0.3","WAS"},
            {"10.2.0.4","WAS"},
            {"10.2.0.5","WAS"}, // Terminal
            {"11.1.0.6","WAS"},  
            {"11.1.0.7","WAS"}, 
            {"11.2.0.1","WAS"}, 
            {"11.2.0.2","WAS"}, 
            {"11.2.0.3","WAS"},
            {"11.2.0.4","WAS"},
        };
        // 接続可否 ---------------------- ここまで
        static int LINUX32   = 101;
        static int LINUX64   = 102;
        static int LINUX64IT = 103;
        static int WIN32     = 201;
        static int WIN64     = 202;
        static int WIN64IT   = 203;
        static int HPUXPA32  = 301;
        static int HPUXPA64  = 302;
        static int HPUXIT    = 303;
        static int AIX32     = 401;
        static int AIX64     = 402;
        static int SOL32     = 501;
        static int SOL64     = 502;
        static int SOL32x86  = 601;
        static int SOL64x86  = 602;
        Dictionary<String, int> FromPlatform = new Dictionary<String, int>{
            {"Linux (x86)",                            LINUX32},
            {"Linux (x86-64)",                         LINUX64},
            {"Linux (Itanium)",                        LINUX64IT},
            {"Microsoft Windows NT (x86)",             WIN32},
            {"Microsoft Windows 2000 (x86)",           WIN32},
            {"Microsoft Windows 2000 (AMD64/EM64T)",   WIN64},
            {"Microsoft Windows 2003 (x86)",           WIN32},
            {"Microsoft Windows 2003 (AMD64/EM64T)",   WIN64},
            {"Microsoft Windows 2003 (Itanium)",       WIN64IT},
            {"Microsoft Windows 2008 (x86)",           WIN32},
            {"Microsoft Windows 2008 (AMD64/EM64T)",   WIN64},
            {"Microsoft Windows 2008 R2 (AMD64/EM64T)",WIN64},
            {"Microsoft Windows 2012 (AMD64/EM64T)",   WIN64},
            {"Microsoft Windows 2012 R2 (AMD64/EM64T)",WIN64},
            {"HP-UX PA-RISC (32bit)",                  HPUXPA32},
            {"HP-UX PA-RISC (64bit)",                  HPUXPA64},
            {"HP-UX Itanium",                          HPUXIT},
            {"IBM AIX POWER System (32bit)",           AIX32},
            {"IBM AIX POWER System (64bit)",           AIX64},
            {"Sun Solaris SPARC (32bit)",              SOL32},
            {"Sun Solaris SPARC (64bit)",              SOL64},
            {"Sun Solaris x86 (32-bit)",               SOL32x86},
            {"Sun Solaris x86-64 (64-bit)",            SOL64x86},
        };
        Dictionary<String, int> ToPlatform = new Dictionary<String, int>{
            {"Linux (x86)",                            LINUX32},
            {"Linux (x86-64)",                         LINUX64},
            {"Linux (Itanium)",                        LINUX64IT},
            {"Microsoft Windows 2003 (x86)",           WIN32},
            {"Microsoft Windows 2003 (AMD64/EM64T)",   WIN64},
            {"Microsoft Windows 2003 (Itanium)" ,      WIN64IT},
            {"Microsoft Windows 2008 (x86)",           WIN32},
            {"Microsoft Windows 2008 (AMD64/EM64T)",   WIN64},
            {"Microsoft Windows 2008 R2 (AMD64/EM64T)",WIN64},
            {"Microsoft Windows 2012 (AMD64/EM64T)",   WIN64},
            {"Microsoft Windows 2012 R2 (AMD64/EM64T)",WIN64},
            {"HP-UX PA-RISC (64bit)",                  HPUXPA64},
            {"HP-UX Itanium",                          HPUXIT},
            {"IBM AIX POWER System (64bit)",           AIX64},
            {"Sun Solaris SPARC (64bit)",              SOL64},
            {"Sun Solaris x86-64 (64-bit)",            SOL64x86},
        };
        public V2oCheckDB()
        {
        }
        public Boolean CheckEqualSystem(String NewPlatform, String CurrentPlatform)
        {
            if (!ToPlatform.ContainsKey(NewPlatform))
            {
                Console.WriteLine("Key not found 'NewPlatform' :"+ NewPlatform);
                return false;
            }
            if (!FromPlatform.ContainsKey(CurrentPlatform))
            {
                Console.WriteLine("Key not found 'CurrentPlatform' :" + CurrentPlatform);
                return false;
            }
            if (NewPlatform == CurrentPlatform)
                return true; // Itanium同士もOK
           
            return false;
        }
        public Boolean CheckSameSystem(String NewPlatform, String CurrentPlatform)
        {
            // WordSizeが異なっていてもOK！
            double ToPlatformValue;
            double FromPlatformValue;
            if (!ToPlatform.ContainsKey(NewPlatform))
            {
                Console.WriteLine("Key not found 'NewPlatform' :" + NewPlatform);
                return false;
            }
            if (!FromPlatform.ContainsKey(CurrentPlatform))
            {
                Console.WriteLine("Key not found 'CurrentPlatform' :" + CurrentPlatform);
                return false;
            }
            if (NewPlatform == CurrentPlatform)
                return true; // Itanium同士もOK

            ToPlatformValue = ToPlatform[NewPlatform];
            FromPlatformValue = FromPlatform[CurrentPlatform];
            // 以下の2つのswitchで、NewPlatform == CUrrentPlatform 以外のItaniumをfalseとする
            switch ((int)ToPlatformValue)
            {
                case 103: // LINUX64IT
                case 203: // WIN64IT
                case 303: // HPUXIT:
                    return false;
                default:
                    break;
            }
            switch ((int)FromPlatformValue)
            {
                case 103: // LINUX64IT
                case 203: // WIN64IT
                case 303: // HPUXIT:
                    return false;
                default:
                    break;
            }

            if (Math.Floor((ToPlatformValue / 100)) == Math.Floor((FromPlatformValue / 100)))
                return true; // CurrentPlatformがItanium以外(上記switchで判断)で、100番台の値が同じ場合はtrue

            return false;
        }
        public Boolean CheckSamePlatformSameWordsize(String NewPlatform, String CurrentPlatform)
        {
            if (!ToPlatform.ContainsKey(NewPlatform))
                return false;

            if (!FromPlatform.ContainsKey(CurrentPlatform))
                return false;
  
            if (ToPlatform[NewPlatform] == FromPlatform[CurrentPlatform])
                return true;
            
            return false;       
        }
        public Boolean CheckHPUX_PA2IT(String NewPlatform, String CurrentPlatform)
        {
            if (!ToPlatform.ContainsKey(NewPlatform))
            {
                Console.WriteLine("Key not found 'NewPlatform' :" + NewPlatform);
                return false;
            }
            if (!FromPlatform.ContainsKey(CurrentPlatform))
            {
                Console.WriteLine("Key not found 'CurrentPlatform' :" + CurrentPlatform);
                return false;
            }
            
            if (ToPlatform[NewPlatform] == 303) // HPUXIT
            {
                if ((FromPlatform[CurrentPlatform] == 301) ||  // HPUXPA32
                    (FromPlatform[CurrentPlatform] == 302))    // HPUXPA64
                    return true;
            }
            return false;
        }
        public String GetVersionNumberString(String VersionStrings)
        {
            // Oracle Database のバージョン番号を取得する。
            // いくつかのパターンで入ってきたものを変換する必要がある。
            //  " 9.2.0.4" -> 前後の空白を削除するのみ
            //  " 9.2.0.1 - 9.2.0.3"  -> "-"より以前のバージョン番号を取得し、前後の空白を削除する
            //  " 9.0.1.4 (Terminal)" -> "Terminal" 文字列を削除し、前後の空白を削除する
            //  " 8.1.6.x" -> "x" を削除し、"0"(ゼロ)を追加し、前後の空白を削除する
            String VersionStr;
            Regex reg;
            Match m;

            VersionStr = VersionStrings;
            VersionStr.Trim();

            reg = new Regex(@"\d+\.\d+\.\d+\.x");
            m = reg.Match(VersionStr);
            if (m.Success == true)
            {
                return m.Value.Substring(0,m.Value.Length -1) + "0";
            }
            reg = new Regex(@"\d+\.\d+\.\d+\.\d+");
            m = reg.Match(VersionStr);
            if (m.Success == true)
            {
                return m.Value;
            }
            return "";
        }
        public Boolean CheckDirectUpgrade(String NewOracleVersion, String CurrentOracleVersion)
        {
            //直接のUpgradeが可能かどうかを調べる
            int i;
            i = NewOracleVersion.IndexOf("12.1.0");
            if (i != -1) // match
            {
                foreach (String str in For12cR1)
                {
                    if (str == CurrentOracleVersion)
                        return true;
                }
                return false;
            }
            i = NewOracleVersion.IndexOf("11.2.0");
            if (i != -1) // match
            {
                foreach (String str in For11gR2)
                {
                    if (str == CurrentOracleVersion)
                        return true;
                }
                return false;
            }
            i = NewOracleVersion.IndexOf("11.1.0");
            if (i != -1) // match
            {
                foreach (String str in For11gR1)
                {
                    if (str == CurrentOracleVersion)
                        return true;
                }
                return false;
            }
            i = NewOracleVersion.IndexOf("10.2.0");
            if (i != -1) // match
            {
                foreach (String str in For10gR2)
                {
                    if (str == CurrentOracleVersion)
                        return true;
                }
                return false;
            }
            i = NewOracleVersion.IndexOf("10.1.0");
            if (i != -1) // match
            {
                foreach (String str in For10gR1)
                {
                    if (str == CurrentOracleVersion)
                        return true;
                }
                return false;
            }
            i = NewOracleVersion.IndexOf("9.2.0");
            if (i != -1) // match
            {
                foreach (String str in For9iR2)
                {
                    if (str == CurrentOracleVersion)
                        return true;
                }
                return false;
            }
            return false;
        }
        public Boolean CheckConnect(String NewOracleVersion, String CurrentOracleVersion)
        {
            // 現在利用しているOracle Clientがそのまま利用できるか調べる
            int i;
            i = NewOracleVersion.IndexOf("12.1.0");
            if (i != -1) // match
            {
                if (ConnTo12cR1.ContainsKey(CurrentOracleVersion))
                    return true;
                return false;
            }
            i = NewOracleVersion.IndexOf("11.2.0");
            if (i != -1) // match
            {
                if (ConnTo11gR2.ContainsKey(CurrentOracleVersion))
                    return true;
                return false;
            }
            i = NewOracleVersion.IndexOf("11.1.0");
            if (i != -1) // match
            {
                if (ConnTo11gR1.ContainsKey(CurrentOracleVersion))
                    return true;
                return false;
            }
            i = NewOracleVersion.IndexOf("10.2.0");
            if (i != -1) // match
            {
                if (ConnTo10gR2.ContainsKey(CurrentOracleVersion))
                    return true;
                return false;
            }
            i = NewOracleVersion.IndexOf("10.1.0");
            if (i != -1) // match
            {
                if (ConnTo10gR1.ContainsKey(CurrentOracleVersion))
                    return true;
                return false;
            }
            i = NewOracleVersion.IndexOf("9.2.0");
            if (i != -1) // match
            {
                if (ConnTo9iR2.ContainsKey(CurrentOracleVersion))
                    return true;
                return false;
            }
            return false;
        }
        public Boolean CheckConnectStatus(String NewOracleVersion, String CurrentOracleVersion, String Status)
        {
            // 現在利用しているOracle Clientがそのまま利用できるか調べる
            // そのまま接続可能な場合、パラメータで指定されたStatus("YES"|"ES"|"WAS")と同じかどうかを比較
            int i;
            i = NewOracleVersion.IndexOf("12.1.0");
            if (i != -1) // match
            {
                if (ConnTo12cR1.ContainsKey(CurrentOracleVersion))
                {
                    if (ConnTo12cR1[CurrentOracleVersion] == Status)
                        return true;
                    return false;
                }
                return false;
            }
            i = NewOracleVersion.IndexOf("11.2.0");
            if (i != -1) // match
            {
                if (ConnTo11gR2.ContainsKey(CurrentOracleVersion))
                {
                    if (ConnTo11gR2[CurrentOracleVersion] == Status)
                        return true;
                    return false;
                }
                return false;
            }
            i = NewOracleVersion.IndexOf("11.1.0");
            if (i != -1) // match
            {
                if (ConnTo11gR1.ContainsKey(CurrentOracleVersion))
                {
                    if (ConnTo11gR1[CurrentOracleVersion] == Status)
                        return true;
                    return false;
                }
                return false;
            }
            i = NewOracleVersion.IndexOf("10.2.0");
            if (i != -1) // match
            {
                if (ConnTo10gR2.ContainsKey(CurrentOracleVersion))
                {
                    if (ConnTo10gR2[CurrentOracleVersion] == Status)
                        return true;
                    return false;
                }
                return false;
            }
            i = NewOracleVersion.IndexOf("10.1.0");
            if (i != -1) // match
            {
                if (ConnTo10gR1.ContainsKey(CurrentOracleVersion))
                {
                    if (ConnTo10gR1[CurrentOracleVersion] == Status)
                        return true;
                    return false;
                }
                return false;
            }
            i = NewOracleVersion.IndexOf("9.2.0");
            if (i != -1) // match
            {
                if (ConnTo9iR2.ContainsKey(CurrentOracleVersion))
                {
                    if (ConnTo9iR2[CurrentOracleVersion] == Status)
                        return true;
                    return false;
                }
                return false;
            }
            return false;
        }

        public Boolean Is9iUpper(String OracleVersionStrings)
        {
            String CurrentVersionStrings;
            CurrentVersionStrings = GetVersionNumberString(OracleVersionStrings);
            switch (CurrentVersionStrings)
            {
                case "":
                case "8.1.7.4":
                case "8.1.7.0":
                case "8.1.6.0":
                case "8.1.5.0":
                case "8.0.6.0":
                    return false;
                default:
                    return true;
            }
        }
        public Boolean IsEE(String EditionStrings)
        {
            int i;
            i = EditionStrings.IndexOf("Enterprise Edition");
            if (i == -1) // not match
                return false;
            else
                return true;
        }
        public String ReturnMajorRelease(String OracleVersionStrings)
        {
            String[] MajorVersions = {
                "8.0",
                "8.1",
                "9.0",
                "9.2",
                "10.1",
                "10.2",
                "11.1",
                "11.2",
                "12.1",
            };
            String CurrentVersionStrings;
            int i;

            CurrentVersionStrings = GetVersionNumberString(OracleVersionStrings);
            if (CurrentVersionStrings == "")
                return "";

            foreach (String str in MajorVersions)
            {
                i = CurrentVersionStrings.IndexOf(str);
                if (i != -1) // match
                    return str;
            }
            return "";
        }
    }
}
