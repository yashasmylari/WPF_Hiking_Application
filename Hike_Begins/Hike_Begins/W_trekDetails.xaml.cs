using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
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
using System.Windows.Shapes;

namespace Hike_Begins
{
    /// <summary>
    /// Interaction logic for W_trekDetails.xaml
    /// </summary>
    public partial class W_trekDetails : Window
    {
        Hill hill;
        Window mainWindow;
        int originalWidth;
        int originalHeight;

        public W_trekDetails( Hill data, Window mw )
        {
            mainWindow = mw;
            InitializeComponent();
            hill = data;
            originalWidth = (int) Ibx_img.Width;
            originalHeight = (int) Ibx_img.Height;
            NameOfPlace.Content = data.name;
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = hill;
        }


        public void trekWindowClosingEvent(object sender, CancelEventArgs e)
        {
            mainWindow.Visibility = Visibility.Visible;
        }



        private void expandImage1X(object sender, RoutedEventArgs e)
        {

            Ibx_img.Width = originalWidth;
            Ibx_img.Height = originalHeight;

        }

        private void expandImage2X(object sender, RoutedEventArgs e)
        {

            int widthNew = originalWidth * 2;
            int heightNew = originalHeight * 2;

            Ibx_img.Width = widthNew;
            Ibx_img.Height = heightNew;

        }


        private void expandImage3X(object sender, RoutedEventArgs e)
        {

            int widthNew = originalWidth * 3;
            int heightNew = originalHeight * 3;

            Ibx_img.Width = widthNew;
            Ibx_img.Height = heightNew;

        }
    }

}
