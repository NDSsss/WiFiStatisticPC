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
using ZedGraph;

namespace ParseFile
{
    public partial class Form1 : Form
    {

        List<ParseInfo> parseInfo = new List<ParseInfo>();
        String rawFile = "";
        List<List<ParseInfo>> measurments;
        List<EniquePoint> eniquePoints;
        List<ObserverPair> observerPairs;
        int selectedMeasure = 0;
        Area area;

        //Полотно для рисования
        GraphPane pane;

        public Form1()
        {
            InitializeComponent();
            area = new Area();
            List<ObserverPoint> observerPoints = new List<ObserverPoint>();
            area.pointsForDraw = new List<PairedPointsForDraw>();
            area.observerPoints = observerPoints;
            pane = zedGraphControl.GraphPane;


            //пример как добавлять точки
//            DrawGraph(pane, 40, 60, 30);
//            DrawGraph(pane, 40, 40, 30);
//            DrawGraph(pane, 90, 40, 30);
//            DrawGraph(pane, 90, 40, 20);
//            DrawGraph(pane, 120, 40, 20);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile2();
            initObserverPair();
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
            string splitMeasure = "Time Stamp|SSID|BSSID|Strength|Primary Channel|Primary Frequency|Center Channel|Center Frequency|Width (Range)|Distance|Security";
            string[] stringmeasurmentsRawSeparators = new string[] { "Time Stamp|SSID|BSSID|Strength|Primary Channel|Primary Frequency|Center Channel|Center Frequency|Width (Range)|Distance|Security"};
            string[] measurmentsRaw = rawFile.Split(stringmeasurmentsRawSeparators, StringSplitOptions.None);
            ObserverPoint currentObserverPoint = new ObserverPoint();
            currentObserverPoint.posx = Int32.Parse(tbPosx.Text);
            currentObserverPoint.posy = Int32.Parse(tbPosy.Text);
            currentObserverPoint.measures = new List<OneMeasure>();
            int i = 1;
            for (; i < measurmentsRaw.Length; i++)
            { 
                OneMeasure oneMeasure = new OneMeasure();
                oneMeasure.points = new List<WiFiPoint>();
                string[] parseinfoRawSeparator = new string[] { "qqqqq" };
                string[] parseinfoRaw = measurmentsRaw[i].Split(parseinfoRawSeparator, StringSplitOptions.None);
                int k = 0;
                for (; k < parseinfoRaw.Length-1; k++)
                {
                    string[] s = parseinfoRaw[k].Split('|');

                    WiFiPoint wifiPoint = new WiFiPoint()
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
                    oneMeasure.points.Add(wifiPoint);
                }
                currentObserverPoint.measures.Add(oneMeasure);
            }
            if (area.observerPoints.Count == 0)
            {
                area.minMeasures = currentObserverPoint.measures.Count;
            }
            else
            {
                if(currentObserverPoint.measures.Count< area.minMeasures)
                {
                    area.minMeasures = currentObserverPoint.measures.Count;
                }
            }
            area.observerPoints.Add(currentObserverPoint);
            preparePointsForDraw();
        }

