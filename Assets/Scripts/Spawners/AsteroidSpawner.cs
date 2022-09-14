using System.Collections;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Spawners
{
    public sealed class AsteroidSpawner : Spawner
    {
        private const float COOLDOWN_MIN_BOUND = 1f;
        private float _cooldown = 3f;

        protected override float Cooldown
        {
            get => _cooldown;
            set
            {
                if (value > COOLDOWN_MIN_BOUND)
                    _cooldown = value;
            }
        }
        public override float DifficultyModifier => 0.05f;

        protected override IEnumerator Spawn()
        {
            while (!PauseManager.IsPaused)
            {
                Instantiate(GetRandomPrefab(), GetSpawnVector(GetSpawnPosition()), Quaternion.identity);
                IncreaseDifficulty();
                yield return new WaitForSeconds(_cooldown);
            }
        }

        protected override SpawnPosition GetSpawnPosition()
        {
            var positionNumber = Random.Range(0, 4);
            return positionNumber switch
            {
                0 => SpawnPosition.TopLeft,
                1 => SpawnPosition.TopRight,
                2 => SpawnPosition.BottomLeft,
                3 => SpawnPosition.BottomRight,
                _ => throw new UnityException("Incorrect position number")
            };
        }
    }
}