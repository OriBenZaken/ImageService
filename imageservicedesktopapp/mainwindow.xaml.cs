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

namespace ImageServiceDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Members
        private IImageServiceClient imageServiceClient;
        private IVM_ImageService vm_imageService;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm_imageService;
            imageServiceClient = new ImageServiceClient();
            vm_imageService = new VM_ImageService()
            {
                ImageServiceClient = imageServiceClient
            };
        }
    }
}