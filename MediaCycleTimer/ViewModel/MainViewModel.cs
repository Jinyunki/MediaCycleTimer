using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MediaCycleTimer.Model;
using System.Collections.ObjectModel;

namespace MediaCycleTimer.ViewModel {
    public class MainViewModel : MainModel {
        public MainViewModel()
        {
            CurrentViewModel = _locator.MediaViewModel;
            BtnWindows();
            BtnSideTabs();
        }
        
    }
}