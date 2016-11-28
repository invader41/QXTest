using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QXTest.Model;
using System.IO;

namespace QXTest
{
    public class QXReader
    {
        public static QX ReadQXFile(string filePath, out Exception exception)
        {
            exception = null;
            if (File.Exists(filePath))
            {
                //解析谱文件
                try
                {
                    QX qx = new QX();


                    FileInfo info = new FileInfo(filePath);
                    qx.FileName = info.Name.TrimEnd(new char[]{'.','q','x'});
                    qx.LegendName = qx.FileName;
                    qx.FileData = new List<QXItem>();

                    FileStream fs = new FileStream(filePath, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string data = sr.ReadToEnd();

                    sr.Close();
                    fs.Close();
                    

                    if (string.IsNullOrEmpty(data))
                    {
                        exception = new Exception("文件为空");
                        return null;
                    }

                    StringBuilder str = new StringBuilder();
                    string[] dataArry = data.Split(new char[] { '\n' });
                    int splitIndex = 0;
                    for (int i = 0; i <= dataArry.Length; i++)
                    {
                        if (dataArry[i].Trim('\r') == "")
                        {
                            splitIndex = i;
                            break;
                        }
                        else
                        {
                            qx.FileData.Add(new QXItem { Key = i + 1, Value = Convert.ToInt32(dataArry[i].Trim('\r')) });
                        }
                    }
                    if (splitIndex == 0)
                    {
                        exception = new Exception("数据为空");
                        return null;
                    }

                    qx.Ratio = Convert.ToInt32(dataArry[splitIndex + 1].Trim('\r'));
                    qx.Datum = Convert.ToInt32(dataArry[splitIndex + 2].Trim('\r'));
                    qx.Distance = Convert.ToInt32(dataArry[splitIndex + 3].Trim('\r'));
                    qx.Run = Convert.ToInt32(dataArry[splitIndex + 4].Trim('\r'));
                    qx.Model = Convert.ToInt32(dataArry[splitIndex + 5].Trim('\r'));
                    qx.SamplingDelay = Convert.ToInt32(dataArry[splitIndex + 6].Trim('\r'));
                    qx.SamplingCount = Convert.ToInt32(dataArry[splitIndex + 7].Trim('\r'));
                    qx.LightenTime = Convert.ToInt32(dataArry[splitIndex + 8].Trim('\r'));
                    qx.Param6 = Convert.ToInt32(dataArry[splitIndex + 9].Trim('\r'));
                    qx.CompleteDelay = Convert.ToInt32(dataArry[splitIndex + 10].Trim('\r'));
                    qx.RunDistance = Convert.ToInt32(dataArry[splitIndex + 11].Trim('\r'));

                    return qx;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    return null;
                }
            }
            else
            {
                exception = new Exception("文件不存在");
                return null;
            }
        }
    }
}
