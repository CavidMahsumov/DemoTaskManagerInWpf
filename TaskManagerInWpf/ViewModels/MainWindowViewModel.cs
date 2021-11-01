using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerInWpf.RelayCommands;

namespace TaskManagerInWpf.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public RelayCommand CreateBtnRelayCommand { get; set; }
        public ObservableCollection<Process> Processes { get; set; }
        public MainWindowViewModel(MainWindow mainWindow)
        {
            Processes= new ObservableCollection<Process>(Process.GetProcesses());
            
            CreateBtnRelayCommand = new RelayCommand((sender) =>
            {


            });
        }
    } 
    
}
