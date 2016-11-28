using System.Windows;
using QXTest.ViewModel;
using QXTest.Model;
using System.Linq;
using System;
using Infragistics.Controls.Charts;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace QXTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<bool>(this, "ClearChart", m =>
                {
                    if (m)
                    {
                        dataChart.Series.Clear();
                    }

                });
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void fileList_ItemSelectionChanged(object sender, Xceed.Wpf.Toolkit.Primitives.ItemSelectionChangedEventArgs e)
        {
            var item = e.Item as QX;
            if (e.IsSelected)
            {
                Random rd = new Random(0);

                ScatterLineSeries lineSeries = new ScatterLineSeries();
                lineSeries.Title = item.LegendName;
                //lineSeries.Brush = new SolidColorBrush(Color.FromRgb(Convert.ToByte(rd.Next(255)), Convert.ToByte(rd.Next(255)), Convert.ToByte(rd.Next(255))));
                //lineSeries.Outline = new SolidColorBrush(Color.FromRgb(Convert.ToByte(rd.Next(255)), Convert.ToByte(rd.Next(255)), Convert.ToByte(rd.Next(255))));
                lineSeries.Thickness = 3;
                //lineSeries.ValueMemberPath = "Value";
                lineSeries.XMemberPath = "Key";
                lineSeries.YMemberPath = "Value";
                lineSeries.Tag = item.FileName;
                lineSeries.ToolTip = this.Resources["toolTip"];
                lineSeries.Legend = this.titleLegend;
                //lineSeries.LegendItemTemplate = this.Resources["LegendTemplate"] as DataTemplate;
                lineSeries.MarkerType = MarkerType.Circle;

                lineSeries.XAxis = xAxis;
                lineSeries.YAxis = yAxis;

                lineSeries.ItemsSource = item.FileData;
                dataChart.Series.Add(lineSeries);
            }
            else
            {
                var q = dataChart.Series.Where(s => s.Tag == item.FileName).ToList();
                if (q.Count() > 0)
                {
                    foreach (var series in q)
                    {
                        dataChart.Series.Remove(series);
                    }
                }
            }
        }

        //private void DataGrid_AutoGeneratingColumn(object sender, System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs e)
        //{
        //    var result = e.PropertyName;
        //    var p = (e.PropertyDescriptor as PropertyDescriptor).ComponentType.GetProperties().FirstOrDefault(x => x.Name == e.PropertyName);

        //    if (p != null)
        //    {
        //        var found = p.GetCustomAttributes(typeof(DisplayNameAttribute), true);
        //        if (found != null) result = (found[0] as DisplayNameAttribute).DisplayName;
        //    }

        //    e.Column.Header = result;
        //}


    }
}