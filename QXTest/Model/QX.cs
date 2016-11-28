using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace QXTest.Model
{
    public class QX: ObservableObject
    {
        [Category("基本信息")]
        [DisplayName("文件名")]
        [ReadOnly(true)]
        [PropertyOrder(0)]
        public string FileName { get; set; }

        [Category("基本信息")]
        [DisplayName("图例名")]
        [PropertyOrder(13)]
        public string LegendName { get; set; }

        [Category("数据列表")]
        [DisplayName("数据")]
        [PropertyOrder(12)]
        [Editor(typeof(FileDataEditor), typeof(FileDataEditor))]
        public List<QXItem> FileData { get; set; }

        [Category("基本信息")]
        [DisplayName("比例")]
        [ReadOnly(true)]
        [PropertyOrder(1)]
        public int Ratio { get; set; }

        [Category("基本信息")]
        [DisplayName("基准")]
        [ReadOnly(true)]
        [PropertyOrder(2)]
        public int Datum { get; set; }

        [Category("基本信息")]
        [DisplayName("距离")]
        [ReadOnly(true)]
        [PropertyOrder(3)]
        public int Distance { get; set; }

        [Category("基本信息")]
        [DisplayName("运行")]
        [ReadOnly(true)]
        [PropertyOrder(4)]
        public int Run { get; set; }

        [Category("基本信息")]
        [DisplayName("模式")]
        [ReadOnly(true)]
        [PropertyOrder(5)]
        public int Model { get; set; }

        [Category("基本信息")]
        [DisplayName("采样延时")]
        [ReadOnly(true)]
        [PropertyOrder(6)]
        public int SamplingDelay { get; set; }

        [Category("基本信息")]
        [DisplayName("采样次数")]
        [ReadOnly(true)]
        [PropertyOrder(7)]
        public int SamplingCount { get; set; }

        [Category("基本信息")]
        [DisplayName("点亮时间")]
        [ReadOnly(true)]
        [PropertyOrder(8)]
        public int LightenTime { get; set; }

        [Category("基本信息")]
        [DisplayName("完成延时")]
        [ReadOnly(true)]
        [PropertyOrder(9)]
        public int CompleteDelay { get; set; }

        [Category("基本信息")]
        [DisplayName("参数6")]
        [ReadOnly(true)]
        [PropertyOrder(10)]
        public int Param6 { get; set; }

        [Category("基本信息")]
        [DisplayName("运行距离")]
        [ReadOnly(true)]
        [PropertyOrder(11)]
        public int RunDistance { get; set; }
    }
}
