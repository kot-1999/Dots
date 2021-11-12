using dots;
using dots.services;
using dots.tableBuilder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using dots.forms;

namespace WebApplication.Controllers
{
    public class DotsController : Controller
    {




        private EntityInfoService infoService = new EntityInfoService();
        private EntityCommentsService commentService=new EntityCommentsService();

        const string TableSessionKey= "table";
        const string TableControlSessionKey = "control";
        const string PlayerSessionKey = "player";

        public IActionResult Index()
        {
            TableBuilder tableBuilder = new CommonTableBuilder();
            DotTable dotTable = DotTable.Create(5, 5);
            tableBuilder.SetTable(dotTable);
            tableBuilder.BuildTable();

            Control control = new SimplyControl(dotTable);
            control.SetTable(dotTable);
            Player player;
            try
            {
                player  = (Player)HttpContext.Session.GetObject(PlayerSessionKey);
                player.Highest = infoService.Get(new InfoForm(player.Name, null, 0)).HighestScore;
                player.Score = 0;
                
            }
            catch(Exception e)
            {
                player = new Player();
                player.Name = "Player";
               
            }
            dotTable.refresh();
            dotTable.player = player;
            HttpContext.Session.SetObject(TableSessionKey, dotTable);
            HttpContext.Session.SetObject(TableControlSessionKey, control);
            HttpContext.Session.SetObject(PlayerSessionKey, player);
            return View(new DotsModel { infos = infoService.GetList(), dotTable = dotTable,comments=commentService.GetList() });
        }

        public IActionResult Select(int y,int x)
        {
            Control control = (Control)HttpContext.Session.GetObject(TableControlSessionKey);
            DotTable dotTable = (DotTable)HttpContext.Session.GetObject(TableSessionKey);
            Player player = (Player)HttpContext.Session.GetObject(PlayerSessionKey);
            dotTable.player = player;

            control.SetTable(dotTable);
            control.Move(y, x);
        
            HttpContext.Session.SetObject(PlayerSessionKey, player);
            HttpContext.Session.SetObject(TableSessionKey, dotTable);
            HttpContext.Session.SetObject(TableControlSessionKey, control);
            return View("Index", new DotsModel { infos = infoService.GetList(), dotTable = dotTable, comments = commentService.GetList() });
        }

        public IActionResult Collect()
        {
            Control control = (Control)HttpContext.Session.GetObject(TableControlSessionKey);
            DotTable dotTable = (DotTable)HttpContext.Session.GetObject(TableSessionKey);
            Player player = (Player)HttpContext.Session.GetObject(PlayerSessionKey);
            dotTable.player = player;

            control.SetTable(dotTable);
            control.Move(Key.ENTER);

            HttpContext.Session.SetObject(PlayerSessionKey, player);
            HttpContext.Session.SetObject(TableSessionKey, dotTable);
            HttpContext.Session.SetObject(TableControlSessionKey, control);
            if (dotTable.player.Name != "Player" && player.Score>player.Highest)
                infoService.UpdateScore(new InfoForm(player.Name, player.Email, dotTable.player.Score));

            return View("Index", new DotsModel { infos = infoService.GetList(), dotTable = dotTable, comments = commentService.GetList() });
        }

        public IActionResult login(string name,string password)
        {
            DotTable dotTable = (DotTable)HttpContext.Session.GetObject(TableSessionKey);
            Player player = (Player)HttpContext.Session.GetObject(PlayerSessionKey);
            

            try
            {
                if(name!=null && password!=null)
                infoService.Autorize(new InfoForm(name, password, 0));
                player.Highest = infoService.Get(new InfoForm(name, null, 0)).HighestScore;
                player.Score = 0;
                dotTable.refresh();
                player.Name = name;
                player.Email = password;
            }
            catch(PlayerAutorizationExeption e)
            {
                Console.WriteLine(e.Message);
                return View("Index", new DotsModel { infos = infoService.GetList(), dotTable = dotTable, comments = commentService.GetList() });

            }
            dotTable.player = player;

            HttpContext.Session.SetObject(PlayerSessionKey, player);
            HttpContext.Session.SetObject(TableSessionKey, dotTable);
       

            return View("Index", new DotsModel { infos = infoService.GetList(), dotTable = dotTable, comments = commentService.GetList() });
        }

        public IActionResult logout()
        {
            DotTable dotTable = (DotTable)HttpContext.Session.GetObject(TableSessionKey);
            Player player = (Player)HttpContext.Session.GetObject(PlayerSessionKey);


            player.Highest = 0;
            player.Score = 0;
            dotTable.refresh();
            player.Name = "Player";
            player.Email = "";

            dotTable.player = player;

            HttpContext.Session.SetObject(PlayerSessionKey, player);
            HttpContext.Session.SetObject(TableSessionKey, dotTable);


            return View("Index", new DotsModel { infos = infoService.GetList(), dotTable = dotTable, comments = commentService.GetList() });
        }

        public IActionResult addComment(string comment,int rate)
        {
            DotTable dotTable = (DotTable)HttpContext.Session.GetObject(TableSessionKey);
            commentService.Add(new CommentForm(dotTable.player.Name, comment, rate));



            return View("Index", new DotsModel { infos = infoService.GetList(), dotTable = dotTable, comments = commentService.GetList() });

        }
    }
}
