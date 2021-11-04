using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TaskManagerInWpf.RelayCommands;

namespace TaskManagerInWpf.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public RelayCommand CreateBtnRelayCommand { get; set; }
        public RelayCommand SelectedProcessCommand { get; set; }
        public RelayCommand KillBtnClickCommand { get; set; }
        public RelayCommand AddBlackBoxCommand { get; set; }
        public ObservableCollection<Process> Processes { get; set; }
        private Process selectedprocess;

        public Process SelectedProcess
        {
            get { return selectedprocess; }
            set { selectedprocess = value; }
        }
        public MainWindow mainWindow1 { get; set; }
        public MainWindowViewModel(MainWindow mainWindow)
        {
            mainWindow1 = mainWindow;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Start();





            CreateBtnRelayCommand = new RelayCommand((sender) =>
            {
                if (mainWindow.BlackBoxListBox.Items == null)
                {
                    Process.Start(mainWindow.ProcessTextBox.Text);
                }
                else
                {
                    try
                    {
                        Process.Start(mainWindow.ProcessTextBox.Text);
                        Thread.Sleep(7000);
                        foreach (var item in mainWindow.BlackBoxListBox.Items)
                        {
                            var process = Processes.FirstOrDefault(p => p.ProcessName ==item.ToString());
                            process.Kill();
                        }
                        

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                }


            });

            KillBtnClickCommand = new RelayCommand((sender) =>
            {
                try
                {
                    if (SelectedProcess != null)
                    {
                        SelectedProcess.Kill();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            });
            AddBlackBoxCommand = new RelayCommand((sender) =>
            {

                mainWindow.BlackBoxListBox.Items.Add(mainWindow.BlackBoxTextBox.Text);

            });
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Processes = new ObservableCollection<Process>(Process.GetProcesses());
            mainWindow1.proceslistox.ItemsSource = Processes;

        }
    }

}
