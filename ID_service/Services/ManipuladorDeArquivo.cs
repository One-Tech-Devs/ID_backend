using ID_model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_service.Services
{
    internal class ManipuladorDeArquivo
    {
        private static string FilePath = AppDomain.CurrentDomain.BaseDirectory + "data.txt";
        public static List<ClientModel> ReadFile()
        {
            List<ClientModel> dataList = new List<ClientModel>();
            if (File.Exists(FilePath))
            {
                using (StreamReader sr = File.OpenText(FilePath))
                {
                    while (sr.Peek() >= 0)
                    {
                        string linha = sr.ReadLine();
                        string[] linhaComSplit = linha.Split(';');
                        if (linhaComSplit.Count() == 3)
                        {
                            ClientModel client = new ClientModel();
                            client.Name = linhaComSplit[0];
                            client.Email = linhaComSplit[1];
                            client.PhoneNumber = linhaComSplit[2];
                            dataList.Add(client);
                        }
                    }
                }
            }
            return dataList;
        }

        public static void EscreverArquivo(List<ClientModel> dataList)
        {
            using (StreamWriter sw = new StreamWriter(FilePath, false))
            {
                foreach (ClientModel client in dataList)
                {
                    string linha = string.Format("{0};{1};{2}", client.Name, client.Email, client.PhoneNumber);
                    sw.WriteLine(linha);
                }
                sw.Flush();
            }
        }
    }
}
