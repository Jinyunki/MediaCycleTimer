using MediaCycleTimer.ViewModel;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MediaCycleTimer.Views {
    /// <summary>
    /// MediaView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MediaView : UserControl {
        public MediaView() {
            InitializeComponent();
        }
        private void PosListBox_MouseDown(object sender, MouseButtonEventArgs e) {
            PosListBox.SelectedItem = null;
        }
    }

}
