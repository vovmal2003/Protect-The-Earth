using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Assets.Scripts.Enemy;
using Assets.Scripts.Managers;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    { 
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private EffectsManager _effectsManager;
        [SerializeField] private DataManager _dataManager;

        private int _score;

        private int HighScore
        {
            get => _dataManager.GameData.HighScore;
            set
            {
                if (value < 0)
                    throw new UnityException("Invalid argument");
                _dataManager.GameData.HighScore = value;
            }
        }
        public static GameController Instance { get; private set; }
        public PauseManager PauseManager { get; private set; }
        public AudioManager AudioManager => _audioManager;

        private void Awake()
        {
            Instance = this;
            PauseManager = new();
        }

        private void Start()
        {
            StartCoroutine(StartGame());
        }

        public void GameOver()
        {
            StartCoroutine(EndGame());
        }

        public void AddScore(int receivedScore)
        {
            if (receivedScore < 0)
                throw new UnityException("Invalid argument");
            _score += receivedScore;
            _uiManager.UpdateScoreText(_score);
        }

        private IEnumerator StartGame()
        {
            PauseManager.SetPaused(true);
            _dataManager.LoadData();
            _uiManager.UpdateHighScoreText(HighScore);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            PauseManager.SetPaused(false);
        }

        private IEnumerator EndGame()
        {
            CheckHighScore();
            DestroyObjectsWithTags(new[] { "Spawner", "Player", "Enemy", "Powerup" });
            _audioManager.StopMainTheme();
            _effectsManager.MakeExplosion();
            _audioManager.PlayPlanetExplosionSound();
            yield return new WaitForSeconds(_effectsManager.ExplosionLifetime);
            _audioManager.StopPlanetExplosionSound();
            SceneManager.LoadScene(0);
            Asteroid.ResetSpeed();
            Start();
        }

        private void DestroyObjectsWithTags(string[] tags)
        {
            foreach (var tag in tags)
            {
                var gameObjects = GameObject.FindGameObjectsWithTag(tag);
                foreach (var gameObject in gameObjects)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void CheckHighScore()
        {
            if (_score > HighScore)
            {
                HighScore = _score;
                _dataManager.SaveData();
            }
        }
    }
}