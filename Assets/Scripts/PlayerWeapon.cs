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
    [SerializeField] private Image reloadImage;

    private Weapon currentWeapon;
    private bool reloading = false;
    private float reloadTimer = 0f;

    private void Start() {
        currentWeapon = spawnWeapon;
        currentWeapon.currentAmmo = currentWeapon.maxAmmo;
        reloadImage.enabled = false;
        UpdateUI();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Shoot();
            UpdateUI();
        }

        if(Input.GetKeyDown(KeyCode.R)) {
            Reload();
        }

        CheckReload();
    }

    private void Shoot() {
        if (currentWeapon.currentAmmo >= 1) {
            currentWeapon.currentAmmo--;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            Debug.DrawRay(ray.origin, ray.direction * 2000, Color.red, 1f);

            int layerMask = LayerMask.GetMask("Enemy");

            if (Physics.Raycast(ray, out hit, 2000, layerMask)) {
                IDamageable enemyHit = hit.collider.GetComponentInParent<IDamageable>();
                if (enemyHit != null) {
                    enemyHit.Damage(currentWeapon.damage);
                }
            } else {
            }
        } else {
            Reload();
        }
    }


    private void UpdateUI() {
        ammoTracker.text = currentWeapon.currentAmmo + " / " + currentWeapon.maxAmmo;
    }

    private void Reload() {
        reloading = true;
        reloadImage.enabled = true;
    }

    private void CheckReload() {
        if (reloading) {
            reloadTimer += Time.deltaTime;
            reloadImage.fillAmount = (float) (reloadTimer / currentWeapon.reloadTime);
        }

        if(reloading && reloadTimer >= currentWeapon.reloadTime) {
            currentWeapon.Reload();
            reloading = false;
            reloadImage.enabled = false;
            reloadTimer = 0;
            UpdateUI();
        }
    }
}
