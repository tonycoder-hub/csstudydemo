using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppHomework07
{
    class Program
    {
        private Hashtable urls = new Hashtable();
        private int count = 0;
        static void Main(string[] args)
        {
            Program program = new Program();

            string startUrl = "http://www.cnblogs.com/dstang2000/";
            if (args.Length >= 1) startUrl = args[0];

            program.urls.Add(startUrl, false);
            program.Crawl();
            Console.ReadKey();
        }
        public void Crawl()
        {
            Console.WriteLine("Start Crawling....");
            while (true)
            {
                string current = null;
                foreach(string url in urls.Keys)
                {
                    if ((bool)urls[url])
                    {
                        continue;
                    }
                    current = url; 
                }
                if (current == null || count > 10)
                {
                    break;
                }
                Console.WriteLine("Crawling "+current+" Page");
                string html = Download(current);
                
                urls[current] = true;
                
                Parse(html);
                foreach (string i in urls.Keys)
                {
                    Console.WriteLine(i);
                }
            }

        }
        public string Download(string url)
        {
            
            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                
                string html = webClient.DownloadString(url);
                if(!html.Contains("<!DOCTYPE html>")||html.Length<4096)
                {
                    return "";
                }
                string fileName = count.ToString()+".html";
                File.WriteAllText(fileName, html, Encoding.UTF8);
                count++;
                return html;
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }

        public void Parse(string html)
        {
            string strRef = @"(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach(Match match in matches)
            {
                strRef = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\'', '#', ' ', '>');
                
                if (strRef.Length < 5)
                {
                    continue;
                }
                /*string type = strRef.Substring(strRef.Length - 5, 4).ToLower();
                if (!type.Equals("html"))
                {
                    continue;
                }*/
                if (urls[strRef] == null)
                {
                    urls[strRef] = false;
                }

            }
        }
    }
}
