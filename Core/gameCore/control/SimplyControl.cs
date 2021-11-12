using System;

namespace dots
{
    [Serializable]
    public class SimplyControl : Control
    {
        private DotTable dotTable;
        private int currY, currX;

        private int marked;
        public SimplyControl(DotTable dt)
        {
            dotTable = dt;
            currX = currY = 0;
            dt[currY, currX].SetMark(Mark.MARKED);
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
            if (x < 0 || y < 0 || x == dotTable.Width || y == dotTable.Height)
                return;
            if (dotTable[currY, currX].GetColor() != dotTable[y, x].GetColor())
            {
                marked = 0;
                Unmark();
            }

            dotTable[y, x].SetMark(Mark.MARKED);
            currY = y;
            currX = x;
            marked++;
        }

        public int X => currX;
        public int Y => currY;

        private void Refresh()
        {
            if (marked < 2)
                return;

            dotTable.ReplaceMarked();
            
            dotTable[currY, currX].SetMark(Mark.MARKED);
            marked = 1;
        }
        public void Move(int y, int x)
        {
            if (dotTable[y, x].GetMark() == Mark.MARKED)
                return;
            
            if (y == currY + 1 && x == currX)
            {
                Move(Key.DOWN);
            }
            else if (y == currY - 1 && x == currX)
            {
                Move(Key.UP);
            }
            else if (x == currX + 1 && y == currY)
            {
                Move(Key.RIGHT);
            }
            else if (x == currX - 1 && y == currY)
            {
                Move(Key.LEFT);
            }
            else
            {
                currX = x;
                currY = y;
                Unmark();
                dotTable[currY, currX].SetMark(Mark.MARKED);
                marked = 1;
            }

        }
        private void Unmark() 
        {
            for (int y = 0; y < dotTable.Height; y++)
                for (int x = 0; x < dotTable.Width; x++)
                    dotTable[y, x].SetMark(Mark.UNMARKED);
        }
    }
}
