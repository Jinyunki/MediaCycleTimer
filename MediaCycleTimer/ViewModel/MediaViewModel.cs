using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MediaCycleTimer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediaCycleTimer.ViewModel {
    public class MediaViewModel : MediaModel {
        public MediaViewModel() {
            BtnEvents();
        }

        private void BtnEvents() {
            BtnFileOpen = new RelayCommand(FileOpenExcute);
        }

        private void FileOpenExcute() {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog {
                Filter = "Video Files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv|All Files (*.*)|*.*",
                Title = "Select a Video File"
            };
            if (openFileDialog.ShowDialog() == true) {
                SelectedFilePath = openFileDialog.FileName;
                VideoObject.Source = new Uri(SelectedFilePath);
                VideoObject.LoadedBehavior = MediaState.Manual;
                VideoObject.UnloadedBehavior = MediaState.Manual;
                VideoObject.Stretch = Stretch.Uniform;
                VideoObject.MediaEnded += (sender, e) =>
                {
                    // 반복재생 되도록
                    VideoObject.Position = TimeSpan.Zero;
                };
                VideoObject.Play();
            }
           
        }
        
    }
}
