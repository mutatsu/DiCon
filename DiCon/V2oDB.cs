using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiCon
{
    class V2oDB
    {
        public V2oDB()
        {
        }
        public void setDics(
            Dictionary<string, string> hsDic,
            Dictionary<string, string> rkDic,
            Dictionary<string, bool> akDic
            )
        {
            V2oCheck ck = new V2oCheck();
            try
            {
                ck.UpgradeEnvCheck(hsDic);
                ck.ReplaceKeywordGenerate(hsDic, rkDic);
                ck.AreacutKeywordGenerate(hsDic, akDic);
            }
            catch (Exception e)
            {
                throw e;
            }
        }    
    }
}
