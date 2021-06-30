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

namespace ConsoleAppHomework08
{
    class Program
    {
        public static volatile Hashtable urls = new Hashtable();
        public static int count = 0;
        public string myHtml;
        static void Main(string[] args)
        {
            Program program = new Program();

            string startUrl = "https://www.sina.com.cn/";
            if (args.Length >= 1) startUrl = args[0];

            Program.urls.Add(startUrl, false);
            ThreadPool.SetMinThreads(5,10);
            while (Program.count < 20)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(program.Crawl),program);
                Thread.Sleep(3000);
            }

        }
        private void Crawl(object program)
        {
            Program pro = (Program)program;
            Console.WriteLine("Thread "+Thread.CurrentThread.ManagedThreadId+" Start Crawling....");
            while (true)
            {
                string current = null;
                foreach (string url in urls.Keys)
                {
                    if ((bool)urls[url])
                    {
                        continue;
                    }
                    current = url;
                    
                }
                if (current == null || count > 20)
                {
                    break;
                }
                Console.WriteLine("Crawling " + current + " Page");
                lock (this) {
                    urls[current] = true;
                    string html = pro.Download(current);

                    pro.Parse(html);
                }
                Thread.Sleep(2000);
            }

        }
        public string Download(string url)
        {

            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = Encoding.UTF8;
                
                webClient.DownloadStringAsync(new Uri(url));
                
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(getResult);
                Thread.Sleep(1500);
                return myHtml;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }
        
        public void getResult(Object sender, DownloadStringCompletedEventArgs e)
        {
            string html="";
            try { 
              html  = e.Result; 
            }
            catch(Exception ex)
            {
                myHtml = "";
                Console.WriteLine(ex.Message);
                return;
            }
            
            if (!(html.Contains("<!DOCTYPE html>"))|| html.Length < 4096)
            {
                myHtml = "";
                return;
            }
            string fileName = count.ToString() + ".html";

            Console.WriteLine("Write to file: " + fileName);
            File.WriteAllText(fileName, html, Encoding.UTF8);
            count++;
            myHtml = html;
            

        }
        public void Parse(string html)
        {
            string strRef = @"(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]";
            MatchCollection matches = new Regex(strRef).Matches(html);
            foreach (Match match in matches)
            {
                // strRef = match.Value.Substring(match.Value.IndexOf('=') + 1).Trim('"', '\'', '#', ' ', '>');
                strRef = match.Value;
                if (strRef.Length < 5||strRef.Contains("rss") || strRef.Contains("ssl")||strRef.Contains("png")||strRef.Contains("js") || strRef.Contains("aliyun"))
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
