using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace AddressCaseStudy.Controller
{
    public class AddressController : ApiController
    {
        public string[] getAddressFromTXT()
        {
            ObjectCache cache = MemoryCache.Default;
            var cachedObject = (string[])cache["addressList"];
            if (cachedObject == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(60);
                cachedObject = getAddressList();
                cache.Set("addressList", cachedObject, policy);
                return cachedObject;
            }
            return cachedObject;
        }
        public static string[] getAddressList()
        {
            System.Net.WebClient WebClient = new System.Net.WebClient();
            string dirpath = AppDomain.CurrentDomain.BaseDirectory;
            WebClient.DownloadFile(dirpath + "\\Assets\\addresslist.txt", @"link.txt");
            StreamReader reader = new StreamReader(@"link.txt");
            string addressvalues = reader.ReadToEnd();
            var result = Regex.Split(addressvalues, "\r\n|\r|\n");
            reader.Close();
            reader.Dispose();
            return result;
        }

        public Dictionary<string, int> getDistance(string address, string[] addressFromTXT)
        {
            int distance = 0;
            List<string> addresses = new List<string>();
            Dictionary<string, int> distances = new Dictionary<string, int>();
            try
            {
                addresses.AddRange(addressFromTXT.Select(s => s));
                foreach (var (item, o) in from item in addresses
                                          let url = "https://maps.googleapis.com/maps/api/directions/json?key=AIzaSyAN62dHhzKgfH4Tf3GalKzh69OXCSIM0qo&origin=" + address + "&destination=" + item + "&sensor=false"
                                          let requesturl = url
                                          let content = fileGetContents(requesturl)
                                          let o = JObject.Parse(content)
                                          select (item, o))
                {
                    distance = (int)o.SelectToken("routes[0].legs[0].distance.value");
                    distance = distance / 1000;
                    distances.Add(item, distance);
                }

                return distances;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return distances;
            }
            return distances;
        }

        public string fileGetContents(string fileName)
        {
            string sContents = string.Empty;
            string me = string.Empty;
            try
            {
                if (fileName.ToLower().IndexOf("https:") > -1)
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] response = wc.DownloadData(fileName);
                    sContents = System.Text.Encoding.ASCII.GetString(response);

                }
                else
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                    sContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch { sContents = "unable to connect to server "; }
            return sContents;
        }

        public List<KeyValuePair<string, int>> topFiveAddresses(string fileName, string[] addressFromTXT)
        {
            ObjectCache cache = MemoryCache.Default;
            var cachedObject = (Dictionary<string, int>)cache["distanceCache"];
            if (cachedObject == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(60);
                cachedObject = getDistance(fileName, addressFromTXT);
                cache.Set("distanceCache", cachedObject, policy);
                List<KeyValuePair<string, int>> sortedDict = (from entry in cachedObject orderby entry.Value ascending select entry).Take(5).ToList();
                return sortedDict;
            }
            else
            {
                List<KeyValuePair<string, int>> sortedDict = (from entry in cachedObject orderby entry.Value ascending select entry).Take(5).ToList();
                return sortedDict;
            }


        }
    }


    }
