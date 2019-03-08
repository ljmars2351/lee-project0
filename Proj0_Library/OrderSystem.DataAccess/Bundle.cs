using System;
using System.Collections.Generic;

namespace OrderSystem.DataAccess
{
    public partial class Bundle
    {
        public int Id { get; set; }
        public decimal? Price { get; set; }
        public string BundleName { get; set; }
    }
}
