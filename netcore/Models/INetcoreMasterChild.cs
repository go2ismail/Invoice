using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore.Models
{
    public class INetcoreMasterChild : INetcoreBasic
    {
        //never used to store data, just a mark for master detail
        public string HasChild { get; set; }
    }
}
