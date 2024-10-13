using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class Weapon : ScriptableObject
{
    public int maxAmmo;
    public new string name;
    public int currentAmmo;
    public double damage;

}