        private void preparePointsForDraw()
        {
            if(area.observerPoints.Count > 1)
            {
                area.pointsForDraw.Clear();
                for (int i = 1; i < area.observerPoints.Count; i++)
                {
                    ObserverPoint observerPointOne = area.observerPoints[i-1];
                    ObserverPoint observerPointTwo = area.observerPoints[i];
                    PairedPointsForDraw currentPointsForDraw = new PairedPointsForDraw();
                    currentPointsForDraw.measuresOfCommonPoints = new List<List<CommonPoint>>();
                    currentPointsForDraw.pointOnePosX = observerPointOne.posx;
                    currentPointsForDraw.pointOnePosY = observerPointOne.posy;
                    currentPointsForDraw.pointTwoPosX = observerPointTwo.posx;
                    currentPointsForDraw.pointTwoPosY = observerPointTwo.posy;
                    for (int measure = 0; measure < area.minMeasures; measure++)
                    {
                        List<WiFiPoint> pointOnePoints = observerPointOne.measures[measure].points;
                        List<WiFiPoint> pointTwoPoints = observerPointTwo.measures[measure].points;
                        List<CommonPoint> commonPointsForCurrentMeasure = new List<CommonPoint>();
                        for(int pointOneIterator = 0; pointOneIterator < pointOnePoints.Count; pointOneIterator++)
                        {
                            for (int pointTwoIterator = 0; pointTwoIterator < pointTwoPoints.Count; pointTwoIterator++)
                            {
                                if (!pointOnePoints[pointOneIterator].SSId.Equals("", StringComparison.InvariantCultureIgnoreCase)) { 
                                if (pointOnePoints[pointOneIterator].SSId.Equals(pointTwoPoints[pointTwoIterator].SSId, StringComparison.InvariantCultureIgnoreCase))
                                {
                                    CommonPoint commonPoint = new CommonPoint();
                                    commonPoint.ssid = pointOnePoints[pointOneIterator].SSId;
                                    commonPoint.metersToPointOne = Int32.Parse(pointOnePoints[pointOneIterator].Distance.Replace("~", "").Split('.')[0]);
                                    commonPoint.metersToPointTwo = Int32.Parse(pointTwoPoints[pointTwoIterator].Distance.Replace("~", "").Split('.')[0]);
                                    commonPointsForCurrentMeasure.Add(commonPoint);
                                }
                            }
                            }
                        }
                        currentPointsForDraw.measuresOfCommonPoints.Add(commonPointsForCurrentMeasure);
                    }
                    area.pointsForDraw.Add(currentPointsForDraw);
                }
                Console.WriteLine("Points Prepared for draw");
                Console.WriteLine("Points Sorted");
                List<int> cbSelectedMeasureDataSource = new List<int>();
                for (int cbSelectedMeasureDataSourceIndex = 0; cbSelectedMeasureDataSourceIndex < area.minMeasures; cbSelectedMeasureDataSourceIndex++)
                {
                    cbSelectedMeasureDataSource.Add(cbSelectedMeasureDataSourceIndex);
                }
                cbSelectedMeasure.DataSource = cbSelectedMeasureDataSource;
                
            }
        }

        private void drawPoints()
        {
            for(int pairedPointsIterator = 0; pairedPointsIterator< area.pointsForDraw.Count; pairedPointsIterator++)
            {
                PairedPointsForDraw currentPairedPointsForDraw = area.pointsForDraw[pairedPointsIterator];
                if (currentPairedPointsForDraw.measuresOfCommonPoints[selectedMeasure].Count > 0)
                {
                    for (int pointsIterator = 0; pointsIterator < currentPairedPointsForDraw.measuresOfCommonPoints[selectedMeasure].Count; pointsIterator++)
                    {
                        DrawGraph(pane,
                            currentPairedPointsForDraw.pointOnePosX,
                            currentPairedPointsForDraw.pointOnePosY,
                            currentPairedPointsForDraw.measuresOfCommonPoints[selectedMeasure][pointsIterator].metersToPointOne, pairedPointsIterator);
                        DrawGraph(pane,
                            currentPairedPointsForDraw.pointTwoPosX,
                            currentPairedPointsForDraw.pointTwoPosY,
                            currentPairedPointsForDraw.measuresOfCommonPoints[selectedMeasure][pointsIterator].metersToPointTwo, pairedPointsIterator);
                    }
                } else
                {
                    DrawGraph(pane,
                            currentPairedPointsForDraw.pointOnePosX,
                            currentPairedPointsForDraw.pointOnePosY,
                            1, pairedPointsIterator);
                    DrawGraph(pane,
                            currentPairedPointsForDraw.pointTwoPosX,
                            currentPairedPointsForDraw.pointTwoPosY,
                            1, pairedPointsIterator);
                }
            }
            Console.WriteLine("Points draw");
            bindDataGridView();
        }

        private Color getColorByPosition(int position)
        {
            switch (position % 10)
            {
                case 0:
                    return Color.Black;
                case 1:
                    return Color.Yellow;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Blue;
                case 4:
                    return Color.Black;
                case 5:
                    return Color.Yellow;
                case 6:
                    return Color.Green;
                case 7:
                    return Color.Blue;
                case 8:
                    return Color.Black;
                case 9:
                    return Color.Yellow;
                default:
                    return Color.Green;
            }
        }

