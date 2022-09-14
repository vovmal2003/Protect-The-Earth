namespace Assets.Scripts.Interfaces
{
    public interface IMovable
    {
        float MovementSpeed { get; set; }
        void Move();
    }
}