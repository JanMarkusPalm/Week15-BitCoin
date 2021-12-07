using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitCoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            bitcoinRate currentBitCoin = GetRates();
            Console.WriteLine($"current rate: {currentBitCoin.bpi.EUR.code} {currentBitCoin.bpi.EUR.rate_float}");
            Console.WriteLine("calculate in EUR/USD/GBP");
            string userChoice = Console.ReadLine();
            Console.WriteLine("enter the amount of bitcoins");
            float userCoins = float.Parse(Console.ReadLine());
            float currentRate = 0;

            if (userChoice == "EUR")
            {
                currentRate = currentBitCoin.bpi.EUR.rate_float;
            }
            else if (userChoice == "USD")
            {
                currentRate = currentBitCoin.bpi.USD.rate_float;
            }
            else if (userChoice == "GBP")
            {
                currentRate = currentBitCoin.bpi.GBP.rate_float;
            }
            float result = currentRate * userCoins;
            Console.WriteLine($"your bitcoins are worth {result} {userChoice}");
        }
        public static bitcoinRate GetRates()
        {
            string url = "https://jsoneditoronline.org/#left=local.kikeki&right=local.havaje";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();
            bitcoinRate bitcoindata;
            using (var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoindata = JsonConvert.DeserializeObject<bitcoinRate>(response);
            }
            return bitcoindata;
        }
    }
}
