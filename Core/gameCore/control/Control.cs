namespace dots
{
    public interface Control
    {
        public void Move(Key move);
        int X { get; }
        int Y { get; }
        void SetTable(DotTable dotTable);
        void Move(int y, int x);
    }
}