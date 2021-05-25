using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.Visuals;
using SciChart.Charting.Visuals.RenderableSeries;

namespace SciChartDemo
{
    ///<summary>
    ///     An Attached Behaviour that creates one FastLineRenderableSeries with random colour for each IRenderableSeriesViewModel passed in to the DataSeries property
    ///</summary>
    public class LineSeriesSource
    {
        public static readonly DependencyProperty DataSeriesProperty =
            DependencyProperty.RegisterAttached("DataSeries", typeof(IEnumerable<IRenderableSeriesViewModel>),
                                                typeof(LineSeriesSource),
                                                new PropertyMetadata(default(IEnumerable<IRenderableSeriesViewModel>),
                                                                     OnDataSeriesDependencyPropertyChanged));

        public static void SetDataSeries(UIElement element, IEnumerable<IRenderableSeriesViewModel> value)
        {
            element.SetValue(DataSeriesProperty, value);
        }

        public static IEnumerable<IRenderableSeriesViewModel> GetDataSeries(UIElement element)
        {
            return (IEnumerable<IRenderableSeriesViewModel>)element.GetValue(DataSeriesProperty);
        }

        private static void OnDataSeriesDependencyPropertyChanged(DependencyObject d,
                                                                  DependencyPropertyChangedEventArgs e)
        {
            var sciChartSurface = d as SciChartSurface;
            if (sciChartSurface == null) return;

            if (e.NewValue == null)
            {
                sciChartSurface.RenderableSeries.Clear();
                return;
            }

            using (sciChartSurface.SuspendUpdates())
            {
                sciChartSurface.RenderableSeries.Clear();

                var random = new Random();
                var itr = (IEnumerable<IRenderableSeriesViewModel>)e.NewValue;
                var renderSeries = new List<IRenderableSeries>();
                foreach (var dataSeries in itr)
                {
                    if (dataSeries == null) continue;

                    var rgb = new byte[3];
                    random.NextBytes(rgb);
                    var renderableSeries = new FastLineRenderableSeries()
                    {
                        AntiAliasing = true,
                        Stroke = Color.FromArgb(255, rgb[0], rgb[1], rgb[2]),
                        DataSeries = dataSeries.DataSeries,
                        IsVisible = dataSeries.IsVisible,
                        StrokeThickness = 1,
                    };

                    renderSeries.Add(renderableSeries);
                }

                sciChartSurface.RenderableSeries = new ObservableCollection<IRenderableSeries>(renderSeries);
            }
        }
    }
}


