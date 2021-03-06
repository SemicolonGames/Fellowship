using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Effect", menuName = "Abilities/Effects/Health", order = 0)]
public class HealthEffect : EffectStrategy
{
    [SerializeField] float skillDamageMultiplier = 1;

    public override void StartEffect(AbilityData data, Action finished)
    {
        foreach (var target in data.GetTargets())
        {
            float healthChange = data.GetUser().GetComponent<BaseStats>().GetStat(Stat.Damage) * skillDamageMultiplier;
            var health = target.GetComponent<Health>();
            if (health)
            {
                if (healthChange > 0)
                {
                    health.ApplyDamage(data.GetUser(), healthChange);
                }
                else
                {
                    health.Heal(healthChange);
                }
            }
        }
        finished();
    }
}