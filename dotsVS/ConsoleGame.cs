
using System;
using System.Collections.Generic;
using dots.comments;
using dots.forms;
using dots.services;
using dots.tableBuilder;
namespace dots.konsoleGame
{
    public class ConsoleGame : Game
    {
        private DotTable dt;
        private TableBuilder tb;
        private Control ctrl;
        private Player player;
        private EntityScoreService infoService;
        private EntityCommentsService commentService;
        
        public ConsoleGame()
        {
            dt = DotTable.Create(5,5);
            tb = new CommonTableBuilder();
            player = new Player();

            dt.player = player;
            tb.SetTable(dt);
            tb.BuildTable();
            infoService = new EntityScoreService();
            commentService = new EntityCommentsService();

        }
        
        
        
        public override void Play()
        {

            /*Console.WriteLine("STARTED");
            EntityInfoService entityInfoService

            InfoForm f1 = new InfoForm("sawa", "sawa@gmail.com", 23);

            try{
                entityInfoService.Add(new InfoForm("alex", "alex@gmail.com", 123));
                entityInfoService.Add(new InfoForm("peter", "peter@gmail.com", 65));
            }
            catch(PlayerAutorizationExeption e)
            {
                Console.WriteLine("player exists");
            }
            Console.WriteLine(entityInfoService.Get(f1));
*/

            //COMMENTS
            /*EntityCommentsService entityCommentsService = new EntityCommentsService();

           
            *//*entityCommentsService.Add(new CommentForm { Player ="sawa",Comment="not bad"}) ;
            entityCommentsService.Add(new CommentForm { Player = "alex", Comment = "sheet" });
            entityCommentsService.Add(new CommentForm { Player = "andy", Comment = "finnaly" });*//*

            List<Form> list = entityCommentsService.GetList();
            
            Form f;

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }
            f = list[1];
            Console.WriteLine(entityCommentsService.Get(f));
            CommentForm cf = (CommentForm)f;
            cf.Comment = "my new cooment";
            entityCommentsService.Update(cf);
            Console.WriteLine(entityCommentsService.Get(cf));*/





            autorizePlayer();
            
            
            
            Console.WriteLine("email: " + player.Email);
            ctrl = new ObserverControl(dt);
            //
            while (gameState != GameState.EXIT)
            {
                Console.Out.WriteLine("Press <f> to start game | <r> to see raiting | <c> comments overviev | <q> to exit");
                Key k = InputReader.GetKey();
                switch (k)
                {
                    case Key.ENTER:
                        PlayGame();
                        break;
                    case Key.CONTROL:
                        CommentsOverviev();
                        break;
                    case Key.RAITING:
                        ShowRaiting();
                        break;
                    case Key.EXIT:
                        gameState = GameState.EXIT;
                        break;
                }
            }
        


        }

        private void autorizePlayer()
        {
            player.Name="sawa";
            player.Email="sawa@gmail.com";
            ScoreEntity info = new ScoreEntity(player.Name, 0);
            try
            {
                infoService.Add(info);
            }
            catch (PlayerAutorizationExeption e)
            {
                Console.WriteLine("Wrong name or email");
                autorizePlayer();
            }
            info = (ScoreEntity)infoService.Get(info);
            player.Highest = info.HighestScore;
        }

        private void ShowRaiting()
        {
            List<ScoreEntity> storage = infoService.GetList(10);
           
            int i = 1;
            foreach (ScoreEntity s in storage)
            {
                Console.WriteLine(i+". "+s.Player+" "+s.HighestScore);
                i++;
              
            }
        }
        private void CommentsOverviev()
        {
            List<CommentEntity> storage= commentService.GetList();
            
            foreach (CommentEntity s in storage)
            {
                Console.WriteLine(s.Player+"\n\t"+s.Comment+" | Raited: "+s.Raiting+"/10;"+'\n');
            }
        
            
            gameState = GameState.PLAYING;
            while (gameState == GameState.PLAYING)
            {
                Console.Out.WriteLine("Press <f> to write a comment | <c> to exit main menu | <q> to exit");
                Key k = InputReader.GetKey();
                switch (k)
                {
                    case Key.ENTER:
                        Console.Write("Please enter your comment: ");
                        string str = Console.ReadLine();
                        
                        int rate=0;
                        do
                        {
                            Console.Write("Please rate the game from 1 to 10: ");
                            string s=Console.ReadLine();
                            if (int.TryParse(s, out int n))
                                rate = int.Parse(s);
                            } while (!(rate > 0 && rate < 11));

                
                        commentService.Add(new CommentEntity { Player = player.Name, Comment = str,Raiting=rate });
                        break;
                    case Key.CONTROL:
                        gameState = GameState.END;
                        break;
                    case Key.EXIT:
                        gameState = GameState.EXIT;
                        break;
                }
            }
        }

