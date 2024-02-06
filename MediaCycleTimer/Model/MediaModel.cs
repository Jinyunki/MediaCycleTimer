using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediaCycleTimer.Model {
    public class MediaModel : ViewModelBase {
        public ICommand BtnFileOpen { get; set; }
        public ICommand BtnMediaPlayStop { get; set; }

        private string _playTime;
        public string PlayTime {
            get { return _playTime; }
            set {
                if (_playTime !=value) {
                    _playTime = value;
                    RaisePropertyChanged(nameof(PlayTime));
                }
            }
        }

        private int _mediaSliderMin = 0;
        public int MediaSliderMin {
            get { return _mediaSliderMin; }
            set {
                if (_mediaSliderMin != value) {
                    _mediaSliderMin = value;
                    RaisePropertyChanged(nameof(MediaSliderMin));
                }
            }
        }

        private int _mediaSliderMax = 100;
        public int MediaSliderMax {
            get { return _mediaSliderMax; }
            set {
                if (_mediaSliderMax != value) {
                    _mediaSliderMax = value;
                    RaisePropertyChanged(nameof(MediaSliderMax));
                }
            }
        }

        private bool _isPlaying = false;
        public bool IsPlaying {
            get { return _isPlaying; }
            set {
                if (_isPlaying != value) {
                    _isPlaying = value;
                    RaisePropertyChanged(nameof(IsPlaying));
                }
            }
        }

        private double _mediaSliderValue;
        public double MediaSliderValue {
            get { return _mediaSliderValue; }
            set {
                if (_mediaSliderValue != value) {
                    _mediaSliderValue = value;
                    RaisePropertyChanged(nameof(MediaSliderValue));
                }
            }
        }

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
