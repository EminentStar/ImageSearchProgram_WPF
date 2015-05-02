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
using System.Xml;

namespace ImageSearchProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainControl mainControl = new MainControl();
        SearchControl searchControl = new SearchControl();
        ResultControl resultControl = new ResultControl();

        public MainWindow()
        {
            InitializeComponent();
            MainGrid.Children.Add(mainControl);

            // Binding Handlers of mainControl
            mainControl.btn_image.Click += new RoutedEventHandler(btn_image_Click);
            mainControl.btn_recent.Click += btn_recent_Click;


            // Binding Handlers of searchControl
            searchControl.btn_back.Click += btn_back_Click;
            searchControl.btn_search.Click += btn_search_Click;
            searchControl.cb.SelectionChanged += cb_SelectionChanged;
            searchControl.c1.Selected += c1_Selected;
            searchControl.c2.Selected += c2_Selected;
            searchControl.c3.Selected += c3_Selected;

            // Binding Handlers of resultControl
            resultControl.btn_result_back.Click += btn_result_back_Click;
            resultControl.btn_remove.Click += btn_remove_Click;

        }

        //*************MainControl's Handlers
        void btn_image_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(searchControl);
        }
        void btn_recent_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(resultControl);
            resultControl.ShowResultOfSearch();
        }
        //************SearchControl's Handlers
        void btn_back_Click(object sender, RoutedEventArgs e)
        {
            searchControl.wp.Children.Clear();
            searchControl.tb.Clear();
            MainGrid.Children.Clear();
            MainGrid.Children.Add(mainControl);
        }
        void btn_search_Click(object sender, RoutedEventArgs e)
        {
            searchControl.wp.Children.Clear();
            //int imgCnt = Convert.ToInt32(searchControl.cb.Text);
            XmlDocument doc = new XmlDocument();
            ImageItem item = null;

            searchControl.InsertDB(searchControl.tb.Text, DateTime.Now);
            doc.Load("http://openapi.naver.com/search?key=4432e614518baff96f7dcc60d0fe5c88&query=" + searchControl.tb.Text + "&target=image&start=1&display=" + searchControl.cb.Text );
            
            XmlNodeList imgList = doc.GetElementsByTagName("thumbnail");

            foreach(XmlNode element in imgList)
            {
                item = new ImageItem(element);

                searchControl.wp.Children.Add(item);
            }
        }

        /* 네이버 api 이용, key값은 각자 발급 받은 키를 사용하거나 또는 그대로 사용해도 무방
         XmlDocument doc = new XmlDocument();
         doc.Load("http://openapi.naver.com/search?key=4432e614518baff96f7dcc60d0fe5c88&query=제주도&target=image&start=1&display=10");
         XmlNodeList imgList = doc.GetElementsByTagName("thumbnail");
        */

        void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        void c1_Selected(object sender, RoutedEventArgs e)
        {
        }
        void c2_Selected(object sender, RoutedEventArgs e)
        {
        }
        void c3_Selected(object sender, RoutedEventArgs e)
        {
        }

        //************ResultControl's Handlers
        void btn_result_back_Click(object sender, RoutedEventArgs e)
        {
            resultControl.wp.Children.Clear();
            MainGrid.Children.Clear();
            MainGrid.Children.Add(mainControl); 

        }
        void btn_remove_Click(object sender, RoutedEventArgs e)
        {
            resultControl.wp.Children.Clear();
            resultControl.DeleteDB();

        }
        
    }
}
