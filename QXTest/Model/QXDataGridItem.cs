using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace QXTest.Model
{
     public class QXDataGridItem
    {
        [DisplayName("文件名")]
        public string FileName { get; set; }

        [DisplayName("Ac峰值")]
        public string AcCrest { get; set; }

        [DisplayName("At峰值")]
        public string AtCrest { get; set; }

        [DisplayName("波谷")]
        public string TroughOfWave { get; set; }

        [DisplayName("Ac(IP+IV)/2")]
        public string AcIPIV2 { get; set; }

        [DisplayName("At(IP+IV)/2")]
        public string AtIPIV2 { get; set; }

        [DisplayName("At面积")]
        public string SumT { get; set; }

        [DisplayName("Ac面积")]
        public string SumC { get; set; }

        [DisplayName("At/Ac")]
        public string AtAc { get; set; }
    }
}
