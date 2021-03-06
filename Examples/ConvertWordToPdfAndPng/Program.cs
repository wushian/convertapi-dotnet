﻿using System;
using System.IO;
using ConvertApiDotNet;

namespace ConvertWordToPdfAndPng
{
    class Program
    {
        /// <summary>
        /// Example of saving Word docx to PDF and to PNG
        /// https://www.convertapi.com/docx-to-pdf
        /// https://www.convertapi.com/docx-to-png
        /// </summary>
        static void Main(string[] args)
        {
            //Get your secret at https://www.convertapi.com/a
            var convertApi = new ConvertApi("your api secret");
            const string sourceFile = @"..\..\..\TestFiles\test.docx";

            var fileParam = new ConvertApiParam("File", File.OpenRead(sourceFile));

            var convertToPdf = convertApi.ConvertAsync("docx", "pdf", new[]
            {
                fileParam
            });

            var outputFileName = convertToPdf.Result.Files[0];
            var fileInfo = outputFileName.AsFileAsync(Path.Combine(Path.GetTempPath(), outputFileName.FileName)).Result;

            Console.WriteLine("The PDF saved to " + fileInfo);
            
            var convertToPng = convertApi.ConvertAsync("docx", "png", new[]
            {
                //Reuse the same uploaded file parameter
                fileParam
            });

            foreach (var processedFile in convertToPng.Result.Files)
            {
                fileInfo = processedFile.AsFileAsync(Path.Combine(Path.GetTempPath(), processedFile.FileName)).Result;
                Console.WriteLine("The PNG saved to " + fileInfo);
            }

            Console.ReadLine();
        }
    }
}
