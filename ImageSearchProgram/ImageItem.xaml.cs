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
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Xml;

namespace ImageSearchProgram
{
    /// <summary>
    /// Interaction logic for ImageItem.xaml
    /// </summary>
    public partial class ImageItem : UserControl
    {
        public ImageItem(XmlNode paramXmlNode)
        {
            //Mission : Make a clickable image
            InitializeComponent();

            //I was wondering how I can set Source of image 
            // reference 1 : http://stackoverflow.com/questions/350027/setting-wpf-image-source-in-code
            // reference 2 : http://stackoverflow.com/questions/2720463/creating-a-clickable-image-in-wpf
            // reference 3 : http://stackoverflow.com/questions/15627123/adding-an-image-inside-a-button-programmatically

            /*
             * as long as you insert <Button.Template> and <ControlTemplate> tag inside <Button> tag, you can make a button Hidden
            <Button Height="150" Width="150">
                <Button.Template>
                    <ControlTemplate></ControlTemplate>
                </Button.Template>
            </Button>
             */

            //Button btn = new Button { 
            //    Width = 150,
            //    Height = 150,
            //    Content = new Image
            //    {
            //        Source = new BitmapImage(new Uri(paramXmlNode.InnerXml)),
            //        VerticalAlignment = VerticalAlignment.Center,
            //        Stretch = Stretch.Fill
            //    }
            //};

            //gridImg.Children.Add(btn);






            ///////////////////



            /////////////////////

            //btn = new Button
            //{
            //    Content = new Image
            //    {
            //        Source = new BitmapImage(new Uri(paramXmlNode.InnerXml)),
            //        VerticalAlignment = VerticalAlignment.Center,
            //        Stretch = Stretch.Fill,
            //        Name = "img",

            //    }
            //};

            //btn.Click += btn_Click;

            //gridImg.Children.Add(btn);

            ///////////////////////////////////

            Image image = new Image();
            image.Width = 150;
            image.Height = 150;


            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(paramXmlNode.InnerXml);
            logo.EndInit();
            image.Source = logo;
            image.MouseDown += new MouseButtonEventHandler(image_DoubleClick);

            gridImg.Children.Add(image);

        }

        public void image_DoubleClick(object sender, MouseButtonEventArgs e)
        {

           //MyNewWindow window2 = new MyNewWindow();
            
            Image paramimg = sender as Image;

            
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                MyNewWindow win2 = new MyNewWindow();

                ImageBrush myBrush = new ImageBrush();
                Image image = new Image();
                image.Source = new BitmapImage(
                    new Uri(
                       paramimg.Source.ToString()));
                myBrush.ImageSource = image.Source;
                myBrush.Stretch = Stretch.None;
                Grid grid = new Grid();
                //grid.Background = myBrush;

                win2.Content = grid;
                //Image image = new Image();
                
                //BitmapImage logo = new BitmapImage();

                //logo.BeginInit();
                //logo.UriSource = new Uri(paramimg.Source.ToString());
                //logo.EndInit();
                //image.Source = logo;
                //win2.myNewGrid.Children.Add(image);
                
                win2.Show();

                //MessageBox.Show(paramimg.Source.ToString());
                win2.Closing += win2_Closing;
            }
                
        }

        void win2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

    }
}
