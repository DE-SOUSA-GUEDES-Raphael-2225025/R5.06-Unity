using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public int maxAmmo;
    public new string name;
    public int currentAmmo;
    public double damage;
    public double reloadTime;

    public float trailSpeed;
    public GameObject trail;
    public GameObject gunModel;



    public void Reload() {
        currentAmmo = maxAmmo;
    }

    public int GetAmmo() {
        return currentAmmo;
    }

    public int GetMaxAmmo() {
        return maxAmmo;
    }
}
