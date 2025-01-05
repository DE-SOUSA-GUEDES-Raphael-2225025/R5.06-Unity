using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private float delayBetweenDamage = 2.0f;
    private float remainingDelay = 0f;

    private void Update() {
        if(remainingDelay > 0f) {
            remainingDelay -= Time.deltaTime;   
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.GetComponent<PlayerManager>() != null && remainingDelay <= 0) {
            other.GetComponent<PlayerManager>().Damage(10);
            remainingDelay = delayBetweenDamage;
            gameObject.SetActive(false);
        }
    }

}
