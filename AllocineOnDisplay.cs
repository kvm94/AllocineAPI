using AllocineAPI.Entyties;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllocineAPI
{
    public class AllocineOnDisplay
    {
        private const String url = "http://www.allocine.fr/film/aucinema/";
        private HtmlDocument website;
        public List<Movie> Movies { get; private set; }

        public AllocineOnDisplay()
        {
            website = null;
            Movies = null;
        }

        public async Task LoadWebsite()
        {
            website = await Parser.GetWebsite(url);
        }

        public void LoadMovies()
        {
            if(website != null)
            {
                Movies = new List<Movie>();
                List<HtmlNode> movieNodes = LoadNodes();
                foreach (var metaData in movieNodes)
                {
                    Movies.Add(LoadMovie(metaData));
                }
            }
            else
            {
                throw new Exception("Website isn't loaded!");
            }

        }

        private List<HtmlNode> LoadNodes()
        {
            List<HtmlNode> movieNodes = website.DocumentNode.Descendants().Where
                    (x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("card card-entity card-entity-list cf hred"))).ToList();
            return movieNodes;
        }

        private Movie LoadMovie(HtmlNode metaData)
        {
            Movie movie = new Movie();

            HtmlNode meta       = metaData.Descendants().Where(x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("meta"))).ElementAt<HtmlNode>(0);
            HtmlNode metaTitle  = meta.Descendants().Where(x => (x.Name == "h2")).ElementAt<HtmlNode>(0);
            HtmlNode titleNode  = metaData.Descendants().Where(x => (x.Name == "a")).ElementAt<HtmlNode>(0);
            String title = titleNode.InnerText;

            movie.Title = title;
            return movie;
        }
    }
}