        private void PlayGame()
        {
            gameState = GameState.PLAYING;
            PrintGame();
            while (gameState==GameState.PLAYING)
            {
                Key k = InputReader.GetKey();
                switch (k)
                {
                    case Key.UP:
                        ctrl.Move(k);
                        break;
                    case Key.LEFT:
                        ctrl.Move(k);
                        break;
                    case Key.DOWN:
                        ctrl.Move(k);
                        break;
                    case Key.RIGHT:
                        ctrl.Move(k);
                        break;
                    case Key.ENTER:
                        ctrl.Move(k);
                        break;
                    case Key.CONTROL:
                        gameState = GameState.END;
                        break;
                    case Key.EXIT:
                        gameState = GameState.EXIT;
                        break;
                    default:
                        continue;
                }
                
                // if (player.Score >= 100)
                //     gameState = GameState.WIN;
                PrintGame();
                if (dt.hasStep() == false)
                    gameState = GameState.LOOSE;
            }

            if (gameState == GameState.LOOSE)
            {
                Console.WriteLine("Game ended\nYour core is: "+player.Score);
                dt = DotTable.Create(5,5);
                dt.player = player;
                tb.SetTable(dt);
                tb.BuildTable();
                ctrl.SetTable(dt);
            }

            
            infoService.Update(new ScoreEntity(player.Name,player.Score));
            if (player.Highest<player.Score)
                player.Highest = player.Score;
            player.Score = 0;
            dt.refresh();
        }

      

        private void PrintGame()
        {
            Console.WriteLine("Score : "+player.Score +" Highest score: "+player.Highest);
            for(int y=0;y<dt.Height;y++)
            {
                for (int x = 0; x < dt.Width; x++)
                {
                    Dot d = dt[y, x];
                    switch (d.GetColor())
                    {
                        case Color.RED:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("   ");
                            Console.ResetColor();
                            break;
                        case Color.BLUE:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("   ");
                            Console.ResetColor();
                            break;
                        case Color.VIOLET:
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.Write("   ");
                            Console.ResetColor();
                            break;
                        case Color.YELLOW:
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.Write("   ");
                            Console.ResetColor();
                            break;
                        case Color.GREEN:
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write("   ");
                            Console.ResetColor();
                            break;
                    }
                }
                Console.WriteLine();
                for (int x = 0; x < dt.Width; x++)
                {
                    Dot d = dt[y, x];
                    switch (d.GetColor())
                    {
                        case Color.RED:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("   ");
                            Console.ResetColor();
                            break;
                        case Color.BLUE:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write("   ");
                            Console.ResetColor();
                            break;
                        case Color.VIOLET:
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.Write("   ");
                            Console.ResetColor();
                            break;
                        case Color.YELLOW:
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.Write("   ");
                            Console.ResetColor();
                            break;
                        case Color.GREEN:
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write("   ");
                            Console.ResetColor();
                            break;
                    }
                }
                Console.WriteLine();
                
                for (int x = 0; x < dt.Width; x++)
                    if (x == ctrl.X && y == ctrl.Y)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("^^^");
                        Console.ResetColor();
                    }else if(dt[y,x].GetMark()==Mark.MARKED)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write("xxx");
                        Console.ResetColor();
                    }else
                        Console.Write("   ");
                Console.WriteLine();
            }
            Console.WriteLine("Press: | <w> <a> <s> <d> to play | <f> enter | <q> exit | <c> exit to main menu");
        
        }
    }
}