using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class EffectsManager : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionPrefab;
        public float ExplosionLifetime => 7f;

        public void MakeExplosion()
        {
            var explosion = Instantiate(_explosionPrefab, Vector2.zero, Quaternion.identity);
            Destroy(explosion, ExplosionLifetime);
        }
    }
}