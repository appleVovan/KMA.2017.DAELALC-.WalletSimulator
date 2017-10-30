﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WalletSimulator.Interface.Models;

namespace WalletSimulator
{
    /// <summary>
    /// Interaction logic for WalletView.xaml
    /// </summary>
    public partial class WalletView : UserControl
    {
        private Wallet wallet;

        public WalletView(Wallet wallet)
        {
            this.wallet = wallet;
            InitializeComponent();
        }
    }
}
