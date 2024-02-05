using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MediaCycleTimer.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MediaCycleTimer.Model {
    public class MainModel : ViewModelBase {
        public ViewModelLocator _locator = new ViewModelLocator();
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel {
            get {
                return _currentViewModel;
            }
            set {
                // 이전 View 종료
                if (_currentViewModel != null) {
                    _currentViewModel = null;
                }
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        private WindowState _windowState;
        public WindowState WindowState {
            get { return _windowState; }
            set {
                if (_windowState != value) {
                    _windowState = value;
                    RaisePropertyChanged(nameof(WindowState));
                }
            }
        }
        private int _minTransparent = 1;
        public int MinTransparent {
            get => _minTransparent;
            set {
                if (_minTransparent != value) {
                    _minTransparent = value;
                    RaisePropertyChanged("MinTransparent");
                }
            }
        }
        private int _maxTransparent = 100;
        public int MaxTransparent {
            get => _maxTransparent;
            set {
                if (_maxTransparent != value) {
                    _maxTransparent = value;
                    RaisePropertyChanged("MaxTransparent");
                }
            }
        }
        private int _transparentValue = 100;
        public int TransparentValue {
            get => _transparentValue;
            set {
                if (_transparentValue != value) {
                    _transparentValue = value;
                    RaisePropertyChanged("TransparentValue");
                    RealTransparentValue = value * 0.01;
                }
            }
        }
        private double _winBtnOpacity = 0.8;
        public double WindowBtnOpacity {
            get => _winBtnOpacity;
            set {
                if (value != _winBtnOpacity) {
                    _winBtnOpacity = value;
                    RaisePropertyChanged("WindowBtnOpacity");
                }
            }
        }

        private double _realTransparentValue = 1.0;
        public double RealTransparentValue {
            get => _realTransparentValue;
            set {
                if (_realTransparentValue != value) {
                    _realTransparentValue = value;
                    RaisePropertyChanged("RealTransparentValue");
                }
            }
        }


        #region Window Viewing Btn
        public void BtnWindows() {
            BtnMinmize = new RelayCommand(()=> { WindowState = WindowState.Minimized; });
            BtnMaxsize = new RelayCommand(()=> { WindowState = WindowState.Maximized; });
            BtnClose = new RelayCommand(()=> { Application.Current.Shutdown(); });
        }
        public ICommand BtnMinmize { get; set; }
        public ICommand BtnMaxsize { get; set; }
        public ICommand BtnClose { get; set; }
        #endregion

        #region SideTab Btn Command
        public void BtnSideTabs() {
            BtnMedia = new RelayCommand(()=> { CurrentViewModel = _locator.MediaViewModel; });
        }
        public ICommand BtnMedia { get; set; }
        #endregion
        
    }
}
