using UnityEngine;
using System.Collections;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Player;

namespace Assets.Scripts.Powerups
{
    public abstract class Powerup : MonoBehaviour, IMovable
    {
        [SerializeField] private Transform _targetCenter;

        private float _movementSpeed = 1.5f;

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

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            Vector2 movementDirection = (_targetCenter.position - transform.position).normalized;
            transform.Translate(movementDirection * Time.deltaTime * _movementSpeed);
        }

        protected abstract float Duration { get; }
        protected abstract float PowerupCoefficient { get; }
        public abstract IEnumerator Apply(Moon moon);
    }
}