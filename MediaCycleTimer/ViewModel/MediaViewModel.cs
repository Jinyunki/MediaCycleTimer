using GalaSoft.MvvmLight.Command;
using MediaCycleTimer.Model;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace MediaCycleTimer.ViewModel {
    public class MediaViewModel : MediaModel {
        public MediaViewModel() {
            BtnEvents();
        }
        private void BtnEvents() {
            SpeedRatio = 1.0;
            BtnFileOpen = new RelayCommand(ExcuteFileOpen);
            BtnMediaPlayStop = new RelayCommand(ExcuteMediaPlayer);
            AddCycleTimeList = new RelayCommand(ExcuteAddCycleTimeList);
            BtnMoveSpeedUp = new RelayCommand(() => { SpeedRatio += 0.5; });
            BtnMoveSpeedDown = new RelayCommand(() => { SpeedRatio -= 0.5; });
        }
        private void ExcuteAddCycleTimeList() {
            if (SaveDataList.Count < 1) {
                InputTecTime = double.Parse(PlayTime);
            } else {
                double value = 0;
                for (int index = 0; index <= SaveDataList.Count - 1; index++) {
                    value += double.Parse(SaveDataList[index].TecTime);
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
            // play중 일때 버튼 이벤트가 들어오면
            if (IsPlaying) {
                // CompositionTarget.Rendering 이벤트 핸들러 제거
                CompositionTarget.Rendering -= UpdateMediaPosition;
                LoadedBehavior = MediaState.Pause;
                MediaPlayerObject.SpeedRatio = SpeedRatio;
                ResumePausePlayer(IsPlaying);
                IsPlaying = false;
            } else {
                // Slider Controll Thread
                CompositionTarget.Rendering += UpdateMediaPosition;
                LoadedBehavior = MediaState.Play;
                MediaPlayerObject.SpeedRatio = SpeedRatio;
                ResumePausePlayer(IsPlaying);
                IsPlaying = true;
            }
        }
        
        private void ResumePausePlayer(bool isPlayStatus) {
            MediaPlayerObject.SpeedRatio = SpeedRatio;
            if (isPlayStatus) {
                if (MediaPlayerObject != null && MediaPlayerObject.CanPause) {
                    // 일시정지된 시점의 재생 위치를 저장
                    PausedPosition = MediaPlayerObject.Position;
                    MediaPlayerObject.Pause();
                }
            } else {
                if (MediaPlayerObject != null) {
                    // 일시정지된 위치부터 다시 재생
                    MediaPlayerObject.Position = PausedPosition;
                    MediaPlayerObject.Play();
                }
            }
        }

        private void UpdateMediaPosition(object sender, EventArgs e) {
            MediaSliderMax = MediaPlayerObject.NaturalDuration.TimeSpan.TotalSeconds;
            TimeSpan currentPosition = MediaPlayerObject.Position;
            MediaTime = currentPosition.ToString(@"hh\:mm\:ss\.ff");

            // 재생 중일 때에만 값 업데이트
            if (IsPlaying) {
                PlayTime = Math.Round(currentPosition.TotalSeconds, 2).ToString();
                MediaSliderValue = currentPosition.TotalSeconds;
            }
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
            // 총 재생 시간
            TimeSpan totalDuration = MediaPlayerObject.NaturalDuration.TimeSpan;
            TotalPlayTime = Math.Round(totalDuration.TotalSeconds, 2).ToString();
        }
    }
}
