using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MediaCycleTimer.Model {
    public class SideTabButton : ViewModelBase {
        private string _buttonText;
        private ICommand _command;

        public string ButtonText {
            get => _buttonText;
            set {
                if (_buttonText != value) {
                    _buttonText = value;
                    RaisePropertyChanged(nameof(ButtonText));
                }
            }
        }

        public ICommand Command {
            get => _command;
            set {
                if (_command != value) {
                    _command = value;
                    RaisePropertyChanged(nameof(Command));
                }
            }
        }
    }
}
