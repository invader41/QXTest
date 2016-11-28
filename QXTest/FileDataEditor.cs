using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.Windows.Controls;
using QXTest.Model;
using System.Windows.Data;

namespace QXTest
{
    public class FileDataEditor : ITypeEditor
    {

        public System.Windows.FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            DataGrid dg = new DataGrid();
            var _binding = new Binding("Value"); //bind to the Value property of the PropertyItem
            _binding.Source = propertyItem;
            _binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(dg, DataGrid.ItemsSourceProperty, _binding);

            return dg;
        }
    }
}
