using dots.observer;

namespace dots
{
    
    public interface  Dot :Subscriber
    {
        public Color GetColor();
        public void SetColor(Color color);
        public void SetRandomColor();
        public void SetMark(Mark m);
        public Mark GetMark();


    }
    
}