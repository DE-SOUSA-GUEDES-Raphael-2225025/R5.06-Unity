using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private WeaponScriptableObject spawnWeapon;
    [SerializeField] private TMP_Text ammoTracker;
    [SerializeField] private Image reloadImage;
    [SerializeField] private Transform shootPoint;

    private WeaponScriptableObject currentWeapon;
    private bool reloading = false;
    private float reloadTimer = 0f;
   

    private void Start() {
        
        currentWeapon = spawnWeapon;
        currentWeapon.Reload();
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
        if (currentWeapon.GetAmmo() >= 1) {
            currentWeapon.currentAmmo--;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            Debug.DrawRay(ray.origin, ray.direction * 2000, Color.red, 1f);

            if (Physics.Raycast(ray, out hit, 2000)) {
                CreateTrail(shootPoint.position, hit.point);

                IDamageable enemyHit = hit.collider.GetComponentInParent<IDamageable>();
                if (enemyHit != null) {
                    enemyHit.Damage(currentWeapon.damage);
                }
            } else {
                CreateTrail(shootPoint.position,shootPoint.position + ray.direction * 1000);
            }
        } else {
            Reload();
        }
    }

    public void CreateTrail(Vector3 origin, Vector3 endPoint) {
        GameObject trailObject = Instantiate(currentWeapon.trail, origin, Quaternion.identity);
        Destroy(trailObject, 3f);
        StartCoroutine(PlayTrail(origin, endPoint, trailObject));
    }

    public IEnumerator PlayTrail(Vector3 origin, Vector3 endPoint, GameObject instance) {
        float distance = Vector3.Distance(origin, endPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0) {

            if(instance == null) { break; }

            instance.transform.position = Vector3.Lerp(
                origin, 
                endPoint,
                Mathf.Clamp01(1 - (remainingDistance / distance))
            );
            remainingDistance -= currentWeapon.trailSpeed * Time.deltaTime;

            yield return null;
        }
    }


    private void UpdateUI() {
        ammoTracker.text = currentWeapon.GetAmmo() + " / " + currentWeapon.GetMaxAmmo(); 
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
