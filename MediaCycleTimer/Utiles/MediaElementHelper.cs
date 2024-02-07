using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MediaCycleTimer.Utiles {
    public static class MediaElementHelper {
        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.RegisterAttached(
                "SpeedRatio",
                typeof(double),
                typeof(MediaElementHelper),
                new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, SpeedRatioPropertyChanged));

        public static double GetSpeedRatio(MediaElement element) {
            return (double)element.GetValue(SpeedRatioProperty);
        }

        public static void SetSpeedRatio(MediaElement element, double value) {
            element.SetValue(SpeedRatioProperty, value);
        }

        private static void SpeedRatioPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is MediaElement mediaElement) {
                mediaElement.SpeedRatio = (double)e.NewValue;
            }
        }
    }

}
