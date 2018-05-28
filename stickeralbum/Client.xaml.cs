﻿using stickeralbum.Design;
using stickeralbum.Entities;
using stickeralbum.IO;
using stickeralbum.Utils;
using System;
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

namespace stickeralbum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Client : Window
    {
        public Client()
        {
            Cache.Load();
            Cache.Debug();
            InitializeComponent();
            ClientArea.Background = new ImageBrush(Sprite.Get("greek_background").Source);
            //TestUtil.RunAllTests();
        }

        public void SetCurrentPage(UserControl control) {
            Page.Children.Clear();
            Page.Children.Add(control);
        }
    }
}
