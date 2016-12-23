using GalaSoft.MvvmLight;
using QXTest.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using System.IO;
using Infragistics.Controls.Charts;
using System.Windows.Media;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;

namespace QXTest.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            QXList = new List<QX>();
            Logs = new List<string>();
            SelectedQXList = new ObservableCollection<QX>();
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }

        private List<QX> _QXList;
        public List<QX> QXList
        {
            get { return _QXList; }
            set
            {
                _QXList = value;
                RaisePropertyChanged("QXList");
            }
        }

        private ObservableCollection<QX> _selectedQXList;
        public ObservableCollection<QX> SelectedQXList
        {
            get { return _selectedQXList; }
            set
            {
                _selectedQXList = value;
                RaisePropertyChanged("SelectedQXList");
            }
        }

        private ObservableCollection<QX> _selectedChartQXList;
        public ObservableCollection<QX> SelectedChartQXList
        {
            get { return _selectedChartQXList; }
            set
            {
                _selectedChartQXList = value;
                RaisePropertyChanged("SelectedChartQXList");
            }
        }

        private List<string> _logs;
        public List<string> Logs
        {
            get { return _logs; }
            set
            {
                _logs = value;
                RaisePropertyChanged("Logs");
            }
        }

        private bool _selectChartAll;
        public bool SelectChartAll
        {
            get { return _selectChartAll; }
            set
            {
                if (value)
                    this.SelectedChartQXList = new ObservableCollection<QX>(QXList);
                else
                    this.SelectedChartQXList.Clear();
                _selectChartAll = value;
                RaisePropertyChanged("SelectChartAll");
            }
        }

        private bool _selectAll;
        public bool SelectAll
        {
            get { return _selectAll; }
            set
            {
                if (value)
                    this.SelectedQXList = new ObservableCollection<QX>(QXList);
                else
                    this.SelectedQXList.Clear();
                _selectAll = value;
                RaisePropertyChanged("SelectAll");
            }
        }

        private RelayCommand _selectFileCommand;
        public RelayCommand SelectFileCommand
        {
            get
            {
                if (_selectFileCommand == null)
                    _selectFileCommand = new RelayCommand(SelectFile);
                return _selectFileCommand;
            }
        }

        private RelayCommand _selectDirectoryCommand;
        public RelayCommand SelectDirectoryCommand
        {
            get
            {
                if (_selectDirectoryCommand == null)
                    _selectDirectoryCommand = new RelayCommand(SelectDirectory);
                return _selectDirectoryCommand;
            }
        }

        private RelayCommand _computeCommand;
        public RelayCommand ComputeCommand
        {
            get
            {
                if (_computeCommand == null)
                    _computeCommand = new RelayCommand(delegate
                    {
                        List<QXComputeResult> QXComputeResultList = QXComputer.ComputeQX(SelectedQXList, Precision, AtRangeMax, AtRangeMin, AcRangeMax, AcRangeMin);
                        List<QXDataGridItem> items = new List<QXDataGridItem>();
                        foreach (QXComputeResult computeResult in QXComputeResultList)
                        {
                            QXDataGridItem item = new QXDataGridItem
                            {
                                FileName = computeResult.FileName,
                                AcCrest = computeResult.AcCrest.ToString(),
                                AtCrest = computeResult.AtCrest.ToString(),
                                AcIPIV2 = computeResult.AcIPIV2.ToString(),
                                AtIPIV2 = computeResult.AtIPIV2.ToString(),
                                SumC = computeResult.SumC.ToString(),
                                SumT = computeResult.SumT.ToString(),
                                TroughOfWave = computeResult.TroughOfWave.ToString(),
                                AtAc = computeResult.AtAc.ToString()
                            };
                            items.Add(item);
                        };
                        this.QXDataGridItemList = items;
                    });
                return _computeCommand;
            }
        }

        private int _AtRangeMax = 320;
        public int AtRangeMax
        {
            get { return _AtRangeMax; }
            set
            {
                _AtRangeMax = value;
                RaisePropertyChanged("AtRangeMax");
            }
        }

        private int _AtRangeMin = 230;
        public int AtRangeMin
        {
            get { return _AtRangeMin; }
            set
            {
                _AtRangeMin = value;
                RaisePropertyChanged("AtRangeMin");
            }
        }

        private int _AcRangeMax = 150;
        public int AcRangeMax
        {
            get { return _AcRangeMax; }
            set
            {
                _AcRangeMax = value;
                RaisePropertyChanged("AcRangeMax");
            }
        }

        private int _AcRangeMin = 60;
        public int AcRangeMin
        {
            get { return _AcRangeMin; }
            set
            {
                _AcRangeMin = value;
                RaisePropertyChanged("AcRangeMin");
            }
        }

        private int _precision = 6;
        public int Precision
        {
            get { return _precision; }
            set
            {
                _precision = value;
                RaisePropertyChanged("Precision");
            }
        }

        private List<QXDataGridItem> _QXDataGridItemList;
        public List<QXDataGridItem> QXDataGridItemList
        {
            get
            {
                return _QXDataGridItemList;
            }
            set
            {
                _QXDataGridItemList = value;
                RaisePropertyChanged("QXDataGridItemList");
            }
        }

        private void SelectFile()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "QX文件(*.qx)|*.qx";
                dialog.Multiselect = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Messenger.Default.Send(true, "ClearChart");
                    List<QX> list = new List<QX>();
                    List<string> logs = new List<string>();
                    foreach (var item in dialog.FileNames)
                    {
                        Exception e;
                        QX qx = QXReader.ReadQXFile(item, out e);
                        if (e == null)
                        {
                            list.Add(qx);
                        }
                        else
                        {
                            logs.Add(e.Message);
                        }
                    }
                    QXList = list;
                    Logs = logs;
                    SelectAll = true;
                    this.Title = (new FileInfo(dialog.FileName)).Directory.Name;
                }
            }
        }

        private void SelectDirectory()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Messenger.Default.Send(true, "ClearChart");
                    DirectoryInfo dir = new DirectoryInfo(dialog.SelectedPath);
                    FileInfo[] fileList = dir.GetFiles("*.qx");

                    List<QX> list = new List<QX>();
                    List<string> logs = new List<string>();

                    foreach (var item in fileList)
                    {
                        Exception e;
                        QX qx = QXReader.ReadQXFile(item.FullName, out e);
                        if (e == null)
                        {
                            list.Add(qx);
                        }
                        else
                        {
                            logs.Add(e.Message);
                        }
                    }
                    QXList = list;
                    Logs = logs;
                    SelectAll = true;
                    this.Title = dir.Name;
                }
            }
        }
    }
}