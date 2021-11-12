using System;
using dots.observer;

namespace dots
{
    [Serializable]
    public class ObserverControl : Control
    {
        private DotTable dotTable;
        private int currY, currX ;
        private Observer observer;
        private int marked;
        public ObserverControl(DotTable dt)
        {
            dotTable = dt;
            currX = currY = 0;
            dt[currY,currX].SetMark(Mark.MARKED);
            observer = new SimplyObserver();
            observer.Subscribe(dt[currY,currX]);
            marked = 1;

        }

        public void SetTable(DotTable dotTable)
        {
            if (dotTable != null)
                this.dotTable = dotTable;
        }
        public void Move(Key move)
        {
          
            int y = currY;
            int x = currX;
            
            switch (move)
            {
                case Key.UP:
                    y--;
                    break;
                case Key.LEFT:
                    x--;
                    break;
                case Key.DOWN:
                    y++;
                    break;
                case Key.RIGHT:
                    x++;
                    break;
                case Key.ENTER:
                    Refresh();
                    return;
            }
            if(x<0 || y<0 || x==dotTable.Width || y==dotTable.Height)
                return;
            if(dotTable[currY,currX].GetColor()!=dotTable[y,x].GetColor())
            {
                marked = 0;
                observer.NotifyAll();
                observer.DeleteAll();
            }
            
            dotTable[y, x].SetMark(Mark.MARKED);
            currY = y;
            currX = x;
            observer.Subscribe(dotTable[currY,currX]);
            marked++;
        }

        public int X => currX;
        public int Y => currY;

        private void Refresh(){
            // Console.WriteLine("marked "+marked);
            if(marked<2)
                return;
            
            dotTable.ReplaceMarked();
            observer.Subscribe(dotTable[currY,currX]);
            dotTable[currY,currX].SetMark(Mark.MARKED);
            marked = 1;
        }
        public void Move(int y, int x)
        {
            for (int yy = 0; yy < dotTable.Height; yy++)
                for (int xx = 0; xx < dotTable.Width; xx++)
                    dotTable[yy, xx].SetMark(Mark.UNMARKED);
       

                if (y == currY+1 && x==currX)
               {
                   Move(Key.DOWN);
               }else if (y == currY-1 && x == currX)
               {
                   Move(Key.UP);
               }else if (x == currX+1 && y==currY)
               {
                   Move(Key.RIGHT);
               }else if (x== currX-1 && y == currY)
               {
                   Move(Key.LEFT);
               }
               else
               {
                   currX = x;
                   currY = y;
                   observer.NotifyAll();
                   observer.DeleteAll();
                   observer.Subscribe(dotTable[currY, currX]);
                   dotTable[currY, currX].SetMark(Mark.MARKED);
                   marked = 1;
               }
        }
    }

}