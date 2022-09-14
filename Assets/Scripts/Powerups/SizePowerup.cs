using UnityEngine;
using System.Collections;
using Assets.Scripts.Player;

namespace Assets.Scripts.Powerups
{
    public sealed class SizePowerup : Powerup
    {
        protected override float Duration => 6f;
        protected override float PowerupCoefficient => 1.7f;

        public override IEnumerator Apply(Moon moon)
        {
            moon.transform.localScale *= PowerupCoefficient;
            yield return new WaitForSeconds(Duration);
            moon.transform.localScale /= PowerupCoefficient;
        }
    }
}