using UnityEngine;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Managers
{
    public class AudioManager : MonoBehaviour, IPauseHandler
    {
        [SerializeField] private AudioSource _mainTheme;
        [SerializeField] private AudioSource _planetExplosionSound;
        [SerializeField] private AudioSource _receivingPowerupSound;
        [SerializeField] private AudioSource _receivingScoreSound;
        [SerializeField] private AudioSource _startGameSound;

        private PauseManager PauseManager => GameController.Instance.PauseManager;

        private void Start()
        {
            PauseManager.Register(this);
        }

        public void SetPaused(bool isPaused)
        {
            if (isPaused)
                _mainTheme.Play();
            else
                _startGameSound.Play();
        }

        public void PlayMainTheme() => _mainTheme.Play();
        public void StopMainTheme() => _mainTheme.Stop();
        public void PlayPlanetExplosionSound() => _planetExplosionSound.Play();
        public void StopPlanetExplosionSound() => _planetExplosionSound.Stop();
        public void PlayReceivingPowerupSound() => _receivingPowerupSound.Play();
        public void PlayReceivingScoreSound() => _receivingScoreSound.Play();
        public void PlayStartGameSound() => _startGameSound.Play();
    }
}