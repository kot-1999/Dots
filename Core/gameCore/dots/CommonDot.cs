using System;

namespace dots
{
    [Serializable]
    public class CommonDot : Dot
    {
        private Color color { get; set; } = Color.NONE;
        private Mark mark = Mark.UNMARKED;
        private CommonDot()
        {
            
        }
    

        public static CommonDot Create()
        {
            CommonDot result=new CommonDot();
            result.SetRandomColor();
            return result;
        }

        public void SetRandomColor()
        {
            Random rnd = new Random();
            int num = rnd.Next(5);
            
            switch (num)
            {
                case 0:
                    color = Color.RED;
                    break;
                case 1:
                    color = Color.YELLOW;
                    break;
                case 2:
                    color = Color.GREEN;
                    break;
                case 3:
                    color = Color.BLUE;
                    break;
                case 4:
                    color = Color.VIOLET;
                    break;
            }
        }

        public void SetMark(Mark m)
        {
            mark = m;
        }

        public Mark GetMark()
        {
            return mark;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }
        
        public Color GetColor()
        {
            return color;
        }


        public void notify()
        {
            if (mark == Mark.MARKED)
                mark = Mark.UNMARKED;
        }
    }
}