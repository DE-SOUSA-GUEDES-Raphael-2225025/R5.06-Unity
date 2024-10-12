using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField] private float damageOnHitValue = 0f;
    [SerializeField] private bool destroyOnHit = false;
    [SerializeField] private float noDamageDelay = 1;
    private float delay = 0f;

    private void Update() {
        if (delay > 0) delay -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<PlayerHealthManager>() != null) {
            if (delay > 0) return;

            PlayerHealthManager playerHealthManager = other.GetComponent<PlayerHealthManager>();
            playerHealthManager.Damage(damageOnHitValue);
            delay = noDamageDelay;

            if (destroyOnHit) {
                Destroy(gameObject);
            }
        }
    }



 
}
