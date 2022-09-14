using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Spawners
{ 
    public abstract class Spawner : MonoBehaviour, IPauseHandler, IDifficultyIncreaser
    {
        [SerializeField] private List<GameObject> _prefabs;

        protected PauseManager PauseManager => GameController.Instance.PauseManager;

        private void Start()
        {
            PauseManager.Register(this);
        }

        public void SetPaused(bool isPaused)
        {
            if (!isPaused)
                StartCoroutine(Spawn());
        }

        public void IncreaseDifficulty()
        {
            Cooldown -= DifficultyModifier;
        }

        protected GameObject GetRandomPrefab()
        {
            var index = Random.Range(0, _prefabs.Count);
            return _prefabs[index];
        }

        protected Vector2 GetSpawnVector(SpawnPosition position)
        {
            return position switch
            {
                SpawnPosition.TopLeft => new Vector2(-15, 7),
                SpawnPosition.TopCenter => new Vector2(0, 7),
                SpawnPosition.TopRight => new Vector2(15, 7),
                SpawnPosition.MiddleLeft => new Vector2(-15, 0),
                SpawnPosition.MiddleRight => new Vector2(15, 0),
                SpawnPosition.BottomLeft => new Vector2(-15, -7),
                SpawnPosition.BottomCenter => new Vector2(0, -7),
                SpawnPosition.BottomRight => new Vector2(15, -7),
                _ => throw new UnityException("Incorrect spawn position")
            };
        }

        protected abstract float Cooldown { get; set; }
        public abstract float DifficultyModifier { get; }
        protected abstract IEnumerator Spawn();
        protected abstract SpawnPosition GetSpawnPosition();
    }

    public enum SpawnPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }
}