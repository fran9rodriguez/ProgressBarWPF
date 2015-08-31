using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.ViewModel;
using System.Threading;

namespace ProgressBarWPF.ViewModels
{

    /// <summary>
    /// Install-Package EnterpriseLibrary.Data
    /// </summary>
    public class MainViewModel : ViewModel, INotifyPropertyChanged
    {
        private BackgroundWorker _backgroundWorker;

        #region Events

        public event EventHandler TaskStarting = (s, e) => { };

        public event ProgressChangedEventHandler ProgressChanged
        {
            add { _backgroundWorker.ProgressChanged += value; }
            remove { _backgroundWorker.ProgressChanged -= value; }
        }

        public event RunWorkerCompletedEventHandler TaskCompleted
        {
            add { _backgroundWorker.RunWorkerCompleted += value; }
            remove { _backgroundWorker.RunWorkerCompleted -= value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Properties

        private ICommand _executeImportation;
        public ICommand ExecuteImportation
        {
            get
            {
                if (_executeImportation == null)
                {
                    _executeImportation = new DelegateCommand(param => _backgroundWorker.RunWorkerAsync());
                }
                return _executeImportation;
            }
        }

        #endregion

        public MainViewModel()
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.DoWork += executeTask;
        }

        private void executeTask(object sender, DoWorkEventArgs e)
        {
            OnTaskStarting();

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                _backgroundWorker.ReportProgress(i + 1);                
            }
        }

        private void OnTaskStarting()
        {
            TaskStarting(this, EventArgs.Empty);
        }


    }
}
