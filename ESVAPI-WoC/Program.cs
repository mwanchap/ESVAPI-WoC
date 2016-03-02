using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net;

namespace ESVAPI_WoC
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebClient client = new WebClient())
            {
                var url = "http://www.esvapi.org/v2/rest/passageQuery?key=IP&passage=John+1";
                var resp = client.DownloadString(url);
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(resp);

                if (doc.ParseErrors != null && doc.ParseErrors.Count() > 0)
                {
                    throw new Exception("there are parse errors");
                }
                else
                {
                    if (doc.DocumentNode != null)
                    {
                        var wocNodes = doc.DocumentNode.SelectNodes("//*[contains(concat(\" \", normalize-space(@class), \" \"), \" woc \")]");

                        if (wocNodes != null)
                        {
                            // Do something with bodyNode
                            foreach(var node in wocNodes)
                            {
                                Console.WriteLine(node.InnerText);
                            }
                        }
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
