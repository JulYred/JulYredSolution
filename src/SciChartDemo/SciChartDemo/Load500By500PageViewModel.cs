using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SciChart.Charting.Common.Helpers;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SciChart.Charting.ViewportManagers;
using SciChart.Examples.ExternalDependencies.Common;
using SciChart.Examples.ExternalDependencies.Data;

namespace SciChartDemo
{
    public class Load500By500PageViewModel : BaseViewModel
    {
        private DispatcherObservableCollection<string> _messages;
        private int _seriesCount;
        private int _pointCount;
        private bool _isBusy;
        private ObservableCollection<IRenderableSeriesViewModel> _dataSeries;

        private readonly IViewportManager _viewportManager = new DefaultViewportManager();

        public Load500By500PageViewModel()
        {
            SeriesCount = 500;
            PointCount = 500;

            Messages = new DispatcherObservableCollection<string>();

            RunExampleCommand = new ActionCommand(OnRunExample);
            VisibilityShowCommand = new ActionCommand(() =>
            {
                bool aa = !DataSeries.First().IsVisible;

                foreach (var item in DataSeries)
                {
                    item.IsVisible = aa;
                }
                DataSeries = new ObservableCollection<IRenderableSeriesViewModel>(DataSeries);
            });
        }

        private void UpdateExampleTitle()
        {
            OnPropertyChanged("ExampleTitle");
        }

        public ActionCommand RunExampleCommand { get; private set; }

        public IViewportManager ViewportManager
        {
            get { return _viewportManager; }
        }

        public int SeriesCount
        {
            get { return _seriesCount; }
            set
            {
                if (_seriesCount == value) return;
                _seriesCount = value;
                UpdateExampleTitle();
                OnPropertyChanged("SeriesCount");
            }
        }

        public int PointCount
        {
            get { return _pointCount; }
            set
            {
                if (_pointCount == value) return;
                _pointCount = value;
                UpdateExampleTitle();
                OnPropertyChanged("PointCount");
            }
        }

        public DispatcherObservableCollection<string> Messages
        {
            get { return _messages; }
            set
            {
                if (_messages == value) return;
                _messages = value;
                OnPropertyChanged("Messages");
            }
        }

        public ObservableCollection<IRenderableSeriesViewModel> DataSeries
        {
            get { return _dataSeries; }
            set
            {
                if (_dataSeries == value) return;
                _dataSeries = value;
                OnPropertyChanged("DataSeries");
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public void OnPageExit()
        {
            DataSeries = null;
        }

        public ActionCommand VisibilityShowCommand { get; set; }

        private void OnRunExample()
        {
            Task.Factory.StartNew(() =>
                {
                    DataSeries = null;
                    IsBusy = true;
                    var stopwatch = Stopwatch.StartNew();

                    // Generate Data and mark time                 
                    DoubleSeries[] xyData = new DoubleSeries[SeriesCount];
                    var generator = new RandomWalkGenerator(0d);
                    for (int i = 0; i < SeriesCount; i++)
                    {
                        xyData[i] = generator.GetRandomWalkSeries(PointCount);
                        generator.Reset();
                    }

                    stopwatch.Stop();

                    IsBusy = false;

                    // Append to SciChartSurface and mark time
                    stopwatch = Stopwatch.StartNew();
                    var allDataSeries = new IRenderableSeriesViewModel[SeriesCount];
                    for (int i = 0; i < SeriesCount; i++)
                    {
                        var dataSeries = new XyDataSeries<double, double>();
                        dataSeries.Append(xyData[i].XData, xyData[i].YData);
                        allDataSeries[i] = new LineRenderableSeriesViewModel { DataSeries = dataSeries, IsVisible = false };
                    }
                    DataSeries = new ObservableCollection<IRenderableSeriesViewModel>(allDataSeries);
                    stopwatch.Stop();

                    ViewportManager.AnimateZoomExtents(TimeSpan.FromMilliseconds(500));
                });
        }
    }
}

