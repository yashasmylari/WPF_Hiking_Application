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
        private Uri locUri;
        private int count = 1;
       

        public W_trekDetails( Hill data, Window mw )
        {
            mainWindow = mw;
            InitializeComponent();
            hill = data;
            NameOfPlace.Content = hill.name;
            NameOfPlace1.Content = hill.name;
            Tbx_distance.Text = hill.distance;
            Tbx_uphill.Text = hill.Uphill;
            Tbx_downhill.Text = hill.hike_speed;
            Tbx_description.Text = hill.description;
            locUri = new Uri(hill.locations);
            Wbx_locations.Source =  locUri;
            Img_gallery.Visibility = Visibility.Visible;
            loadImage(count++);
            manageImageScrollButton();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = hill;
        }


        public void trekWindowClosingEvent(object sender, CancelEventArgs e)
        {
            mainWindow.Visibility = Visibility.Visible;
        }



        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void WebBrowser_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.N && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
            {
                e.Handled = true;
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            if (count > 0 )
                loadImage(count--);
            // Logic to hide the back button when it is the first image
            manageImageScrollButton();

        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            if (count<3)
                loadImage(count++);
            // Logic to hide the next button when it is the last image
            manageImageScrollButton();
        }

        // Function to manage the visiblility of the back and the next button
        private void manageImageScrollButton() {
            btnGallery_ScrollBack.Visibility = (count >= 2) ? Visibility.Visible : Visibility.Hidden;
            btnGallery_ScrollNext.Visibility = (count <= 2) ? Visibility.Visible : Visibility.Hidden;
        }

        // Function to load the image
        private void loadImage(int count) {
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri("C:/Users/YASHAS/source/repos/Hike_Begins/HillImages/" + hill.name + "/" + count + ".jpg");
            logo.EndInit();
            Img_gallery.Source = logo;
        }

        private void Ibx_img12_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tabcontrol1.SelectedIndex = 2;
        }
        private void Ibx_img1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tabcontrol1.SelectedIndex = 1;
        }

        
    }

}
