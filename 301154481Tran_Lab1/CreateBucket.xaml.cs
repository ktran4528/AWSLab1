using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _301154481Tran_Lab1
{
    /// <summary>
    /// Interaction logic for CreateBucket.xaml
    /// </summary>
    public partial class CreateBucket : Window
    {


        private static IAmazonS3 s3Client;


        public CreateBucket()
        {
            InitializeComponent();
        }


        async void OnLoad(object sender, RoutedEventArgs e)
        {
           RefreshData();
        }

        public class BucketData
        {
            public string bucketName { get; set; }
            public DateTime CreationTime { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string bucketName = newTxtBucketName.Text;


            if (!string.IsNullOrEmpty(bucketName))
            {

                var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion = true
                    };
                var response = Helper.s3Client.PutBucketAsync(putBucketRequest);


                
                TextBox textBox = newTxtBucketName as TextBox;
                if (textBox != null)
                {
                    textBox.Text = string.Empty;
                    RefreshData();

                }

                if (textBox.Text.Equals(null))
                {
                    RefreshData();
                }
                RefreshData();

            }

            else
            {
                RefreshData();
            }


        }

        public async void RefreshData()
        {


            ListBucketsResponse bucketResponse = await Helper.s3Client.ListBucketsAsync();

            // Clear the DataGrid's items and add a column for each object property you want to display
            dataGrid.Items.Clear();
            dataGrid.Columns.Clear();
            dataGrid.Columns.Add(new DataGridTextColumn {
                Header = "Object Name",
                Binding = new Binding("Name"),
            HeaderStyle = new Style(typeof(DataGridColumnHeader))
            {
                Setters =
        {
            new Setter(MarginProperty, new Thickness(0, 0, 325, 0))
        }
            }
            });
            dataGrid.Columns.Add(new DataGridTextColumn {
                Header = "Creation Date", 
                Binding = new Binding("Date"),
            HeaderStyle = new Style(typeof(DataGridColumnHeader))
            {
                Setters =
        {
            new Setter(MarginProperty, new Thickness(0, 0, 325, 0))
        }
            }
            });

            // Add each object to the DataGrid
            foreach (var obj in bucketResponse.Buckets)
            {
                dataGrid.Items.Add(new
                {
                    Name = obj.BucketName,
                    Date = obj.CreationDate

                });
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();

            mainWindow.Show();

            this.Close();
        }
    }
}
