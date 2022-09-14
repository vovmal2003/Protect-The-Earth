using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Enemy;

namespace Assets.Scripts.Player
{
    public class Earth : MonoBehaviour, IRotatable
    {
        private float _rotationSpeed = 10f;

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

        private void Update()
        {
            Rotate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Asteroid>())
                GameController.Instance.GameOver();
        }

        private void OnTriggerEnter(Collider powerupCollider)
        {
            Destroy(powerupCollider.gameObject);
        }

        public void Rotate()
        {
            transform.Rotate(Vector2.up, Time.deltaTime * _rotationSpeed);
        }
    }
}