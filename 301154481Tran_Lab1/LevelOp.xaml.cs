using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace _301154481Tran_Lab1
{
    /// <summary>
    /// Interaction logic for LevelOp.xaml
    /// </summary>
    public partial class LevelOp : Window
    {

        public LevelOp()
        {
            InitializeComponent();
        }

        async void OnLoad(object sender, RoutedEventArgs e)
        {
            // Call the ListBucketsAsync method to get a list of all the S3 buckets in your account
            var response = await Helper.s3Client.ListBucketsAsync();

            // Add the name of each bucket to the ComboBox
            foreach (var bucket in response.Buckets)
            {
                CmboxBucket.Items.Add(bucket.BucketName);
            }
        }



        private async void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            RefereshGrid();

  
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string selectedBucket = CmboxBucket.SelectedItem.ToString();


            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                txtObjName.Text = filePath;



            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string selectedBucket = CmboxBucket.SelectedItem.ToString();

            var fileTransferUtility = new TransferUtility(Helper.s3Client);

            //  Option 1.Upload a file. The file name is used as the object key name.
            await fileTransferUtility.UploadAsync(txtObjName.Text, selectedBucket);

            // Call the ListBucketsAsync method to get a list of all the S3 buckets in your account

            RefereshGrid();
        }

        public async void RefereshGrid()

        {

            string selectedBucket = CmboxBucket.SelectedItem.ToString();

            var response = await Helper.s3Client.ListObjectsV2Async(new ListObjectsV2Request
            {
                BucketName = selectedBucket
            });
            // Clear the DataGrid's items and add a column for each object property you want to display
            dataGrid.Items.Clear();
            dataGrid.Columns.Clear();
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Object Name", Binding = new Binding("Name") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Size", Binding = new Binding("Size") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Last Modified", Binding = new Binding("LastModified") });

            // Add each object to the DataGrid
            foreach (var obj in response.S3Objects)
            {
                dataGrid.Items.Add(new
                {
                    Name = obj.Key,
                    Size = obj.Size,
                    LastModified = obj.LastModified
                });
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();
            this.Close();
        }
    }
}
