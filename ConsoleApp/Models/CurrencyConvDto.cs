﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Models
{
    public class CurrencyConvDto
    {
        public string id { get; set; }
        public string symbol { get; set; }
        public string description { get; set; }
        public int decimal_places { get; set; }
        public double ratio { get; set; }
    }
}
