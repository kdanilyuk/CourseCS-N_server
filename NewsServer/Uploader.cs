using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsServer
{
    class Uploader
    {
        static string filePath = @"D:\Study\4sem\КСиС\Ксис курач\data.txt";
        static string newsPath = @"D:\Study\4sem\КСиС\Ксис курач\news.txt";
        static string[] login = new string[File.ReadAllLines(filePath).Length];
        static string[] password = new string[File.ReadAllLines(filePath).Length];

        public static string[] InitLogin()
        {
            login = new string[File.ReadAllLines(filePath).Length];
            int i = 0;
            using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(new char[] { '#' });
                    login[i] = words[0];
                    i++;
                }
            }
            return login;
        }
        public static string[] InitPassword()
        {
            password = new string[File.ReadAllLines(filePath).Length];
            int i = 0;

            using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(new char[] { '#' });
                    password[i] = words[1];
                    i++;
                }
            }
            return password;
        }

        public static string GetActualNews()
        {
            string news = "";

            using (StreamReader sr = new StreamReader(newsPath, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    news = news + line + "#\n";
                }
            }

            return news;
        }

        public static void AppendNews(string text)
        {
            StreamWriter sw = new StreamWriter(newsPath, true);
            sw.WriteLine(text);
            sw.Close();
        }
    }
}
