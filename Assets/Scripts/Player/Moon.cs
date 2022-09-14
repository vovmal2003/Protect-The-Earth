using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Enemy;
using Assets.Scripts.Powerups;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Player
{
    public class Moon : MonoBehaviour, IMovable, IDifficultyIncreaser
    {
        [SerializeField] private Transform _rotationCenter;

        private const float SPEED_MAX_BOUND = 8f;
        private readonly float _radius = 2f;
        private float _movementSpeed = 1f;
        private float _angle = 0f;

        private AudioManager AudioManager => GameController.Instance.AudioManager;

        public float MovementSpeed
        {
            get => _movementSpeed;
            set
            {
                if (Mathf.Abs(value) < SPEED_MAX_BOUND)
                    _movementSpeed = value;
            }
        }
        public float DifficultyModifier => 1.1f;

        private void Update()
        {
            Move();
            CheckMovementDirection();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Asteroid>())
            {
                AudioManager.PlayReceivingScoreSound();
                GameController.Instance.AddScore(1);
                IncreaseDifficulty();
            }
        }

        private void OnTriggerEnter(Collider powerupCollider)
        {
            AudioManager.PlayReceivingPowerupSound();
            StartCoroutine(powerupCollider.gameObject.GetComponent<Powerup>().Apply(this));
            Destroy(powerupCollider.gameObject);
        }

        public void Move()
        {
            var posX = _rotationCenter.position.x + Mathf.Cos(_angle) * _radius;
            var posY = _rotationCenter.position.y + Mathf.Sin(_angle) * _radius;
            transform.position = new Vector2(posX, posY);
            _angle += Time.deltaTime * MovementSpeed;
        }

        public void IncreaseDifficulty()
        {
            MovementSpeed *= DifficultyModifier;
        }

        private void CheckMovementDirection()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                MovementSpeed = -MovementSpeed;
        }
    }
}