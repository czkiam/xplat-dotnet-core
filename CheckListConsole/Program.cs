﻿using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace CheckListConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var outputFolder = "reports";
            var outputFile = "report.txt";
            var outputPath = Path.Combine(currentDirectory, outputFolder, outputFile);
            
            var directory = Path.GetDirectoryName(outputPath);

            //will create directory if not exists. 
            Directory.CreateDirectory(directory);
            var file = outputPath;

            var site = "https://g0t4.github.io/pluralsight-dotnet-core-xplat-apps";
            var client = new HttpClient();

            var body = client.GetStringAsync(site);
            Console.WriteLine(body.Result);

            Console.WriteLine();
            Console.WriteLine("Links");
            var links = LinkChecker.Getlinks(body.Result);
            links.ToList().ForEach(Console.WriteLine);

            //will create/ overwrite content of file
            File.WriteAllLines(file, links);
        }
    }
}
