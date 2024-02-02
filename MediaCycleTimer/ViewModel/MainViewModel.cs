using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MediaCycleTimer.Model;
using System.Collections.ObjectModel;

namespace MediaCycleTimer.ViewModel {
    public class MainViewModel : MainModel {
        public MainViewModel()
        {
            BtnWindows();
            SideTabs = new ObservableCollection<SideTabButton>();
            AddSideTabButton("Tab 1", new MediaViewModel());
            //AddSideTabButton("Tab 2", new ViewModel2());
        }
        private void AddSideTabButton(string buttonText, ViewModelBase viewModel) {
            var button = new RelayCommand(() => ChangeContent(viewModel));

            var sideTabButton = new SideTabButton {
                ButtonText = buttonText,
                Command = button
            };

            SideTabs.Add(sideTabButton);
        }

        private void ChangeContent(object parameter) {
            if (parameter is ViewModelBase newViewModel) {
                CurrentViewModel = newViewModel;
            }
        }
    }
}