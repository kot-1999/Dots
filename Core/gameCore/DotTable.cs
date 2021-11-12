



using System;

namespace dots
{   
    [Serializable]
    public class DotTable
    {
        private Dot[,] table;
        private int width;
        private int height;
        private int replaced;

        public Player player { get; set; }

        public int Height => height;

      

        public int Width => width;
        
        private DotTable(int y, int x)
        {
            replaced = 0;
            height = y;
            width = x;
            table = new Dot[y, x];
        }
        
        public static DotTable Create()
        {
            return  new DotTable(5, 5);
        }
        public static DotTable Create(int y)
        {
            DotTable dt = new DotTable(y, y);
            return dt;
        }
        
        public static DotTable Create(int y,int x)
        {
            return new DotTable(y, x);
        }

        public void refresh()
        {
            replaced = 0;
        }
   

        public Dot this[int y, int x]
        {
            get => table[y,x];
            set => table[y, x] = value;
        }

        public void ReplaceMarked()
        {
            for(int y=0;y<height;y++)
                for(int x=0;x<width;x++)
                    if (table[y, x] != null && table[y, x].GetMark() == Mark.MARKED)
                    {
                        table[y, x] = null;
                        replaced++;
                        player.Score = replaced;
                    }
            for(int y=height-1;y>=0;y--)
                for (int x = 0; x < width; x++)
                {
                    if (table[y, x] == null)
                        for (int k = y - 1; k >= 0; k--)
                            if (table[k, x] != null)
                            {
                                table[y, x] = table[k, x];
                                table[k, x] = null;
                                break;
                            }

                    if (table[y, x] == null)
                        table[y, x] = CommonDot.Create();
         
                    
                    
                }
        }

        public Boolean hasStep()
        {
            for(int y=0;y<height;y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x!=width-1 && table[y, x].GetColor() == table[y, x + 1].GetColor() )
                        return true;
                    if (y!=height-1 && table[y, x].GetColor() == table[y+1, x].GetColor() )
                        return true;
                }
            }
            return false; 
        }

        
    }
}