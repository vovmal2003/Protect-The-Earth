using UnityEngine;
using System.Collections;
using Assets.Scripts.Player;

namespace Assets.Scripts.Powerups
{
    public sealed class SpeedPowerup : Powerup
    {
        protected override float Duration => 6f;
        protected override float PowerupCoefficient => 1.5f;

        public override IEnumerator Apply(Moon moon)
        {
            moon.MovementSpeed *= PowerupCoefficient;
            yield return new WaitForSeconds(Duration);
            moon.MovementSpeed /= PowerupCoefficient;
        }
    }
}