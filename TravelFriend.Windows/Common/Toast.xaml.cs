using System;
using System.Collections.Generic;
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

namespace TravelFriend.Windows.Common
{
    /// <summary>
    /// Toast.xaml 的交互逻辑
    /// </summary>
    public partial class Toast : UserControl
    {
        public Toast()
        {
            InitializeComponent();
        }

        public async void Show(string content)
        {
            ToastText.Text = content;
            this.Visibility = Visibility.Visible;
            await Task.Delay(1500);
            this.Visibility = Visibility.Collapsed;
        }
    }
}
