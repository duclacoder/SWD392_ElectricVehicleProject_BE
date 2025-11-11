using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV.Application.CustomEntities
{
    public class PostStatisticCustom
    {
        public int TotalPosts { get; set; }
        public int ActivePosts { get; set; }
        public int InactivePosts { get; set; }
        public int VehiclePosts { get; set; }
        public int BatteryPosts { get; set; }
    }
}
