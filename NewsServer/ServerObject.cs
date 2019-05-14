using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NewsServer
{
    class ServerObject
    {
        public static void StartServer()
        {
            string[] login = Uploader.InitLogin();
            string[] password = Uploader.InitPassword();

            const int port = 8888;//11000
            const string path = @"D:\Study\4sem\КСиС\Ксис курач\data.txt";

            TcpListener server = null;
            try
            {
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                server = new TcpListener(localAddr, port);//IPAddress.Any

                // запуск слушателя
                server.Start();

                while (true)
                {
                    string response = "";
                    // получаем входящее подключение
                    TcpClient client = server.AcceptTcpClient();      
                    // получаем сетевой поток для чтения и записи
                    NetworkStream stream = client.GetStream();

                    // получаем сообщение
                    byte[] data = new byte[256];
                    StringBuilder resp = new StringBuilder();

                    do
                    {
                        int bytes = stream.Read(data, 0, data.Length);
                        resp.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable); // пока данные есть в потоке

                    //проверка на совпадение
                    string[] words = resp.ToString().Split(new char[] { '#' });
                    bool result;

                    string answer = words[0];
                    switch (answer)
                    {
                        case "SIGNIN":
                            result = Equals(words[1], words[2]);
                            // сообщение для отправки клиенту
                            response = result.ToString();
                            break;
                        case "SIGNUP":
                            result = AppendToBase(words[1], words[2]);
                            response = result.ToString();
                            break;
                        case "GETNEWS":
                            response = Uploader.GetActualNews();
                            break;
                        case "OFFERNEWS":
                            Uploader.AppendNews(words[1] + "#" + words[2] + "#" + words[3]);
                            break;

                        default:
                            break;
                    }
                    
                    // преобразуем сообщение в массив байтов
                    byte[] sendData = Encoding.UTF8.GetBytes(response);
                    
                    // отправка сообщения
                    stream.Write(sendData, 0, sendData.Length);
                    // закрываем поток
                    stream.Close();
                    // закрываем подключение
                    client.Close();
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (server != null)
                    server.Stop();
            }

            bool Equals(string log, string pass)
            {
                login = Uploader.InitLogin();
                password = Uploader.InitPassword();
                bool res = false;
                for (int i = 0; i < login.Length; i++)
                {
                    if (log == login[i])
                    {
                        if (pass == password[i])
                        {
                            res = true;
                        }
                    }
                }
                return res;
            }
            
            bool AppendToBase(string log, string pass)
            {
                bool result = true;

                if (EqualsLogin(log))
                {
                    result = false;
                }
                else
                {
                    StreamWriter sw = new StreamWriter(path, true);
                    sw.WriteLine(log + "#" + pass);
                    sw.Close();
                }

                return result;
            }

            bool EqualsLogin(string log)
            {
                login = Uploader.InitLogin();
                password = Uploader.InitPassword();
                bool res = false;
                for (int i = 0; i < login.Length; i++)
                {
                    if (log == login[i])
                    {
                        res = true;
                    }
                }
                return res;
            }
        }
    }
}
