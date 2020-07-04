using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelFriend.Windows.Transport
{
    /// <summary>
    /// TransportPage.xaml 的交互逻辑
    /// </summary>
    public partial class TransportPage : UserControl
    {
        public TransportPage()
        {
            InitializeComponent();
            Loaded += TransportPage_Loaded;
        }

        private void TransportPage_Loaded(object sender, RoutedEventArgs e)
        {
            UploadList.Items.Add(new UploadBlock());
            UploadList.Items.Add(new UploadBlock());
            UploadList.Items.Add(new UploadBlock());
            UploadList.Items.Add(new UploadBlock());
            UploadList.Items.Add(new UploadBlock());
            UploadList.Items.Add(new UploadBlock());
        }
    }
}
