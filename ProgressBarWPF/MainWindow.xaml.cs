using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProgressBarWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModels.MainViewModel vm = new ViewModels.MainViewModel();
            vm.TaskStarting += TaskStarted;
            vm.ProgressChanged += ProgressChanged;
            vm.TaskCompleted += TaskCompleted;

            DataContext = vm;
        }

        void TaskStarted(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => ProgressPopup.IsOpen = true));
        }

        void TaskCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => ProgressPopup.IsOpen = false));
        }

        void ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => ProgressBar.Value = e.ProgressPercentage));
        }
    }
}
