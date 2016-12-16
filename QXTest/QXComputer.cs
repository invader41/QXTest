using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QXTest.Model;

namespace QXTest
{
    public class QXComputer
    {
        public static List<QXComputeResult> ComputeQX(IList<QX> data, int precision,int AtRangeMax, int AtRangeMin, int AcRangeMax, int AcRangeMin)
        {
            List<QXComputeResult> result = new List<QXComputeResult>();
            foreach (var item in data)
            {
                QXComputeResult r = new QXComputeResult();
                r.FileName = item.FileName;
                List<double> at = (from i in item.FileData
                               where i.Key >= AtRangeMin && i.Key <= AtRangeMax
                               select i.Value ).ToList();
                List<double> ac = (from i in item.FileData
                                where i.Key >= AcRangeMin && i.Key <= AcRangeMax
                                select i.Value).ToList();
                r.TroughOfWave = item.FileData.GetRange(AcRangeMax -1, AtRangeMin - AcRangeMax + 1 ).Select(s => s.Value).Min();
                r.AcCrest = ac.Max();
                r.AtCrest = at.Max();
                r.AcIPIV2 = (r.AcCrest + r.TroughOfWave) / 2;
                r.AtIPIV2 = (r.AtCrest + r.TroughOfWave) / 2;

                //int acrangeStartIndex = item.FileData.First(i => i.Value >= r.AcIPIV2).Key;
                //int acrangeEndIndex = item.FileData.GetRange(acrangeStartIndex - 1, item.FileData.Count - acrangeStartIndex + 1).First(i => i.Value < r.AcIPIV2).Key - 1;

                //List<QXItem> acrange = item.FileData.GetRange(acrangeStartIndex - 1, acrangeEndIndex - acrangeStartIndex + 1).ToList();

                List<QXItem> acrange = item.FileData.GetRange(AcRangeMin - 1, AcRangeMax - AcRangeMin + 1).Where(i => i.Value >= r.AcIPIV2).ToList();

                //int atrangeStartIndex = item.FileData.GetRange(AtRangeMin - 1, item.FileData.Count - AtRangeMin + 1).First(i => i.Value >= r.AtIPIV2).Key;
                //int atrangeEndIndex = item.FileData.GetRange(atrangeStartIndex - 1, item.FileData.Count - atrangeStartIndex + 1).First(i => i.Value < r.AtIPIV2).Key - 1;
                //List<QXItem> atrange = item.FileData.GetRange(atrangeStartIndex - 1, atrangeEndIndex - atrangeStartIndex + 1).ToList();
                List<QXItem> atrange = item.FileData.GetRange(AtRangeMin - 1, AtRangeMax - AtRangeMin + 1).Where(i => i.Value >= r.AtIPIV2).ToList();

                //List<decimal> atrangetmp = atrange.Select<QXItem, decimal>(q => { return q.Value - r.AtIPIV2; }).ToList();
                //List<decimal> acrangetmp = acrange.Select<QXItem, decimal>(q => { return q.Value - r.AcIPIV2; }).ToList();
                List<double> atrangetmp = atrange.Select<QXItem, double>(q => { return q.Value - r.TroughOfWave; }).ToList();
                List<double> acrangetmp = acrange.Select<QXItem, double>(q => { return q.Value - r.TroughOfWave; }).ToList();

                r.SumT = atrangetmp.Sum();
                r.SumC = acrangetmp.Sum();



                r.AtAc = Convert.ToDouble(Decimal.Round(Convert.ToDecimal(r.SumT / r.SumC), precision));

                //for (int i = atrangeStartIndex-1; i <= item.FileData.Count; i++)
                //{
                //    if (item.FileData[i].Value > r.AtIPIV2)
                //    {
                //        if (atrangeIndexFlag == 0 || item.FileData[i].Key == atrangeIndexFlag + 1)
                //        {
                //            atrange.Add(item.FileData[i].Value);
                //            atrangeIndexFlag = item.FileData[i].Key;
                //        }
                //    }
                //}

                
                result.Add(r);
            }
            return result;
        }
    }
}
