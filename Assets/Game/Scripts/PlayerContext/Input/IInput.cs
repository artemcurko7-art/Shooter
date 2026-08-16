namespace Game.Scripts.PlayerContext.Input
{
    public interface IInput 
    {
        float Horiontal { get; }
        float Vertical { get; }   
        void Update();
    }
}