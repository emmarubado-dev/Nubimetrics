using ConsoleApp.Models;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            //Mejoras que no se realizaron por falta de tiempo:
            /*
             -Automapper
             -Logueo en txt
             -Las URL y path en archivo de settings
             -Validaciones de path
             */
            var currencies =  Helper.GetCurrencies().Result;
            var currenciesConv = new List<CurrencyConvDto>();
            if(currencies.Count > 0)
            {
                foreach(var currency in currencies)
                {
                    var currencyConv = new CurrencyConvDto();
                    currencyConv.decimal_places = currency.decimal_places;
                    currencyConv.description = currency.description;
                    currencyConv.id = currency.id;
                    currencyConv.symbol = currency.symbol;
                    currencyConv.ratio = Helper.GetCurrencyConversion(currency.id, "USD").Result;
                    currenciesConv.Add(currencyConv);
                }
            }

            if (currenciesConv.Count > 0)
            {
                Helper.GenerateCSV(currenciesConv);
                Helper.GenerateJson(currenciesConv);
            }
        }
    }
}
