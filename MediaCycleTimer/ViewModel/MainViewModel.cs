using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MediaCycleTimer.Model;
using MediaCycleTimer.Utiles;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace MediaCycleTimer.ViewModel {
    public class MainViewModel : MainModel {
        public MainViewModel()
        {
            CurrentViewModel = _locator.MediaViewModel;
            BtnWindows();
            BtnSideTabs();
        }
        public void BtnSideTabs() {
            BtnMedia = new RelayCommand(() => { CurrentViewModel = _locator.MediaViewModel; });
            CaptureScreenCommand = new RelayCommand(() => { ViewCaptureInstance.Capture("Cature"); });
        }
    }
}