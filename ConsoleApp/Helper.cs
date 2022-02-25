using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
   public static  class Helper
   {
        public static async Task<List<CurrencyDto>> GetCurrencies()
        {
            var currencies = new List<CurrencyDto>();
            using (var httpClient = new HttpClient())
            {
                var task = await httpClient.GetAsync($"https://api.mercadolibre.com/currencies/");
                var taskResponse = await task.Content.ReadAsStringAsync();

                if (!String.IsNullOrEmpty(taskResponse))
                {
                   currencies = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CurrencyDto>>(taskResponse);
                }
              
            }

            return currencies;
        }

        public static async Task<double> GetCurrencyConversion(string currency, string currencyTo)
        {
            double ratio = 0;
            using (var httpClient = new HttpClient())
            {
                var task = await httpClient.GetAsync($"https://api.mercadolibre.com/currency_conversions/search?from={currency}&to={currencyTo}");
                var taskResponse = await task.Content.ReadAsStringAsync();

                if (!String.IsNullOrEmpty(taskResponse))
                {
                    var currencyConver = Newtonsoft.Json.JsonConvert.DeserializeObject<CurrencyConversionDto>(taskResponse);
                    ratio = currencyConver.ratio;
                }

            }

            return ratio;
        }

        

        public static void GenerateCSV(List<CurrencyConvDto> currencyConvs) 
        {
            string csvInfo = string.Join(",", currencyConvs.Select(x => x.ratio.ToString()));
            string path = @"C:\file.csv";
            File.WriteAllText(path, csvInfo);
        }

        public static void GenerateJson(List<CurrencyConvDto> currencyConvs)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(currencyConvs);
            string path = @"C:\file.json";
            File.WriteAllText(path, json);
        }
   }
}
