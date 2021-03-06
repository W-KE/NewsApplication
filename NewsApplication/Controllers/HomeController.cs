﻿using System;
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
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(NewsRepository.GetNewsList());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult News(string source)
        {
            try
            {
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
                ViewData["H1"] = document.QuerySelector("h1").Text();
                var images = document.QuerySelectorAll("div.header-image-wrapper");
                foreach (var items in images)
                {
                    var image = items.QuerySelector("img");
                    var dataSrcset = image.GetAttribute("data-srcset");
                    string[] srcset = dataSrcset.Split("0w,//");
                    string src = "https://" + srcset[srcset.Length - 1].Split()[0].Split("/filters:quality(70)/")[1];
                    var figcaption = document.QuerySelector("header").QuerySelector("figcaption").Text();
                    ViewData["SRC"] = "<figure class=\"figure w-100\">";
                    ViewData["SRC"] += "<img src=\"" + src + "\" alt=\"" + figcaption + "\" class=\"figure-img img-fluid rounded w-100\" />";
                    ViewData["SRC"] += "<figcaption class=\"figure-caption\">" + figcaption + "</figcaption>";
                    ViewData["SRC"] += "</figure>";
                }
                ViewData["BODY"] = "";
                var content = document.QuerySelector("div#article-content");
                var paragraphs = content.Children;
                foreach (var p in paragraphs)
                {
                    if (p.ClassList.Contains("element-paragraph"))
                    {
                        p.ClassList.Add("my-3");
                        ViewData["BODY"] += p.ToHtml();
                    }
                    if (p.ClassList.Contains("element-image"))
                    {
                        var image = p.QuerySelector("img");
                        var dataSrcset = image.GetAttribute("data-srcset");
                        string[] srcset = dataSrcset.Split("0w,//");
                        string src = "https://" + srcset[srcset.Length - 1].Split()[0].Split("/filters:quality(70)/")[1];
                        var figcaption = p.QuerySelector("figcaption.closed").QuerySelector("span").Text();
                        ViewData["BODY"] += "<figure class=\"figure w-100\">";
                        ViewData["BODY"] += "<img src=\"" + src + "\" alt=\"" + figcaption + "\" class=\"figure-img img-fluid rounded w-100\" />";
                        ViewData["BODY"] += "<figcaption class=\"figure-caption\">" + figcaption + "</figcaption>";
                        ViewData["BODY"] += "</figure>";
                    }
                }
                return View();
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
