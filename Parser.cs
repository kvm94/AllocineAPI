using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AllocineAPI
{
    internal static class Parser
    {
        private static HtmlDocument website;

        //Récupére le document HTML à partir d'une url.
        private static async Task URLToHtml(String url)
        {
            try
            {
                HttpClient http = new HttpClient();
                var response = await http.GetByteArrayAsync(url);
                String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);
                HtmlDocument resultat = new HtmlDocument();
                resultat.LoadHtml(source);

                website = resultat;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<HtmlDocument> GetWebsite(String url)
        {
            if (website != null)
            {
                return website;
            }
            else
            {
                await URLToHtml(url);
                return website;

            }
        }
    }
}
