using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaCycleTimer.Model {
    public class SaveModel : ViewModelBase {
        public SaveModel(string title, string tecTime, string mediaTime) {
            Title = title;
            TecTime = tecTime;
            MediaTime = mediaTime;
        }
        private string _title;
        public string Title {
            get { return _title; }
            set {
                if (_title != value) {
                    _title = "Chapter #"+value;
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }
        private string _tecTime;
        public string TecTime {
            get { return _tecTime; }
            set {
                if (_tecTime != value) {
                    _tecTime = value;
                    RaisePropertyChanged(nameof(TecTime));
                }
            }
        }
        private string _mediaTime;


        public string MediaTime {
            get { return _mediaTime; }
            set {
                if (_mediaTime != value) {
                    _mediaTime = value;
                    RaisePropertyChanged(nameof(MediaTime));
                }
            }
        }
    }
}
