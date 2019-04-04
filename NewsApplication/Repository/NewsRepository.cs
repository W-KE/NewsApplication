using System.Collections.Generic;
using NewsApplication.Models;
using System.Xml;
using System.Net.Http;
using System.Globalization;
using System;

namespace NewsApplication.Repository
{
    public class NewsRepository
    {
        public static List<NewsModel> GetNewsList()
        {
            string url = "http://rss.nzherald.co.nz/rss/xml/nzhtsrsscid_000000698.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(url);
            XmlNodeList news_nodes = doc.GetElementsByTagName("item");

            List<NewsModel> news_list = new List<NewsModel>();
            string source = doc.GetElementsByTagName("title")[0].InnerText;

            //go over each news
            foreach (XmlNode item in news_nodes)
            {
                //add a new product with the content of every news inner text
                news_list.Add(new NewsModel()
                {
                    NewsId = item.SelectSingleNode("link").InnerText,
                    NewsTitle = item.SelectSingleNode("title").InnerText,
                    NewsDescription = item.SelectSingleNode("description").InnerText,
                    NewsDate = GetTime(item.SelectSingleNode("pubDate").InnerText),
                    NewsLink = item.SelectSingleNode("link").InnerText,
                    NewsSource = source,
                    NewsAuthor = item.SelectSingleNode("author").InnerText,
                });
            }
            return news_list;
        }

        public static DateTime GetTime(string time)
        {
            DateTimeOffset result = DateTimeOffset.Parse(time, CultureInfo.InvariantCulture);
            return result.DateTime;
        }
    }
}
