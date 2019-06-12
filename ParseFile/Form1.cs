using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParseFile
{
    public partial class Form1 : Form
    {

        List<ParseInfo> parseInfo = new List<ParseInfo>();
        String rawFile = "";
        List<List<ParseInfo>> measurments;
        List<EniquePoint> eniquePoints;
        List<ObserverPair> observerPairs;
        

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile2();
            initObserverPair();

            //dataGridView1.DataSource = parseInfo;
            //dfsgsdklhsgefuighudsafghfzdgh'sd[hf
        }
        private void initObserverPair()
        {
            observerPairs = new List<ObserverPair>();
            ObserverPair observerPair1 = new ObserverPair();
            observerPair1.observer1 = "Point 1";
            observerPair1.observer2 = "Point 2";
            observerPair1.from1to2 = 50;
            List<PairPoint> pairPoints1 = new List<PairPoint>();
            PairPoint pairPoint1 = new PairPoint();
            pairPoint1.ssid = "Pair 1";
            pairPoint1.toObserver1 = 35;
            pairPoint1.toObserver2 = 35;
            PairPoint pairPoint2 = new PairPoint();
            pairPoint2.ssid = "Pair 2";
            pairPoint2.toObserver1 = 45;
            pairPoint2.toObserver2 = 45;
            pairPoints1.Add(pairPoint1);
            pairPoints1.Add(pairPoint2);
            observerPairs.Add(observerPair1);
            ObserverPair observerPair2 = new ObserverPair();
            observerPair2.observer1 = "Point 2";
            observerPair2.observer2 = "Point 3";
            observerPair2.from1to2 = 30;
            List<PairPoint> pairPoints2 = new List<PairPoint>();
            PairPoint pairPoint3 = new PairPoint();
            pairPoint3.ssid = "Pair 3";
            pairPoint3.toObserver1 = 20;
            pairPoint3.toObserver2 = 20;
            PairPoint pairPoint4 = new PairPoint();
            pairPoint4.ssid = "Pair 4";
            pairPoint4.toObserver1 = 25;
            pairPoint4.toObserver2 = 25;
            pairPoints2.Add(pairPoint3);
            pairPoints2.Add(pairPoint4);
            observerPairs.Add(observerPair2);
            //fsdfsdfs
        }

        private void OpenFile2()
        {
            rawFile = "";
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Text Documents |*.txt",
                ValidateNames = true,
                Multiselect = false
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                           
                            while (!sr.EndOfStream)
                            {
                                //считываем строку
                                rawFile += sr.ReadLine();
                                
                            }
                            Console.WriteLine("фаил считан");
                            sortPoints();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        public void sortPoints()
        {
            eniquePoints = new List<EniquePoint>();
            measurments = new List<List<ParseInfo>>();
            string splitMeasure = "Time Stamp|SSID|BSSID|Strength|Primary Channel|Primary Frequency|Center Channel|Center Frequency|Width (Range)|Distance|Security";
            string[] stringmeasurmentsRawSeparators = new string[] { "Time Stamp|SSID|BSSID|Strength|Primary Channel|Primary Frequency|Center Channel|Center Frequency|Width (Range)|Distance|Security"};
            string[] measurmentsRaw = rawFile.Split(stringmeasurmentsRawSeparators, StringSplitOptions.None);
            int i = 1;
            for (; i < measurmentsRaw.Length; i++)
            { 
                List<ParseInfo> oneMeasurment = new List<ParseInfo>();
                string[] parseinfoRawSeparator = new string[] { "qqqqq" };
                string[] parseinfoRaw = measurmentsRaw[i].Split(parseinfoRawSeparator, StringSplitOptions.None);
                int k = 0;
                for (; k < parseinfoRaw.Length-1; k++)
                {
                    string[] s = parseinfoRaw[k].Split('|');

                    ParseInfo currentParseInfo = new ParseInfo()
                    {
                        timeStamp = s[0],
                        SSId = s[1],
                        BSSID = s[2],
                        Strength = s[3],
                        primaryChannel = s[4],
                        PrimaryFrequency = s[5],
                        centerChannel = s[6],
                        centerFrequency = s[7],
                        Range = s[8],
                        Distance = s[9],
                        Security = s[10]
                    };
                    oneMeasurment.Add(currentParseInfo);
                    bool isUnique = true;
                    for (int uniquePointsIterator = 0; uniquePointsIterator < eniquePoints.Count; uniquePointsIterator++)
                    {
                        if(eniquePoints[uniquePointsIterator].SSId == currentParseInfo.SSId)
                        {
                            eniquePoints[uniquePointsIterator].timesUsed++;
                            isUnique = false;
                            break;
                        }
                    }
                    if (isUnique)
                    {
                        EniquePoint eniquePoint = new EniquePoint();
                        eniquePoint.SSId = currentParseInfo.SSId;
                        eniquePoint.timesUsed = 1;
                        eniquePoints.Add(eniquePoint);
                    }
                }
                measurments.Add(oneMeasurment);
            }
            label1.Text = eniquePoints.Count.ToString();
            List<EniquePoint> eniquePointsSorted = eniquePoints.OrderBy(x => x.timesUsed).ToList();
            for (int o = eniquePointsSorted.Count -1; o >=0; o--)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = eniquePointsSorted[o].SSId;
                row.Cells[1].Value = eniquePointsSorted[o].timesUsed.ToString();
                dataGridView1.Rows.Add(row);
            }
        }

        public void OpenFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Text Documents |*.txt",
                ValidateNames = true,
                Multiselect = false
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            while (!sr.EndOfStream)
                            {
                                //считываем строку
                                string text = sr.ReadLine();

                                //убираем |qqqqq в начале строк
                                if (text.Contains("|qqqqq"))
                                {
                                    text = text.Remove(0, 6);
                                }

                                //пропускаем строку в которой заканчивается блок
                                if (text.Contains("---"))
                                {
                                    continue;
                                }

                                //отсекаем пустые строки и последнюю text.Length > 50
                                if (text.Trim(' ') != "" && text.Length > 40)
                                {
                                    string[] s = text.Split('|');

                                    parseInfo.Add(new ParseInfo()
                                    {
                                        timeStamp = s[0],
                                        SSId = s[1],
                                        BSSID = s[2],
                                        Strength = s[3],
                                        primaryChannel = s[4],
                                        PrimaryFrequency = s[5],
                                        centerChannel = s[6],
                                        centerFrequency = s[7],
                                        Range = s[8],
                                        Distance = s[9],
                                        Security = s[10]
                                    });
                                }
                            }
                            Console.WriteLine("фаил считан");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
        
}
