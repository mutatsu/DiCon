using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;


namespace DiCon
{
    public class EditWord
    {
        private string docFile;
        private string tmpFile;
        //コンストラクタ
        public EditWord()
        {
        }

        // プロパティ

        // 読み込むDocファイル用
        public string DocFile
        {
            get { return docFile; }
            set { docFile = value; }
        }
        public string TmpFile
        {
            get { return tmpFile; }
            set { tmpFile = value; }
        }

        private void searchWordReplaceAll(
            Word.Application objWord, 
            string findStr, 
            string replaceStr)
        {
            object oFtxt = findStr;
            object oRtxt = replaceStr;
            object oMis = System.Reflection.Missing.Value;
            object oTru = true;
            object oFal = false;
            object oRpl = 2 ; // wdReplaceNone = 0, wdReplaceOne = 1, wdReplaceAll = 2
            //object oCidx = 1; // wdAuto = 0, wdBlack=1 wdRed=6

            object[] Parameters;
            Parameters = new object[15];
            Parameters[0] =  oFtxt;  // FindText
            Parameters[1] =  oMis;   // MatchCase
            Parameters[2] =  oTru;   // MatchWholeWord
            Parameters[3] =  oMis;   // MatchWildcards
            Parameters[4] =  oMis;   // MatchSoundsLike
            Parameters[5] =  oMis;   // MatchAllWordForms
            Parameters[6] =  oTru;   // Forward
            Parameters[7] =  oMis;   // Wrap
            Parameters[8] =  oMis;   // Format
            Parameters[9] =  oRtxt;  // ReplaceWith
            Parameters[10] = oRpl;   // Replace
            Parameters[11] = oMis;   // MatchKashida
            Parameters[12] = oMis;   // MatchDiacritics
            Parameters[13] = oMis;   // MatchAlefHamza
            Parameters[14] = oMis;   // MatchControl
            objWord.Selection.Find.GetType().InvokeMember(
                "Execute", 
                System.Reflection.BindingFlags.InvokeMethod,
                null,
                objWord.Selection.Find,
                Parameters
                );
        }

        private void searchWord(
            Word.Range objRange,
            string findStr,
            ref long startPos,
            ref long endPos
            )
        {
            Word.Find objFind = objRange.Find;
            // objFind.ClearFormatting();
            object oFtxt = findStr;
            object oMis = System.Reflection.Missing.Value;
            object oTru = true;
            object oFal = false;

            // Console.WriteLine("Start: {0}", objRange.Start);
            // Console.WriteLine("End: {0}", objRange.End);

            object[] Parameters;
            Parameters = new object[15];
            Parameters[0] = oFtxt;  // FindText
            Parameters[1] = oMis;   // MatchCase
            Parameters[2] = oTru;   // MatchWholeWord
            Parameters[3] = oMis;   // MatchWildcards
            Parameters[4] = oMis;   // MatchSoundsLike
            Parameters[5] = oMis;   // MatchAllWordForms
            Parameters[6] = oTru;   // Forward
            Parameters[7] = oMis;   // Wrap
            Parameters[8] = oMis;   // Format
            Parameters[9] = oMis;   // ReplaceWith
            Parameters[10] = oMis;   // Replace
            Parameters[11] = oMis;   // MatchKashida
            Parameters[12] = oMis;   // MatchDiacritics
            Parameters[13] = oMis;   // MatchAlefHamza
            Parameters[14] = oMis;   // MatchControl

            bool bFound = (bool) objFind.GetType().InvokeMember(
                "Execute",
                System.Reflection.BindingFlags.InvokeMethod,
                null,
                objFind,Parameters
                );

            // objFind.Execute(
            //     oFtxt,  // FindText
            //     oMis,   // MatchCase
            //     oTru,   // MatchWholeWord
            //     oMis,   // MatchWildcards
            //     oMis,   // MatchSoundsLike
            //     oMis,   // MatchAllWordForms
            //     oTru,   // Forward
            //     oMis,   // Wrap
            //     oMis,   // Format
            //     oMis,   // ReplaceWith
            //     oMis,   // Replace
            //     oMis,   // MatchKashida
            //     oMis,   // MatchDiacritics
            //    oMis,   // MatchAlefHamza
            //    oMis   // MatchControl
            //    );
            // if (objFind.Found == true)
            // {
            //     startPos = objRange.Start;
            //     endPos = objRange.End;
            // }
            // else
            // {
            //     startPos = 0;
            //     endPos = 0;
            // }

            if (bFound == true)
            {
                // Console.WriteLine("True");
                startPos = objRange.Start;
                endPos = objRange.End;
            }
            else
            {
                // Console.WriteLine("False");
                startPos = 0;
                endPos = 0;
            }
            // Console.WriteLine("Start: {0}", objRange.Start);
            // Console.WriteLine("End: {0}", objRange.End);    
        }

