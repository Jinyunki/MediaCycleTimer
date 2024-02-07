﻿using GalaSoft.MvvmLight;
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
        public MediaViewModel() {
            BtnEvents();
        }
        BackgroundWorker MediaWorker;
        private void MediaTimer() {
            MediaWorker = new BackgroundWorker {
                WorkerSupportsCancellation = true
            };
            MediaWorker.DoWork += MediaWorker_DoWork;
            MediaWorker.RunWorkerAsync();
        }

        private void MediaWorker_DoWork(object sender, DoWorkEventArgs e) {
            while (!MediaWorker.CancellationPending) {
                TimeSpan currentPosition = MediaPlayerObject.Position;
                MediaTime = currentPosition.ToString(@"hh\:mm\:ss\.ff");
                PlayTime = Math.Round(currentPosition.TotalSeconds, 2).ToString();
            }
        }
        
        private void BtnEvents() {
            SpeedRatio = 1.0;
            BtnFileOpen = new RelayCommand(ExcuteFileOpen);
            BtnMediaPlayStop = new RelayCommand(ExcuteMediaPlayer);
            AddCycleTimeList = new RelayCommand(ExcuteAddCycleTimeList);
            BtnMoveSpeedUp = new RelayCommand(() => { SpeedRatio += 0.5; });
            BtnMoveSpeedDown = new RelayCommand(() => { SpeedRatio -= 0.1; });
        }
        
        private void ExcuteAddCycleTimeList() {
            
            if (SaveDataList.Count < 1) {
                InputTecTime = double.Parse(PlayTime);
            } else if (SaveDataList.Count == 1) {
                InputTecTime = double.Parse(PlayTime) - double.Parse(SaveDataList[0].TecTime);
            } else {
                double value = 0;
                for (int i = 0; i <= SaveDataList.Count-1; i++) {
                    value += double.Parse(SaveDataList[i].TecTime);
                }
                InputTecTime = double.Parse(PlayTime) - value;
            }  

            SaveModel saveModel = new SaveModel((SaveDataList.Count+1).ToString(), Math.Round(InputTecTime,2).ToString(), MediaTime);
            SaveDataList.Add(saveModel);
        }

        private void ExcuteMediaPlayer() {
            if (MediaPlayerObject.Source == null) {
                if (!string.IsNullOrEmpty(SelectedFilePath)) {
                    MediaPlayerObject.Open(new Uri(SelectedFilePath));
                    LoadedBehavior = MediaState.Play;
                }
            }

            if (IsPlaying) {
                // CompositionTarget.Rendering 이벤트 핸들러 제거
                CompositionTarget.Rendering -= UpdateMediaPosition;
                LoadedBehavior = MediaState.Pause;
                MediaPlayerObject.SpeedRatio = SpeedRatio;
                MediaWorker.CancelAsync();
                IsPlaying = false;
            } else {
                // Slider Controll Thread
                CompositionTarget.Rendering += UpdateMediaPosition;
                LoadedBehavior = MediaState.Play;
                MediaPlayerObject.SpeedRatio = SpeedRatio;
                MediaWorker.RunWorkerAsync();
                IsPlaying = true;
            }
        }
        private void UpdateMediaPosition(object sender, EventArgs e) {
            MediaSliderValue = MediaPlayerObject.Position.TotalSeconds;
        }
        private void ExcuteFileOpen() {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog {
                Filter = "Video Files (*.mp4;*.avi;*.mkv)|*.mp4;*.avi;*.mkv|All Files (*.*)|*.*",
                Title = "Select a Video File"
            };

            if (openFileDialog.ShowDialog() == true) {
                SelectedFilePath = openFileDialog.FileName;
                // MediaPlayer의 소스를 설정하여 미디어를 엽니다.
                MediaPlayerObject.Open(new Uri(SelectedFilePath));
                MediaPlayerObject.MediaOpened += (sender, e) => {
                    // Open MediaFile Full Time Load
                    MediaPlayerObject_MediaOpened(sender, e);
                };
            }
        }

        private void MediaPlayerObject_MediaOpened(object sender, EventArgs e) {
            // 총 재생 시간 가져오기
            TimeSpan totalDuration = MediaPlayerObject.NaturalDuration.TimeSpan;
            //TotalPlayTime = totalDuration.ToString(@"hh\:mm\:ss\.ff");
            TotalPlayTime = Math.Round(totalDuration.TotalSeconds, 2).ToString();
            // MediaTimer 시작
            MediaTimer();
        }
    }
}
