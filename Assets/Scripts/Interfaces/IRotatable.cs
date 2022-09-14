namespace Assets.Scripts.Interfaces
{
    public interface IRotatable
    {
        float RotationSpeed { get; set; }
        void Rotate();
    }
}