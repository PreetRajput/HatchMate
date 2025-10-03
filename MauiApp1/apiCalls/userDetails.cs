using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.apiCalls
{
    internal class userDetails
    {
        public string? id { get; set; }

        public string? username { get; set; }

        public string? petName { get; set; }

        public int petNum { get; set; }

        public List<string>? task { get; set; }
    }
}
