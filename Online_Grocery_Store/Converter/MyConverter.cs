using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Online_Grocery_Store.Converter
{
    class MyConverter : IMultiValueConverter //A converter class implementing of IMultiValueConverter Interface
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) //Overriding Convert(...) function of IMultiValueConverter Interface 
        {
            return values.Clone(); //Shallow Copying values array
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) //Overriding ConvertBack(...) of function IMultiValueConverter Interface 
        {
            throw new NotImplementedException();
        }
    }
}
