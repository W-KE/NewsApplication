using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsApplication.Models;
using NewsApplication.Repository;
using HtmlAgilityPack;
using System.Xml;
using System.Text;
using AngleSharp.Html.Parser;
using System.Net;
using System.IO;
using AngleSharp.Dom;
using AngleSharp;

namespace NewsApplication.Controllers
{
    [ApiController]
    public class NewsController : ControllerBase
    {
        [Route("api/newslist")]
        public NewsModel[] GetList()
        {
            return NewsRepository.GetNewsList().ToArray();
        }

        [Route("api/news")]
        public String[] GetNews(string url, string objectid)
        {
            List<String> data = new List<String>();
            try
            {
                string source = url + "&objectid=" + objectid;
                HtmlParser htmlParser = new HtmlParser();
                WebRequest wRequest = WebRequest.Create(source);
                wRequest.Method = "get";
                wRequest.UseDefaultCredentials = true;
                var task = wRequest.GetResponseAsync();
                WebResponse wResp = task.Result;
                Stream respStream = wResp.GetResponseStream();
                StreamReader reader = new StreamReader(respStream);
                string htmlDoc = reader.ReadToEnd();
                reader.Close();
                var document = htmlParser.ParseDocument(htmlDoc);
                data.Add(document.QuerySelector("h1").Text());
                var images = document.QuerySelectorAll("div.header-image-wrapper");
                foreach (var items in images)
                {
                    var image = items.QuerySelector("img");
                    var dataSrcset = image.GetAttribute("data-srcset");
                    string[] srcset = dataSrcset.Split("0w,//");
                    string src = "https://" + srcset[srcset.Length - 1].Split()[0].Split("/filters:quality(70)/")[1];
                    var figcaption = document.QuerySelector("header").QuerySelector("figcaption").Text();
                    data.Add(src + "|HEAD|" + figcaption);
                }
                var content = document.QuerySelector("div#article-content");
                var paragraphs = content.Children;
                foreach (var p in paragraphs)
                {
                    if (p.ClassList.Contains("element-paragraph"))
                    {
                        data.Add(p.ToHtml());
                    }
                    if (p.ClassList.Contains("element-image"))
                    {
                        var image = p.QuerySelector("img");
                        var dataSrcset = image.GetAttribute("data-srcset");
                        string[] srcset = dataSrcset.Split("0w,//");
                        string src = "https://" + srcset[srcset.Length - 1].Split()[0].Split("/filters:quality(70)/")[1];
                        var figcaption = p.QuerySelector("figcaption.closed").QuerySelector("span").Text();
                        data.Add(src + "|IMG|" + figcaption);
                    }
                }
            }
            catch
            {
                data = new List<String>() { "Error 404" };
            }
            return data.ToArray();
        }
    }
}