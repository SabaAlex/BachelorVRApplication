using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Models
{
    public class JSONData
    {
        public string data { get; set; }

        public float[] GetArrayData()
        {
            return data.Split(',').Select(float.Parse).ToArray();
        }
    }
}
