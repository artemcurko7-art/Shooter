namespace Game.Scripts.PlayerContext.GameInput
{
    public interface IInput 
    {
        float Horizontal { get; }
        float Vertical { get; }   
        void Update();
    }
}