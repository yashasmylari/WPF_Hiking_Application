using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Hike_Begins
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Hill> trekSpotsDetails = new ObservableCollection<Hill>();
        ObservableCollection<Hill> trekSpots;
        private string filter;

        private bool langEn = true;
        System.Globalization.CultureInfo english;
        System.Globalization.CultureInfo kanada;


        public MainWindow()
        {
            english = new System.Globalization.CultureInfo("");
            kanada = new System.Globalization.CultureInfo("kn-IN");

            System.Threading.Thread.CurrentThread.CurrentUICulture = kanada;

            InitializeComponent();
            removeMethod();
            Grd_spots.ItemsSource = trekSpots;
            
        }

        public void changeLanguage()
        {
            if(langEn)
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = kanada;
                langEn = false;
            }
            else
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = english;
                langEn = true;
            }
        }

        private void removeMethod()
        {
            var trek = new ObservableCollection<Hill>();
            for (int i = 0; i < 5; i++)
            {
                trek.Add(new Hill { name = "place" + i, difficulty = "Easy", duration=i , image ="/images/hill1.jpg", distance = i*10 , weather = "79f", diameter = "40meter", elevation= "555meter ", prominence= "365 metres", description= "lgydfgui;earguehrgeurerber efhurkfberfrrrrrrrrrrrrrrrrrrrrrrrrrbwkhjfwf w wefwf waf fwawafvwrefaerfaerfearrrrrrrrrrrrrrrrrgergeargv aergaerrrrrrrrrrgggaergergaergergvaerfarfaerfvfv errrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr errrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr. ergwgerrrrrrrrrrrrrrrrrrrrrrrrgwerwwwwwwwwwwwwvfs" });
            }
            trekSpots = trek;
        }

        

        private void Btn_Go_Click(object sender, RoutedEventArgs e)
        {
            //trekSpotsDetails.Clear();

            //Hill selected = (Hill)Grd_spots.SelectedItem;
            //Tbx_name.Text = selected.name;
            //W_trekDetails detailPage = new W_trekDetails();
            //detailPage.Show();
            //trekSpotsDetails.Add(selected);
        }


        private void Grd_spots_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Grd_spots.SelectedItem!=null) { 
                Tbx_name.Text = ((Hill)Grd_spots.SelectedItem).name;
                Tbx_Search_Difficulty.Text = ((Hill)Grd_spots.SelectedItem).difficulty;
                Tbx_Search_Duration.Text = ((Hill)Grd_spots.SelectedItem).duration.ToString();
            }
        }


        private void Grd_spots_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            W_trekDetails detailPage = new W_trekDetails((Hill)Grd_spots.SelectedItem, this);
            detailPage.Show();

        }
        //this.Visibility = Visibility.Visible;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            removeMethod();

            TestStorge.WriteXml<ObservableCollection<Hill>>(trekSpots,"trekSpots.xml");
        }

        private void Tbx_filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filterName = Tbx_name.Text.ToLower();
            string filterDifficulty = Tbx_Search_Difficulty.Text.ToLower();
            string filterDuration = Tbx_Search_Duration.Text.ToLower();

            if (filterName == "" && filterDifficulty == "" && filterDuration == "")
            {
                Grd_spots.ItemsSource = trekSpots;
            }
            else
            {
                var results = getResult(filterName, filterDifficulty, filterDuration);
                ObservableCollection<Hill> oc = new ObservableCollection<Hill>(results);
                Grd_spots.ItemsSource = results;
            }
        }


        private void English_Language_Radio(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("English Selected Logic");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
            langEn = true;
            InitializeComponent();
        }

        private void Kannada_Language_Radio(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Kanada Selected Logic");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("kn-IN");
            langEn = true;
            InitializeComponent();
        }

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            Tbx_name.Text = "";
            Tbx_Search_Difficulty.Text = "";
            Tbx_Search_Duration.Text = "";
        }


        private IEnumerable<Hike_Begins.Hill> getResult(string filterName, string filterDifficulty, string filterDuration)
        {
            if(filterName!="" && filterDifficulty!="" && filterDuration!="")
            {
                 return from s in trekSpots
                        where
                     s.name.ToLower().Contains(filterName) &&
                     s.difficulty.ToLower().Contains(filterDifficulty) &&
                     s.duration.ToString().Contains(filterDuration)
                        select s;
            }
            else if (filterName != "" && filterDifficulty != "" && filterDuration == "")
            {
                return from s in trekSpots
                       where
                     s.name.ToLower().Contains(filterName) &&
                     s.difficulty.ToLower().Contains(filterDifficulty)
                       select s;
            }
            else if (filterName != "" && filterDifficulty == "" && filterDuration != "")
            {
                return from s in trekSpots
                       where
                     s.name.ToLower().Contains(filterName) &&
                     s.duration.ToString().Contains(filterDuration)
                       select s;
            }
            else if (filterName != "" && filterDifficulty == "" && filterDuration == "")
            {
                return from s in trekSpots
                       where
                     s.name.ToLower().Contains(filterName)
                       select s;
            }
            else if (filterName == "" && filterDifficulty != "" && filterDuration != "")
            {
                return from s in trekSpots
                       where
                     s.difficulty.ToLower().Contains(filterDifficulty) &&
                     s.duration.ToString().Contains(filterDuration)
                       select s;
            }
            else if (filterName == "" && filterDifficulty != "" && filterDuration == "")
            {
                return from s in trekSpots
                       where
                     s.difficulty.ToLower().Contains(filterDifficulty)
                       select s;
            }
            else if (filterName == "" && filterDifficulty == "" && filterDuration != "")
            {
                return from s in trekSpots
                       where
                     s.duration.ToString().Contains(filterDuration)
                       select s;
            }
            return null;
        }
    }
}
