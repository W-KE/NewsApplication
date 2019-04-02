using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsApplication.Models;
using System.Web;

namespace NewsApplication.Repository
{
    public class NewsRepository
    {
        public static List<NewsModel> GetProductList()
        {
            HtmlWeb website = new HtmlWeb();
            List<NewsModel> prod_list = new List<NewsModel>();

            prod_list.Add(new NewsModel()
            {
                NewsId = "www.stuff.co.nz/111746865",
                NewsTitle = "'We're in crazytown now': Spectacular fall of WND, the king of conspiracy sites",
                NewsDescription = "US website founder built an empire stoking \"birther\" theories, but now it's imploding.",
                NewsDate = "Tue, 02 Apr 2019 18:51:51 GMT",
                NewsLink = "https://www.stuff.co.nz/business/world/111746865/were-in-crazytown-now-spectacular-fall-of-wnd-the-king-of-rightwing-conspiracy-sites.html",
                NewsSource = "Stuff.co.nz",
                NewsAuthor = "MANUEL ROIG-FRANZIA"
            });

            prod_list.Add(new NewsModel()
            {
                NewsId = "www.stuff.co.nz/111746865",
                NewsTitle = "'We're in crazytown now': Spectacular fall of WND, the king of conspiracy sites",
                NewsDescription = "US website founder built an empire stoking \"birther\" theories, but now it's imploding.",
                NewsDate = "Tue, 02 Apr 2019 18:51:51 GMT",
                NewsLink = "https://www.stuff.co.nz/business/world/111746865/were-in-crazytown-now-spectacular-fall-of-wnd-the-king-of-rightwing-conspiracy-sites.html",
                NewsSource = "Stuff.co.nz",
                NewsAuthor = "MANUEL ROIG-FRANZIA"
            });

            prod_list.Add(new NewsModel()
            {
                NewsId = "www.stuff.co.nz/111746865",
                NewsTitle = "'We're in crazytown now': Spectacular fall of WND, the king of conspiracy sites",
                NewsDescription = "US website founder built an empire stoking \"birther\" theories, but now it's imploding.",
                NewsDate = "Tue, 02 Apr 2019 18:51:51 GMT",
                NewsLink = "https://www.stuff.co.nz/business/world/111746865/were-in-crazytown-now-spectacular-fall-of-wnd-the-king-of-rightwing-conspiracy-sites.html",
                NewsSource = "Stuff.co.nz",
                NewsAuthor = "MANUEL ROIG-FRANZIA"
            });

            return prod_list;
        }
    }
}
