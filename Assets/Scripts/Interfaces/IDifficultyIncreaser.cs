namespace Assets.Scripts.Interfaces
{
    public interface IDifficultyIncreaser
    {
        float DifficultyModifier { get; }
        void IncreaseDifficulty();
    }
}