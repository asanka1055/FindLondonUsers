using LondonUsers.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Device.Location;

namespace LondonUsers.Controllers
{
    public class UsersController : ApiController
    {     

        [Route("~/api/users/filtered-users")]
        public List<User> GetFilteredUsers()
        {
            var jsonResult = "";
            var usersLondon = new List<User>();
            using (WebClient wc = new WebClient())
            {
                //used to enable TLS Security Protocol to access swagger url
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                jsonResult = wc.DownloadString("https://dwp-techtest.herokuapp.com/users");

                //Convert London Latitude and Longitude to double
                // 51 deg 50 min 74 sec N
                double londonLat = 51 + (50 / 60.0) + (74 / 60.0 / 60.0);

                // 0 deg 12 min 77 sec W
                double londonLon = 0 - (12 / 60.0) - (77 / 60.0 / 60.0);

                dynamic array = JsonConvert.DeserializeObject(jsonResult);

                foreach (var item in array)
                {
                    double userLat = item.latitude;
                    double userLon = item.longitude;

                    //To avoid invalid data error
                    if (londonLat >= -90 && londonLat <= 90 && userLat >= -90 && userLat <= 90 && londonLon >= -180 && londonLon <= 180 && userLon >= -180 && userLon <= 180)
                    {
                        try {

                            //used GeoCoordinate Nuget library to get distance between geo points
                            var sCoord = new GeoCoordinate(londonLat, londonLon);
                            var eCoord = new GeoCoordinate(userLat, userLon);

                            double distanceInMeters = sCoord.GetDistanceTo(eCoord);

                            double distanceInMiles = distanceInMeters / 1609.34;

                            if (distanceInMiles <= 50)
                            {
                                User user = new User();

                                user.Id         = item.id;
                                user.FirstName  = item.first_name;
                                user.LastName   = item.last_name;
                                user.Email      = item.email;
                                user.IpAddress  = item.ip_address;
                                user.Longitude  = item.longitude;
                                user.Laitude    = item.latitude;

                                usersLondon.Add(user);
                            }   
                        
                        }catch(Exception e){

                            Console.WriteLine("GeoCoordinateException: {0}", e.Message);
                        }                       
                            
                    }
                           
                }
                      
            }
            return usersLondon;
        }

        [Route("~/api/users/london-users")]
        public List<User> GetLondonUsers()
        {
            var jsonResult = "";
            var usersLondon = new List<User>();
            using (WebClient wc = new WebClient())
            {
                //used to enable TLS Security Protocol to access swagger url
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                jsonResult = wc.DownloadString("https://bpdts-test-app.herokuapp.com/city/London/users");

                dynamic array = JsonConvert.DeserializeObject(jsonResult);

                foreach (var item in array)
                {
                    User user = new User();

                    user.Id         = item.id;
                    user.FirstName  = item.first_name;
                    user.LastName   = item.last_name;
                    user.Email      = item.email;
                    user.IpAddress  = item.ip_address;
                    user.Longitude  = item.longitude;
                    user.Laitude    = item.latitude;

                    usersLondon.Add(user);
                }
            }
            return usersLondon;
        }
    }
}