        private void bindDataGridView()
        {
            int maxPoints = 0;
            DataTable dataTable = new DataTable();
            for(int i = 0; i < area.observerPoints.Count; i++)
            {
                if(area.observerPoints[i].measures[selectedMeasure].points.Count > maxPoints)
                {
                    maxPoints = area.observerPoints[i].measures[selectedMeasure].points.Count;
                }
                string column = "Point" + i.ToString();
                dataTable.Columns.Add(column);
            }

            for(int rowIterator =0; rowIterator < maxPoints; rowIterator++)
            {
                DataRow _ravi = dataTable.NewRow();
                for(int columnIterator =0; columnIterator < area.observerPoints.Count; columnIterator++)
                {
                    if (rowIterator < area.observerPoints[columnIterator].measures[selectedMeasure].points.Count)
                    {
                        _ravi[columnIterator] = area.observerPoints[columnIterator].measures[selectedMeasure].points[rowIterator].SSId;
                    } else
                    {
                        _ravi[columnIterator] = "";
                    }
                }
                dataTable.Rows.Add(_ravi);
            }

            dataGridView1.DataSource = dataTable;
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

                            //bool isUnique = true;
                            //for (int uniquePointsIterator = 0; uniquePointsIterator < eniquePoints.Count; uniquePointsIterator++)
                            //{
                            //    if(eniquePoints[uniquePointsIterator].SSId == currentParseInfo.SSId)
                            //    {
                            //        eniquePoints[uniquePointsIterator].timesUsed++;
                            //        isUnique = false;
                            //        break;
                            //    }
                            //}
                            //if (isUnique)
                            //{
                            //   EniquePoint eniquePoint = new EniquePoint();
                            //   eniquePoint.SSId = currentParseInfo.SSId;
                            //   eniquePoint.timesUsed = 1;
                            //    eniquePoints.Add(eniquePoint);
                            //}
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

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }
        
       
        /// <summary>
        /// Рисует круг по заданному радиусу и начальным координатам
        /// </summary>
        /// <param name="X0">Координата центра Х.</param>
        /// <param name="Y0">Координата центра Y.</param>
        /// <param name="Radius">Радиус.</param>
        private void DrawGraph(GraphPane pane, int X0, int Y0, int Radius, int position)
        {
            //GraphPane pane = zedGraphControl.GraphPane;

            // Очистим список кривых             
            // pane.CurveList.Clear();

            // Создадим список точек  
            PointPairList pointsOfTheCircle = new PointPairList();
            PointPairList X0Y0 = new PointPairList();

            //блок настройки сетки
            //на случай если нужно
            //pane.XAxis.ScaleFontSpec.Angle = 90;
            //pane.YAxis.Type = AxisType.Log;
            //pane.XAxis.IsOmitMag = true;
            //pane.XAxis.Title = "E"; ///подписи
            //pane.CurveList.Clear();
            //pane.XAxis.IsShowGrid = true;
            //pane.XAxis.GridDashOn = 10;
            //pane.XAxis.GridDashOff = 5;
            //pane.YAxis.IsShowGrid = true;
            //pane.YAxis.GridDashOn = 10;
            //pane.YAxis.GridDashOff = 5;
            //pane.YAxis.IsShowMinorGrid = true;
            //pane.YAxis.MinorGridDashOn = 1;
            //pane.YAxis.MinorGridDashOff = 2;
            //pane.XAxis.IsShowMinorGrid = true;
            //pane.XAxis.MinorGridDashOn = 1;
            //pane.XAxis.MinorGridDashOff = 2;
            //блок настройки сетки

            X0Y0.Add(X0, Y0);

            // Заполняем список точек 
            for (int i = 0; i < 360; i++)// 360 градусов окружность
            {
                // добавим в список точку                 
                pointsOfTheCircle.Add(Fx(i, X0, Radius), Fy(i, Y0, Radius));
            }

            // Создадим кривую             
            LineItem myCurve = pane.AddCurve("", pointsOfTheCircle, getColorByPosition(position), SymbolType.None);
            LineItem myCurve2 = pane.AddCurve("", X0Y0, Color.Red, SymbolType.Star);
            
            //настройки для начала координат
            myCurve2.Line.IsVisible = false;
            myCurve2.Symbol.Fill.Color = Color.Red;
            myCurve2.Symbol.Fill.Type = FillType.Solid;
            myCurve2.Symbol.Size = 5.0f;

            zedGraphControl.AxisChange();
            // Обновляем график             
            zedGraphControl.Invalidate();

        }

        private double Fx(double u, int X0, int R)
        {
            return X0 + R * Math.Cos(u * Math.PI / 180);
        }
        private double Fy(double u, int Y0, int R)
        {
            return Y0 + R * Math.Sin(u * Math.PI / 180);
        }

        private void zedGraphControl_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cbSelectedMeasure_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedMeasure = cbSelectedMeasure.SelectedIndex;
            pane.CurveList.Clear();
            drawPoints();
            Console.WriteLine("Index changed");
        }
    }
        
}
