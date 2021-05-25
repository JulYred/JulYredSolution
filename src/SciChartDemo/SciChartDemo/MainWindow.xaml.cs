using System;
using System.Windows;
using System.Windows.Threading;
using SciChart.Charting.ChartModifiers;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.Annotations;
using SciChart.Charting.Visuals.Axes;

namespace SciChartDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.Loaded += OnLoaded;

        }

        //private void OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    // Create XyDataSeries to host data for our charts
        //    var scatterData = new XyDataSeries<double, double>();
        //    var lineData = new XyDataSeries<double, double>();
        //    scatterData.SeriesName = "Cos(x)";
        //    lineData.SeriesName = "Sin(x)";
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        lineData.Append(i, Math.Sin(i * 0.1));
        //        scatterData.Append(i, Math.Cos(i * 0.1));

        //    }
        //    // Assign dataseries to RenderSeries
        //    LineSeries.DataSeries = lineData;
        //    ScatterSeries.DataSeries = scatterData;
        //    // Start a timer to update our data
        //    double phase = 0.0;
        //    var timer = new DispatcherTimer(DispatcherPriority.Render);
        //    timer.Interval = TimeSpan.FromMilliseconds(10);
        //    timer.Tick += (s, e) =>
        //    {
        //        // SuspendUpdates() ensures the chart is frozen
        //        // while you do updates. This ensures best performance
        //        using (lineData.SuspendUpdates())
        //        {
        //            using var updates = scatterData.SuspendUpdates();
        //            for (int i = 0; i < 1000; i++)
        //            {
        //                // Updates the Y value at index i
        //                lineData.Update(i, Math.Sin(i    * 0.1 + phase));
        //                scatterData.Update(i, Math.Cos(i * 0.1 + phase));
        //            }
        //        }

        //        phase += 0.01;
        //    };
        //    timer.Start();
        //}
    }
}
