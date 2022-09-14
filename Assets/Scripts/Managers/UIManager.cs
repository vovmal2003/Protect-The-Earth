using UnityEngine;
using TMPro;
using Assets.Scripts.Interfaces;
using UnityEngine.SocialPlatforms.Impl;

namespace Assets.Scripts.Managers
{
    public class UIManager : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private TextMeshProUGUI _startGameText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _highScoreText;

        private PauseManager PauseManager => GameController.Instance.PauseManager;

        private void Start()
        {
            PauseManager.Register(this);
        }

        public void SetPaused(bool isPaused)
        {
            if (isPaused)
                ShowPauseScreen();
            else
                ShowGameScreen();
        }

        public void UpdateScoreText(int score)
        {
            _scoreText.text = "Score: " + score;
        }

        public void UpdateHighScoreText(int highScore)
        {
            if (highScore < 0)
                throw new UnityException("Invalid argument");
            _highScoreText.text = "High Score: " + highScore;
        }

        private void ShowGameScreen()
        {
            _startGameText.enabled = false;
            _highScoreText.enabled = false;
            _scoreText.enabled = true;
        }

        private void ShowPauseScreen()
        {
            _startGameText.enabled = true;
            _highScoreText.enabled = true;
            _scoreText.enabled = false;
        }
    }
}