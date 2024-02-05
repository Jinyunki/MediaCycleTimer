using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MediaCycleTimer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MediaCycleTimer.ViewModel {
    public class MediaViewModel : MediaModel {
        private BackgroundWorker mediaWorker;
        public MediaViewModel() {
            BtnEvents();
        }

        private void GetMediaThreadChecker() {
            mediaWorker = new BackgroundWorker {
                WorkerSupportsCancellation = true
            };
            mediaWorker.DoWork += MediaWorker_DoWork;
            mediaWorker.RunWorkerCompleted += MediaWorker_RunWorkerCompleted;
            mediaWorker.RunWorkerAsync();
        }
        private DispatcherTimer timer;
        private void GetMediaThreadChecker2() {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10); // 1초마다 실행 (조절 가능)
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e) {
            if (VideoObject.NaturalDuration.HasTimeSpan) {
                // 현재 시간
                TimeSpan currentPosition = VideoObject.Position;
                string formattedCurrentPosition = currentPosition.ToString(@"hh\:mm\:ss\.fff");
                PlayTime = formattedCurrentPosition;
                Console.WriteLine("Current Position: " + formattedCurrentPosition);

                // 총 시간
                TimeSpan totalDuration = VideoObject.NaturalDuration.TimeSpan;
                string formattedTotalDuration = totalDuration.ToString(@"hh\:mm\:ss\.fff");
                Console.WriteLine("Total Duration: " + formattedTotalDuration);
            }
        }


        private void MediaWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            Console.WriteLine("Finish Completed");
        }

        private void MediaWorker_DoWork(object sender, DoWorkEventArgs e) {
            while (!mediaWorker.CancellationPending) {
                ;
            }
        }

        private void BtnEvents() {
            BtnFileOpen = new RelayCommand(FileOpenExcute);
            BtnMediaPlayStop = new RelayCommand(MediaPlayStop);
        }

        private void MediaPlayStop() {
            if (VideoObject.Source == null) {
                if (!string.IsNullOrEmpty(SelectedFilePath)) {
                    VideoObject.Source = new Uri(SelectedFilePath);
                    VideoObject.Play();
                }
            }

            if (IsPlaying) {
                VideoObject.Pause();
                IsPlaying = false;
            } else {
                VideoObject.Play();
                IsPlaying = true;
            }

        }

        //GetMediaThreadChecker();
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
                
                // 재생 시작
                VideoObject.Play();
                // 일시 정지
                VideoObject.Pause();

                GetMediaThreadChecker2();
            }
        }
        
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
    }
}
