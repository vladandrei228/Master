using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Master
{
    class Program
    {
        static void Main(string[] args)
        {
           
                //TcpListener serverSocket = new TcpListener(port);
                TcpListener server = new TcpListener(IPAddress.Loopback, 6789);
                server.Start();
                Console.WriteLine("Server listning on port: " + 6789);

                TcpClient connectionSocket = server.AcceptTcpClient();
                Console.WriteLine("Server activated");

                Stream ns = connectionSocket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true; // enable automatic flushing

                string request = sr.ReadLine();

                //Read the passwords into a list
                List<UserInfo> userInfos =
                PasswordFileHandler.ReadPasswordFile("passwords.txt");

                //Read the Desctionary
                List<List<string>> chunks = new List<List<string>>();

                using (FileStream fs = new FileStream("webster-dictionary.txt", FileMode.Open, FileAccess.Read))

                using (StreamReader dictionary = new StreamReader(fs))
                {
                    while (!dictionary.EndOfStream)
                    {
                        List<string> tempList = new List<string>();

                        //if you want to send chunks to the client then create chunks
                        //You must use a logic where it puts 10000 dictinary words in tempList and then
                        //Adds tempList to chucks
                        tempList.Add(dictionary.ReadLine());
                        //after the modulus % condition is satisfied i.e. when there is 10000 words in tempList
                        //then
                        chunks.Add(tempList);

                        // IEnumerable<UserInfoClearText> partialResult = CheckWordWithVariations(dictionaryEntry, userInfos);
                        // result.AddRange(partialResult);
                    }
                }

                //now you can send the first chuck to the Slave i.e. the client

            
        }
    }
}
