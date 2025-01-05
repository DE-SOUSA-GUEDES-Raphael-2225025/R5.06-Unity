using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField] private float damageOnHitValue = 0f;
    [SerializeField] private bool destroyOnHit = false;

    

    private void OnTriggerStay(Collider other) {
        if(other.GetComponent<PlayerManager>() != null) {

            PlayerManager playerHealthManager = other.GetComponent<PlayerManager>();
            playerHealthManager.Damage(damageOnHitValue / 10);

            if (destroyOnHit) {
                Destroy(gameObject);
            }
        }
    }



 
}
