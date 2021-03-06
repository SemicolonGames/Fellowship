using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommonFunctions
{
    public void UpdateModifiedSpeed();
    public void UpdateAdditiveAbilityDamage();
    public float GetAdditiveAbilityDamage();
    public void ShowNotEnoughManaText();
    public Transform GetTarget();
    void DisablePlayerControl();
    void EnablePlayerControl();
}