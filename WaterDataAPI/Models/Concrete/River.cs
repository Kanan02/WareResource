﻿using WaterDataAPI.Models.Abstract;

namespace WaterDataAPI.Models
{
    public class River:NaturalWaterResource
    {

        public double WaterFlow{ get; set; }
        public double AirTemperature { get; set; }


    }
}
