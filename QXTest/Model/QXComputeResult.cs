using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QXTest.Model
{
    public class QXComputeResult
    {
        [DisplayName("文件名")]
        public string FileName { get; set; }

        [DisplayName("Ac峰值")]
        public double AcCrest { get; set; }

        [DisplayName("At峰值")]
        public double AtCrest { get; set; }

        [DisplayName("波谷")]
        public double TroughOfWave { get; set; }

        [DisplayName("Ac(IP+IV)/2")]
        public double AcIPIV2 { get; set; }

        [DisplayName("At(IP+IV)/2")]
        public double AtIPIV2 { get; set; }

        [DisplayName("At面积")]
        public double SumT { get; set; }

        [DisplayName("Ac面积")]
        public double SumC { get; set; }

        [DisplayName("At/Ac")]
        public double AtAc { get; set; }
    }
}
