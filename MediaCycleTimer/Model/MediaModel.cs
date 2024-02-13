using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace MediaCycleTimer.Model {
    public class MediaModel : ViewModelBase {
        private string _playTime;
        private string _totalPlayTime;
        private string _mediaTime;
        private double _mediaSliderValue;
        private double _mediaSliderMax = 100;
        private bool _isPlaying = false;
        private string _selectedFilePath;
        private double _speedRatio = 3.0;
        private MediaPlayer _mediaPlayerObject = new MediaPlayer();
        private MediaState _loadedBehavior = MediaState.Manual;
        private MediaState _unloadedBehavior = MediaState.Manual;
        private ObservableCollection<SaveModel> _saveDataList = new ObservableCollection<SaveModel>();
        public DateTime Delay(int MS) {

            DateTime thisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime afterMoment = thisMoment.Add(duration);

            while (afterMoment >= thisMoment) {
                Application.Current?.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
                thisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }
        public DispatcherTimer DispatcherMedia { get; set; }
        public ICommand BtnFileOpen { get; set; }
        public ICommand BtnMediaPlayStop { get; set; }
        public ICommand AddCycleTimeList { get; set; }
        public ICommand BtnMoveSpeedDown { get; set; }
        public ICommand BtnMoveSpeedUp { get; set; }
        public double InputTecTime { get; set; }
        public TimeSpan PausedPosition { get; set; }// 일시정지된 위치를 저장할 변수 추가
        public ObservableCollection<SaveModel> SaveDataList {
            get { return _saveDataList; }
            set {
                if (_saveDataList != value) {
                    _saveDataList = value;
                    RaisePropertyChanged(nameof(SaveDataList));
                }
            }
        }
        public string PlayTime {
            get { return _playTime; }
            set {
                if (_playTime != value) {
                    _playTime = value;
                    RaisePropertyChanged(nameof(PlayTime));
                }
            }
        }
        public string TotalPlayTime {
            get { return _totalPlayTime; }
            set {
                if (_totalPlayTime != value) {
                    _totalPlayTime = " / " + value + " Sec";
                    RaisePropertyChanged(nameof(TotalPlayTime));
                }
            }
        }
        public string MediaTime {
            get { return _mediaTime; }
            set {
                if (_mediaTime != value) {
                    _mediaTime = value;
                    RaisePropertyChanged(nameof(MediaTime));
                }
            }
        }
        

        public double MediaSliderMax {
            get { return _mediaSliderMax; }
            set {
                if (_mediaSliderMax != value) {
                    _mediaSliderMax = value;
                    RaisePropertyChanged(nameof(MediaSliderMax));
                }
            }
        }

        public bool IsPlaying {
            get { return _isPlaying; }
            set {
                if (_isPlaying != value) {
                    _isPlaying = value;
                    RaisePropertyChanged(nameof(IsPlaying));
                }
            }
        }

        public double MediaSliderValue {
            get { return _mediaSliderValue; }
            set {
                if (_mediaSliderValue != value) {
                    _mediaSliderValue = value;
                    RaisePropertyChanged(nameof(MediaSliderValue));
                }
            }
        }

        public string SelectedFilePath {
            get { return _selectedFilePath; }
            set {
                if (_selectedFilePath != value) {
                    _selectedFilePath = value;
                    RaisePropertyChanged(nameof(SelectedFilePath));
                }
            }
        }

        public MediaPlayer MediaPlayerObject {
            get { return _mediaPlayerObject; }
            set {
                if (_mediaPlayerObject != value) {
                    _mediaPlayerObject = value;
                    RaisePropertyChanged(nameof(MediaPlayerObject));
                }
            }
        }

        public MediaState LoadedBehavior {
            get { return _loadedBehavior; }
            set {
                if (_loadedBehavior != value) {
                    _loadedBehavior = value;
                    RaisePropertyChanged(nameof(LoadedBehavior));
                }
            }
        }

        public MediaState UnloadedBehavior {
            get { return _unloadedBehavior; }
            set {
                if (_unloadedBehavior != value) {
                    _unloadedBehavior = value;
                    RaisePropertyChanged(nameof(UnloadedBehavior));
                }
            }
        }

        public double SpeedRatio {
            get { return _speedRatio; }
            set {
                if (_speedRatio != value) {
                    _speedRatio = value;
                    RaisePropertyChanged(nameof(SpeedRatio));
                }
            }
        }
    }
}
