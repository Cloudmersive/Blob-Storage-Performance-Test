using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobPerformanceTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime startTime = DateTime.Now;

            string ConnectionString = "input";
            string ContainerName = "input";
            string BlobPath = "input";

            BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

            BlobClient blobClient = containerClient.GetBlobClient(BlobPath);

            using (BlobDownloadInfo download = blobClient.DownloadAsync().Result)
            {
                using (BinaryReader reader = new BinaryReader(download.Content))
                {
                    const int BytesToRead = 1000 * 1000000;
                    var fileData = reader.ReadBytes(BytesToRead);
                }

            }

            var TestDuration = DateTime.Now.Subtract(startTime);

            Console.WriteLine(TestDuration.TotalMinutes.ToString() + " Minutes to Download");
        }
    }
}