        private bool cutBetweenWords(
            Word.Document objDocument,
            string fromStr,
            string toStr
            )
        {
            long fromStart = 0; // searchWord で使う
            long fromEnd = 0;   // searchWord で使う
            long toStart = 0;   // searchWord で使う
            long toEnd = 0;     // searchWord で使う
            long wStart = 0;
            long wEnd = 0;
            object oStartPos;
            object oEndPos;

            Word.Range objRange = objDocument.Range();
            wStart = objRange.Start;
            wEnd = objRange.End;

            searchWord(objRange, fromStr, ref fromStart, ref fromEnd);
            if ((fromStart == 0) && (fromEnd == 0))
                return false;

            oStartPos = fromEnd;
            oEndPos = wEnd;
            objRange = objDocument.Range(
                ref oStartPos,  // Start
                ref oEndPos     // End
                );
            searchWord(objRange, toStr, ref toStart, ref toEnd);
            if ((toStart == 0) && (toEnd == 0))
                return false;

            //Console.WriteLine("{0}:{1} {2}:{3}",fromStart, fromEnd, toStart,toEnd);

            oStartPos = fromStart;
            oEndPos = toEnd;
            objRange = objDocument.Range(
                ref oStartPos,  // Start
                ref oEndPos     // End
                );  // fromStrのstart と toStrのend の間を選択
            objRange.Cut();

            return true;
        }

        private void cutBetweenWordsAll(
            Word.Document objDocument,
            string fromStr,
            string toStr
            )
        {
            if (cutBetweenWords(objDocument, fromStr, toStr))
            {
                cutBetweenWordsAll(objDocument, fromStr, toStr); // 再帰処理
            }
        }

        public void Edit(
            Dictionary<string, string> replaceKeywordDic, 
            Dictionary<string, bool> areacutKeywordDic)
        {
            Word.Application objWord = null;
            Word.Document objDocument = null;
            object oMis = System.Reflection.Missing.Value;
            object oTru = true;
            object oFal = false;
            object oPth;
            object oFmt;

            try
            {
                // ワード起動
                objWord = new Word.Application();

                // ワード非表示
                objWord.Application.Visible = false;
                objWord.Application.DisplayAlerts =
                    Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;

                // ワードファイルを開く
                oPth = tmpFile;
                objDocument = objWord.Documents.Open(
                    ref oPth,   // FileName
                    ref oMis,   // ConfirmConversions
                    ref oFal,   // ReadOnly
                    ref oMis,   // AddToRecentFiles
                    ref oMis,   // PasswordDocument
                    ref oMis,   // PasswordTemplate
                    ref oMis,   // Revert
                    ref oMis,   // WritePasswordDocument
                    ref oMis,   // WritePasswordTemplate
                    ref oMis,   // Format
                    ref oMis,   // Encoding
                    ref oMis,   // Visible
                    ref oMis,   // OpenAndRepair
                    ref oMis,   // DocumentDirection
                    ref oMis,   // NoEncodingDialog
                    ref oMis    // XMLTransform
                    );
                // ------------------------------------------

                try
                {
                    foreach (KeyValuePair<string, string> kvp in replaceKeywordDic)
                    {
                        // Console.WriteLine("{0} : {1}", kvp.Key, kvp.Value);
                        // searchWordReplaceAll(objDocument, "$" + kvp.Key + "$", kvp.Value);
                        if (kvp.Value.Length >= 256)
                            searchWordReplaceAll(objWord, "$" + kvp.Key + "$", @"【注意】ヒアリングシートを確認してください。文字列が長すぎます。");
                        else
                            searchWordReplaceAll(objWord, "$" + kvp.Key + "$", kvp.Value);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                try
                {
                    foreach (KeyValuePair<string, bool> kvp in areacutKeywordDic)
                    {
                        if (kvp.Value == false)
                        {
                            // Console.WriteLine("{0}", kvp.Key);
                            // cutBetweenWordsAll(objWord, objDocument, "$" + kvp.Key + "_START$", "$" + kvp.Key + "_END$");
                            cutBetweenWordsAll(objDocument, "$" + kvp.Key + "_START$", "$" + kvp.Key + "_END$");
                            searchWordReplaceAll(objWord, "$" + kvp.Key + "_START$", ""); // 空文字に変換
                            searchWordReplaceAll(objWord, "$" + kvp.Key + "_END$", ""); // 空文字に変換
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }


                // ------------------------------------------
                // ワードファイルの保存
                if (tmpFile == docFile)
                {
                    objDocument.Save();
                }
                else
                {
                    oPth = docFile;
                    oFmt = 0; // MS-Word Format
                    objDocument.SaveAs(
                        ref oPth,   // FileName
                        ref oFmt,   // FileFormat
                        ref oMis,   // LockComments
                        ref oMis,   // Password
                        ref oMis,   // AddToRecentFiles
                        ref oMis,   // WritePassword
                        ref oMis,   // ReadOnlyRecommended
                        ref oMis,   // EmbedTrueTypeFonts
                        ref oMis,   // SaveNativePictureFormat
                        ref oMis,   // SaveFormsData
                        ref oMis,   // SaveAsAOCELetter
                        ref oMis,   // Encoding
                        ref oMis,   // InsertLineBreaks
                        ref oMis,   // AllowSubstitutions
                        ref oMis,   // LineEnding
                        ref oMis    // AddBiDiMarks
                        );
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // ワード終了処理
                if (objDocument != null)
                {
                    objDocument.Close(
                        ref oMis,   // SaveChanges
                        ref oMis,   // OriginalFormat
                        ref oMis    // RouteDocument
                        );      // ワードファイルを閉じる
                    Marshal.ReleaseComObject(objDocument);  // オブジェクト参照を解放
                    objDocument = null;                     // オブジェクト解放
                }
                if (objWord != null)
                {
                    objWord.Quit(
                        ref oMis,   // SaveChanges
                        ref oMis,   // OriginalFormat
                        ref oMis    // RouteDocument
                        );      // ワードを終了する
                    Marshal.ReleaseComObject(objWord);      // オブジェクト参照を解放
                    objWord = null;                         // オブジェクト解放
                }
            }
        }
    }
}
