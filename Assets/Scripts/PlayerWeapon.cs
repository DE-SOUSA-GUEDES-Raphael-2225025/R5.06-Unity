using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Weapon spawnWeapon;
    [SerializeField] private TMP_Text ammoTracker;
    private Weapon currentWeapon;

    private void Start() {
        currentWeapon = spawnWeapon;
        currentWeapon.currentAmmo = currentWeapon.maxAmmo;
        UpdateUI();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Shoot();
            UpdateUI();
        }
    }

    private void Shoot() {
        if(currentWeapon.currentAmmo >= 1) {
            currentWeapon.currentAmmo--;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit)) {
                if(hit.collider.GetComponentInParent<IDamageable>() != null) {
                    IDamageable enemyHit = hit.collider.GetComponentInParent<IDamageable>();
                    enemyHit.Damage(currentWeapon.damage);
                }
                Debug.Log("Enemy miss hit:" + hit.collider.name);

            }
        } else {
            Debug.Log("Reload needed");
        }
    }

    private void UpdateUI() {
        ammoTracker.text = currentWeapon.currentAmmo + " / " + currentWeapon.maxAmmo;
    }
}
