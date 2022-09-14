using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;

namespace Assets.Scripts.Enemy
{
    public class Asteroid : MonoBehaviour, IMovable, IRotatable, IDifficultyIncreaser
    {
        [SerializeField] private Transform _targetCenter;

        private const float SPEED_MAX_BOUND = 10f;
        private static float _movementSpeed = 2f;
        private static float _rotationSpeed = 20f;

        public float MovementSpeed
        {
            get => _movementSpeed;
            set
            {
                if (value < 0f)
                    throw new UnityException("Invalid argument");
                _movementSpeed = value;
            }
        }
        public float RotationSpeed
        {
            get => _rotationSpeed;
            set
            {
                if (value < 0f)
                    throw new UnityException("Invalid argument");
                _rotationSpeed = value;
            }
        }
        public float DifficultyModifier => 0.2f;

        private void Update()
        {
            Move();
            Rotate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.GetComponent<Earth>())
            {
                IncreaseDifficulty();
                GetComponent<AsteroidFracture>().FractureObject();
            }
        }

        public static void ResetSpeed()
        {
            _movementSpeed = 2f;
            _rotationSpeed = 20f;
        }

        public void Move()
        {
            Vector2 movementDirection = (_targetCenter.position - transform.position).normalized;
            transform.Translate(movementDirection * Time.deltaTime * _movementSpeed);
        }

        public void Rotate()
        {
            transform.Rotate(transform.position, Time.deltaTime * _rotationSpeed);
        }

        public void IncreaseDifficulty()
        {
            if (_movementSpeed < SPEED_MAX_BOUND)
            {
                var rotationCoefficient = _rotationSpeed / _movementSpeed;
                _rotationSpeed += rotationCoefficient * DifficultyModifier;
                _movementSpeed += DifficultyModifier;
            }
        }
    }
}