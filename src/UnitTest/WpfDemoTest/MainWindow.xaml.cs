using System;
using System.Linq;
using System.Windows;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA3;

namespace WpfDemoTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TestSomething();
        }

        private void TestSomething()
        {
            var app = FlaUI.Core.Application.Launch(@"C:\Users\zhusa\Documents\GitHub\JulYredSolution\src\UnitTest\WpfDemo\bin\Debug\net5.0-windows\WpfDemo.exe");
            using (var automation = new UIA3Automation())
            {
                var window = app.GetMainWindow(automation);
                ConditionFactory cf = new ConditionFactory(new UIA3PropertyLibrary());

                var dd = window.FindAllChildren().FirstOrDefault(a => a.Name == "Click");
                dd?.Click();
            }
        }


    }
}
