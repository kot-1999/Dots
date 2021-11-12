namespace dots
{
    public abstract class Game
    {
        public static GameState gameState = GameState.PLAYING;
        public abstract void Play();
    }
}