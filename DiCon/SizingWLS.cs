using System;
using System.Collections.Generic;
using System.Text;

namespace DiCon
{
    public class SizingWLS
    {
        public SizingWLS()
        {
        }
        public void Regress(ref double a, ref double b)
        {
            // 参考： http://www.geocities.jp/m_hiroi/light/pystat03.html
            //
            Dictionary<double, double> data = new Dictionary<double, double>() // 初期化
            {
                {  1, 0.000376917},
                {  3, 0.000449288},
                {  5, 0.000516402},
                { 10, 0.000676117},
                { 20, 0.000976771},
                { 50, 0.002036268},
                {100, 0.003981744},
                {250, 0.010406280}
            };

            double x = 0.0;
            double y = 0.0;
            double xm = 0.0;
            double ym = 0.0;
            double sx2 = 0.0;
            double sxy = 0.0;
            int i = 0;

            foreach (KeyValuePair<double, double> kvp in data)
            {
                x = kvp.Key;
                y = kvp.Value;

                i += 1;
                x -= xm;
                xm += x / i;
                sx2 += (i - 1) * x * x / i;
                y -= ym;
                ym += y / i;
                sxy += (i - 1) * x * y / i;
            }
            a = sxy / sx2;
            b = ym - a * xm;
            // Console.WriteLine("a:{0}, b:{1}", a, b);
        }
        public double CalcCPUs(
                double base_cint,
                double target_cint_peak,
                double target_cint_base,
                double peak_tpm,
                double complexity,
                double cpu_utilization
            )
        {
            double a = 0.0; // Regress()で使用
            double b = 0.0; // Regress()で使用
            double cpu_cost;
            double need_ref_cpu;
            double cpu_ratio;
            double target_cint_average;
            double need_cpus;
            
            Regress(ref a, ref b);
            cpu_cost = a * complexity + b;
            need_ref_cpu = (peak_tpm * cpu_cost) * (100.0 / cpu_utilization);

            target_cint_average = (target_cint_peak + target_cint_base) / 2;
            cpu_ratio = base_cint / target_cint_average;
            need_cpus = need_ref_cpu * cpu_ratio;
            return need_cpus;
        }
        public void setDics(
            Dictionary<string, string> hsDic,
            Dictionary<string, string> rkDic,
            Dictionary<string, bool> akDic
            )
        {

        }
    }
}
