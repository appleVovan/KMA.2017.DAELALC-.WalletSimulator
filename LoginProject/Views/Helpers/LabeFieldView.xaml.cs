﻿using System.Windows;
using System.Windows.Controls;

namespace WalletSimulator.Views.Helpers
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class LabelFieldView : UserControl
    {
        public LabelFieldView()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PropertyValueProperty = DependencyProperty.Register
        (
            "PropertyValue",
            typeof(string),
            typeof(LabelFieldView),
            new PropertyMetadata(string.Empty)
        );
        
        public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register
        (
            "PropertyName",
            typeof(string),
            typeof(LabelFieldView),
            new PropertyMetadata(string.Empty)
        );
        public string PropertyValue
        {
            get { return (string)GetValue(PropertyValueProperty); }
            set { SetValue(PropertyValueProperty, value); }
        }
        
        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }
    }
}
