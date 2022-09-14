using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public class AsteroidFracture : MonoBehaviour
    {
        [SerializeField] private GameObject _fracturedPrefab;

        private readonly float _fractureLifetime = 2f;

        public void FractureObject()
        {
            Destroy(gameObject);
            var _fracturedObject = Instantiate(_fracturedPrefab, transform.position, transform.rotation);
            Destroy(_fracturedObject, _fractureLifetime);
        }
    }
}