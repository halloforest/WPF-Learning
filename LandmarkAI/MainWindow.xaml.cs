using LandmarkAI.Classes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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

namespace LandmarkAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            selectImageButton.Click += SelectImageButton_Click;
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png; *.jpg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            
            // dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (true == dialog.ShowDialog())
            {
                string fileName = dialog.FileName; // Path & Filename

                infoTextBlock.Text = "File uploaded: \n" + fileName;
                selectedImage.Source = new BitmapImage(new Uri(fileName));

                _ = MakePredictionAsync(fileName);
            }
        }

        private async Task MakePredictionAsync(string fileName)
        {
            string url = "https://westeurope.api.cognitive.microsoft.com/customvision/v3.0/Prediction/3d57c8c9-c521-4b65-8116-db77d9cfaa79/classify/iterations/Iteration1/image";
            string predictionKey = "127467ab4f1346da9424e175f6df0fa1";
            string contentType = "application/octet-stream";


            HttpResponseMessage response = null;
            // Open the file -> streaming the content -> post to URL
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var content = new StreamContent(fileStream))
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Prediction-Key", predictionKey);
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                        response = await client.PostAsync(url, content);                        
                    }                    
                }
            }
            var responseString = await response.Content.ReadAsStringAsync();

            infoTextBlock.Text = "Response from Service: \n" + responseString;

            var predictions = (List<Prediction>)(JsonConvert.DeserializeObject<CustomVision>(responseString)).predictions;
            predictionListView.ItemsSource = predictions;
        }
    }
}
