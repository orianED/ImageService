using ImageService.Logging.Modal;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ImageGUI.ViewModel{
    class TypeToColor : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            MessageTypeEnum input = (MessageTypeEnum)value;
            switch (input) {
                case MessageTypeEnum.FAIL:
                    return Brushes.Red;
                case MessageTypeEnum.INFO:
                    return Brushes.Green;
                case MessageTypeEnum.WARNING:
                    return Brushes.Yellow;
                default:
                    return DependencyProperty.UnsetValue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}
