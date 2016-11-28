using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace QXTest.Model
{
    public class QXItem
    {
        [ReadOnly(true)]
        [PropertyOrder(0)]
        public int Key { get; set; }

        [PropertyOrder(1)]
        [ReadOnly(true)]
        public double Value { get; set; }
    }
}
