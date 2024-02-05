using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediaCycleTimer.Model {
    public class MediaModel : ViewModelBase {
        public ICommand BtnFileOpen { get; set; }

        private string _selectedFilePath;
        public string SelectedFilePath {
            get { return _selectedFilePath; }
            set {
                if (_selectedFilePath != value) {
                    _selectedFilePath = value;
                    RaisePropertyChanged(nameof(SelectedFilePath));
                }
            }
        }
        private MediaElement _videoElement = new MediaElement();
        public MediaElement VideoObject {
            get { return _videoElement; }
            set {
                if (_videoElement != value) {
                    _videoElement = value;
                    RaisePropertyChanged(nameof(VideoObject));
                }
            }
        }
    }
}
