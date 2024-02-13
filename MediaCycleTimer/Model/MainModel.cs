using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MediaCycleTimer.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Diagnostics;
using Point = System.Drawing.Point;

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

        public ICommand CaptureScreenCommand { get; set; }
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
            CaptureScreenCommand = new RelayCommand(CaptureScreen);
        }

        private void CaptureScreen() {
            try {
                int width = (int)SystemParameters.PrimaryScreenWidth;
                int height = (int)SystemParameters.PrimaryScreenHeight;

                // 화면 크기만큼의 Bitmap 생성
                using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb)) {
                    // Bitmap 이미지 변경을 위해 Graphics 객체 생성
                    using (Graphics gr = Graphics.FromImage(bmp)) {
                        // 화면을 그대로 카피해서 Bitmap 메모리에 저장
                        gr.CopyFromScreen(0, 0, 0, 0, bmp.Size);

                    }
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string fileName = $"screenshot_{DateTime.Now:yyyyMMddHHmmss}.png";
                    string filePath = Path.Combine(desktopPath, fileName);
                    // Bitmap 데이타를 파일로 저장
                    bmp.Save(filePath, ImageFormat.Png);
                    ShowNotification(filePath);
                }

            } catch (Exception ex) {
                // Handle exception
            }
        }


        private void ShowNotification(string message) {
            // Create a popup with a textblock to display the message
            Popup popup = new Popup();
            TextBlock textBlock = new TextBlock {
                Text = message,
                Background = System.Windows.Media.Brushes.LightGray,
                Padding = new Thickness(10),
                FontSize = 16,
                TextWrapping = TextWrapping.Wrap,
                Opacity = 0.5
            };
            popup.Child = textBlock;

            // Set the placement target to the main window
            Window mainWindow = Application.Current.MainWindow;
            popup.PlacementTarget = mainWindow;

            // Set the placement to center
            popup.Placement = PlacementMode.Center;
            popup.IsOpen = true;

            // Automatically close the popup after a delay
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += (sender, e) =>
            {
                popup.IsOpen = false;
                timer.Stop();
            };
            timer.Interval = TimeSpan.FromSeconds(2); // Adjust the duration as needed
            timer.Start();
        }
        public ICommand BtnMedia { get; set; }
        #endregion
        
    }
}
