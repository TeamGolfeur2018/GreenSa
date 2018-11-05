﻿using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSa.Models.Tools.Services
{
    class GpsService 
    {
        public GpsService()
        {//STATIC, NOT USE
        }

        public static async Task<MyPosition> getCurrentPosition()  
        {
            CrossGeolocator.Current.DesiredAccuracy = 5;

            if (!isAvaible()) throw new NotAvaibleException();
            Plugin.Geolocator.Abstractions.Position position = await CrossGeolocator.Current.GetPositionAsync(TimeSpan.FromSeconds(15), null, false);
            return new MyPosition(position.Latitude, position.Longitude);
        }

        public static bool isAvaible()
        {
          
                return  CrossGeolocator.Current.IsGeolocationAvailable && CrossGeolocator.Current.IsGeolocationEnabled;
        }


    }
}
