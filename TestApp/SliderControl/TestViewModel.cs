using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SliderControl;

namespace SliderControl
{
    public class TestViewModel : ObservableObject
    {
        private double _refVal;

        public double RefVal
        {
            get { return _refVal; }
            set
            {
                _refVal = value;
                OnPropertyChanged();
            }
        }

        public TestViewModel()
        {
            RefVal = 20;
        }

    }
}
