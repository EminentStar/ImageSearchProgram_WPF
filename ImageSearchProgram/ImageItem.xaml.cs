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
            AddImage(paramXmlNode);
        }

        public Image SetImage(string paramStr)
        {
            Image image = new Image();
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(paramStr);
            logo.EndInit();

            image.Source = logo;
            return image;
        }

        public void AddImage(XmlNode paramXmlNode)
        {
            Image image = SetImage(paramXmlNode.InnerXml);

            image.Width = 100;
            image.Height = 100;
            image.MouseDown += new MouseButtonEventHandler(image_DoubleClick);

            gridImg.Children.Add(image);
        }
                
        public void image_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Image paramimg = sender as Image;
            
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                MyNewWindow win2 = new MyNewWindow();

                Image image = SetImage(paramimg.Source.ToString());
                image.Stretch = Stretch.Uniform;

                win2.Topmost = true;
                win2.Content = image;
                
                win2.Show();
                win2.Closing += win2_Closing;
                // MSDN : https://msdn.microsoft.com/en-us/library/ms744952.aspx ex)A hosted Windows Forms control is drawn in a separate HWND, so it is always drawn on top of WPF elements.
            }
        }

        //public void 

        void win2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

    }
}
