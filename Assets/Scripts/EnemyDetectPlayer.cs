using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectPlayer : MonoBehaviour
{
    private GameObject focusPlayer;
    [SerializeField] private Transform parentTransform;


    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerHealthManager>() == null) return;

        focusPlayer = other.gameObject;
    }

    private void OnTriggerExit(Collider other) {
        if (focusPlayer != null) focusPlayer = null;
    }

    private void Update() {
        if (focusPlayer == null) return;
        parentTransform.LookAt(focusPlayer.transform);
        parentTransform.rotation = new Quaternion(
                0f,
                parentTransform.rotation.y,
                0f,
                parentTransform.rotation.w
                );

    }
}
