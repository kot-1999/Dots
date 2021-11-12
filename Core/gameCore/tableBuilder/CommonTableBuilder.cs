namespace dots.tableBuilder
{
    public class CommonTableBuilder : TableBuilder
    {
        private DotTable dt=null;
        
        public void SetTable(DotTable dt)
        {
            this.dt = dt;
        }

        public void BuildTable()
        {
            if(dt==null)
                return;
            for(int y=dt.Height-1;y>=0;y--)
                for(int x=dt.Width-1;x>=0;x--)
                    dt[y,x] =CommonDot.Create();
        }
    }
}