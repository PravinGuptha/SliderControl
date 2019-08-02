using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace TestLibrary
{
    /// <summary>
    /// Interaction logic for Slider.xaml
    /// </summary>
    public partial class Slider
    {
        #region DependencyProperty
        public bool IsTypingVal
        {
            get { return (bool)GetValue(IsTypingValProperty); }
            set { SetValue(IsTypingValProperty, value); }
        }

        public static readonly DependencyProperty IsTypingValProperty =
            DependencyProperty.Register("IsTypingVal", typeof(bool), typeof(Slider), new PropertyMetadata(OnDependencyPropertyChanged));

        #endregion

        #region Constructor

        public Slider()
        {
            InitializeComponent();
            SizeChanged += this_SizeChanged;
        }
        #endregion
        
        #region Public members
        public string MinStr
        {
            get
            {
                return ToString(Minimum);
            }
        }

        public string MaxStr
        {
            get
            {
                return ToString(Maximum);
            }
        }

        public double PadLeft
        {
            get
            {
                var track = Template.FindName("PART_Track", this) as Track;
                var outer = Template.FindName("PART_Outer", this) as FrameworkElement;
                if (track == null || outer == null) return 0;
                return (outer.ActualWidth - track.Thumb.ActualWidth) * (base.Minimum);
            }
        }
        public double PadRight
        {
            get
            {
                var track = Template.FindName("PART_Track", this) as Track;
                var outer = Template.FindName("PART_Outer", this) as FrameworkElement;
                if (track == null || outer == null) return 0;
                return (outer.ActualWidth - track.Thumb.ActualWidth) * Maximum;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Overrides
        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            OnPropertyChanged("MinStr");
            OnPropertyChanged("PadLeft");
            OnPropertyChanged("PadRight");
        }
        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            OnPropertyChanged("MaxStr");
            OnPropertyChanged("PadLeft");
            OnPropertyChanged("PadRight");
        }
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            Value = newValue;
            if (!IsTypingVal) OnPropertyChanged("ValueStr");
        }
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (IsTypingVal && (e.Key == Key.Enter || e.Key == Key.Escape))
            {
                IsTypingVal = true;
            }
            base.OnPreviewKeyDown(e);
        }
        #endregion
        
        #region Methods
        private void this_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            OnPropertyChanged("PadLeft");
            OnPropertyChanged("PadRight");
        }
        private void PART_Edit_LostFocus(object sender, RoutedEventArgs e)
        {
            IsTypingVal = false;
        }


        void OnDependencyPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property.Name == "IsTypingVal")
            {
                
            }
        }
        
        void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        static void OnDependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Slider)d).OnDependencyPropertyChanged(e);
        }
        
        private string ToString(double d)
        {
            return d.ToString();
        }

        private void PART_Thumb_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IsTypingVal = true;
        }
        #endregion
    }
}
