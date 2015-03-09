using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace cache_slow_PA2
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        private string Filename = HttpContext.Current.Server.MapPath(@"/data.txt");
        public static Dictionary<string, List<string>> cache;

        [WebMethod]
        public List<string> GetPrefixSuggestions(string prefix)
        {
            if (cache == null)
            {
                cache = new Dictionary<string, List<string>>();
            }

            if (cache.ContainsKey(prefix))
            {
                return cache[prefix];
            }
            else
            {
                List<string> suggestions = new List<string>();
                using (StreamReader sr = new StreamReader(Filename))
                {
                    while (sr.EndOfStream == false)
                    {
                        string line = sr.ReadLine().ToLower();

                        if (line.StartsWith(prefix))
                        {
                            suggestions.Add(line);
                        }
                    }
                }
                cache[prefix] = suggestions;

                return suggestions;
            }
        }
    }
}