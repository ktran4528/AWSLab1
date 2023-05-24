using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon;

namespace _301154481Tran_Lab1
{
    public static class Helper
    {
        public readonly static IAmazonS3 s3Client;

        static Helper()
        {
            s3Client = GetS3Client();
        }

        private static IAmazonS3 GetS3Client()
        {
            string awsAccessKey = "Enter Access Key";
            string awsSecretKey = "Enter Secret Key";
            return new AmazonS3Client(awsAccessKey, awsSecretKey, RegionEndpoint.CACentral1);
        }




       

    }
}
