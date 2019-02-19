﻿using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;
using GoodGamseSimpleBot.Models.Auth;
using GoodGamseSimpleBot.Models.Message;

namespace GoodGamseSimpleBot.Controllers
{
    class DataReader
    {
        string authDataFilePath = string.Concat(Environment.CurrentDirectory, @"\AuthenticateData.txt");
        string messageDataFilePath = string.Concat(Environment.CurrentDirectory, @"\MessagesData.txt");

        private string _authData;
        private List<string> _messageData = new List<string>();

        public string authData { get { return _authData; } }
        public List<string> messageData { get { return _messageData; } }


        public void GetData()
        {
            _authData = GetAuthDataFromFile(authDataFilePath);
            _messageData.AddRange(GetMessagesDataFromFile(messageDataFilePath));
        }

        private string GetAuthDataFromFile(string authDataFilePath)
        {
            if (!File.Exists(authDataFilePath))
            {
                Console.WriteLine("Файл данных пользователя не найден.");
                Thread.Sleep(3000);
                return null;
            }
            try
            {
                using (FileStream fs = File.OpenRead(authDataFilePath))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    
                    return Encoding.Default.GetString(buffer); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Файл поврежден, либо не соответствует формату. Подробности:\n {0}", ex);
                Thread.Sleep(3000);
                return null;
            }
        }


        private string[] GetMessagesDataFromFile(string messageDataFilePath)
        {
            if (!File.Exists(messageDataFilePath))
            {
                Console.WriteLine("Файл данных пользователя не найден.");
                Thread.Sleep(3000);
                return null;
            }
            try
            {
                using (FileStream fs = File.OpenRead(messageDataFilePath))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    var inputText = Encoding.Default.GetString(buffer);
                    var ololo = inputText.Split('\n');
                    return ololo;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Файл поврежден, либо не соответствует формату. Подробности:\n {0}", ex);
                Thread.Sleep(3000);
                return null;
            }
        }

    }
}
