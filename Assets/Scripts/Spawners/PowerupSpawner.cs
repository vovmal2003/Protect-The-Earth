using System.Collections;
using UnityEngine;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Spawners
{
    public sealed class PowerupSpawner : Spawner
    {
        private const float COOLDOWN_MIN_BOUND = 10f;
        private float _cooldown = 15f;

        protected override float Cooldown
        {
            get => _cooldown;
            set
            {
                if (value > COOLDOWN_MIN_BOUND)
                    _cooldown = value;
            }
        }
        public override float DifficultyModifier => 0.5f;

        protected override IEnumerator Spawn()
        {
            while (!PauseManager.IsPaused)
            {
                yield return new WaitForSeconds(_cooldown);
                Instantiate(GetRandomPrefab(), GetSpawnVector(GetSpawnPosition()), Quaternion.identity);
                IncreaseDifficulty();
            }
        }

        protected override SpawnPosition GetSpawnPosition()
        {
            var positionNumber = Random.Range(0, 4);
            return positionNumber switch
            {
                0 => SpawnPosition.TopCenter,
                1 => SpawnPosition.MiddleLeft,
                2 => SpawnPosition.MiddleRight,
                3 => SpawnPosition.BottomCenter,
                _ => throw new UnityException("Incorrect position number")
            };
        }
    }
}