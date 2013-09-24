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
using ControlLibrary;
namespace CMS.WPFHeadOffice
{
    /// <summary>
    /// Interaction logic for ContentLoaderImages.xaml
    /// </summary>
    public partial class ContentLoaderImages : UserControl
    {
        public ContentLoaderImages()
        {
            InitializeComponent();

            LoadImageLinks();
        }

        private async void LoadImageLinks()
        {
            var loader = (FlickrImageLoader)Tab.ContentLoader;

            // load image links and assign to tab list
            this.Tab.Links = await loader.GetInterestingnessListAsync();

            // select first link
            this.Tab.SelectedSource = this.Tab.Links.Select(l => l.Source).FirstOrDefault();
        }
    }
}
