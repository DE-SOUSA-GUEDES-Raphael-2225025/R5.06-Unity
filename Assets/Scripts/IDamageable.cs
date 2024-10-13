using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDamageable
{
    public void Kill();

    public void OnTakeDamage();

    public void Damage(double value);

    public void Heal(double value);
}
