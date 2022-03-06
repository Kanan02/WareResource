using WaterDataAPI.Models.Concrete;
using WaterResourcesManager.Models.Abstract;
using WaterResourcesManager.Models.Concrete;

namespace WaterResourcesManager
{
    public class LitresDistribution
    {
        /*
         
            Simple algorith to find best distribution of water resources used by farm owner
         
         */



        private List<WaterResource> _waterResources;  // Water resources user by farmer 
        private double _neededWater;  // in l

        // amount of water left to be taken from possible water sources
        private double _waterLeft;  // in l


        private string _city;


        public LitresDistribution(double area, string product, string fieldname, string city, List<WaterResource> resourcesUsed)
        {
            _waterResources = resourcesUsed;
            _neededWater = GetAmountOfWaterUsage(area, product, fieldname, city);
            _waterLeft = _neededWater;
            _city = city;
        }


        private double GetAmountOfWaterUsage(double area, string product, string fieldname, string city)
        {
            LitresForAreaCounter litresForAreaCounter = new LitresForAreaCounter();

            return litresForAreaCounter.CalculateLitres(area, product, fieldname, city);
        }


        /// <summary>
        ///  Check if rain reservoir is empty
        ///  If false use rain water(add resource and used amount of water to table)
        /// </summary>
        private void DistributeRainWaterReservoir(Dictionary<WaterResource, double> waterDistributionTable, double minPh, double maxPh)
        {
            try
            {
                for (int i = 0; i < _waterResources.Count; i++)
                {
                    if (_waterResources[i].GetType() == typeof(RainWaterReservoir))
                    {
                        if (!(_waterResources[i] as RainWaterReservoir).IsEmpty() && _waterResources[i].PollutionLevel >= minPh && _waterResources[i].PollutionLevel <= maxPh)
                        {
                            double waterInReservoir = (_waterResources[i] as RainWaterReservoir).GetCurrentWaterVolume() * 1000;  // conversion from m3 to litres
                            if (_waterLeft - waterInReservoir <= 0)
                            {
                                waterDistributionTable.Add(_waterResources[i], _waterLeft);
                                _waterLeft = 0;
                            }
                            else
                            {
                                waterDistributionTable.Add(_waterResources[i], waterInReservoir);
                                _waterLeft -= waterInReservoir;
                            }
                        }
                        else
                        {
                            waterDistributionTable.Add(_waterResources[i], 0);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\tMessage: " + ex.Message + "\n\n\tStack Trace: " + ex.StackTrace);
            }
        }


        /// <summary>
        ///  Check if ground reservoir is empty
        ///  If false use ground water(add resource and used amount of water to table)
        /// </summary>
        private void DistributeGroundWaterReservoir(Dictionary<WaterResource, double> waterDistributionTable, double minPh, double maxPh)
        {
            try
            {
                for (int i = 0; i < _waterResources.Count; i++)
                {
                    if (_waterResources[i].GetType() == typeof(GroundWaterReservoir))
                    {
                        if (!(_waterResources[i] as GroundWaterReservoir).IsEmpty() && _waterResources[i].PollutionLevel >= minPh && _waterResources[i].PollutionLevel <= maxPh)
                        {
                            double waterInReservoir = (_waterResources[i] as GroundWaterReservoir).GetCurrentWaterVolume() * 1000;  // conversion from m3 to litres
                            if (_waterLeft - waterInReservoir <= 0)
                            {
                                waterDistributionTable.Add(_waterResources[i], _waterLeft);
                                _waterLeft = 0;
                            }
                            else
                            {
                                waterDistributionTable.Add(_waterResources[i], waterInReservoir);
                                _waterLeft -= waterInReservoir;
                            }
                        }
                        else
                        {
                            waterDistributionTable.Add(_waterResources[i], 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\tMessage: " + ex.Message + "\n\n\tStack Trace: " + ex.StackTrace);
            }
        }


        /// <summary>
        /// Check if channel water height is higher, than critical height
        /// If true use channel water
        /// </summary>
        /// <returns>percent of water taken</returns>
        private void DistributeChannelWater(Dictionary<WaterResource, double> waterDistributionTable, double minPh, double maxPh)
        {
            try
            {
                for (int i = 0; i < _waterResources.Count; i++)
                {
                    if (_waterResources[i].GetType() == typeof(Channel))
                    {
                        if ((_waterResources[i] as Channel).CurrentWaterHeight > (_waterResources[i] as Channel).CriticalWaterLevel
                            && _waterResources[i].PollutionLevel >= minPh && _waterResources[i].PollutionLevel <= maxPh)
                        {
                            waterDistributionTable.Add(_waterResources[i], _waterLeft);
                            _waterLeft = 0;
                        }
                        else if ((_waterResources[i] as Channel).CurrentWaterHeight > (_waterResources[i] as Channel).CriticalWaterLevel)
                        {
                            waterDistributionTable.Add(_waterResources[i], _waterLeft * 0.2);
                            _waterLeft -= _waterLeft * 0.2;
                        }
                        else
                        {
                            waterDistributionTable.Add(_waterResources[i], 0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\tMessage: " + ex.Message + "\n\n\tStack Trace: " + ex.StackTrace);
            }
        }


        /// <summary>
        /// Take water from paid reservoir if there is any
        /// </summary>
        private void DistributeWaterReservoir(Dictionary<WaterResource, double> waterDistributionTable)
        {
            try
            {
                for (int i = 0; i < _waterResources.Count; i++)
                {
                    if (_waterResources[i].GetType() == typeof(WaterReservoir))
                    {
                        waterDistributionTable.Add(_waterResources[i], _waterLeft);
                        _waterLeft = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\tMessage: " + ex.Message + "\n\n\tStack Trace: " + ex.StackTrace);
            }
        }


        /// <summary>
        /// Take water from channel, with critical level of water, when no other sources left
        /// </summary>
        private void DistributeChannelWaterCRITICAL(Dictionary<WaterResource, double> waterDistributionTable)
        {
            for (int i = 0; i < _waterResources.Count; i++)
            {
                if (_waterResources[i].GetType() == typeof(Channel))
                {
                    if ((_waterResources[i] as Channel).CurrentWaterHeight > 0)
                    {
                        waterDistributionTable.Add(_waterResources[i], _waterLeft);
                        _waterLeft = 0;
                    }
                }
            }
        }



        public Dictionary<WaterResource, double> FindBestDistribution()
        {
            Dictionary<WaterResource, double> waterDistributionTable = new Dictionary<WaterResource, double>();

            try
            {
                double minPh = 6.5;
                double maxPh = 8.5;

                DistributeRainWaterReservoir(waterDistributionTable, minPh, maxPh);


                if (_waterLeft > 0)
                    DistributeGroundWaterReservoir(waterDistributionTable, minPh, maxPh);


                if (_waterLeft > 0)
                    DistributeChannelWater(waterDistributionTable, minPh, maxPh);



                if (_waterLeft > 0)
                    DistributeWaterReservoir(waterDistributionTable);



                if (_waterLeft > 0)
                    DistributeChannelWaterCRITICAL(waterDistributionTable);

            }
            catch (Exception ex)
            {
                Console.WriteLine("\tMessage: " + ex.Message + "\n\n\tStack Trace: " + ex.StackTrace);
            }


            return waterDistributionTable;
        }




    }
}
