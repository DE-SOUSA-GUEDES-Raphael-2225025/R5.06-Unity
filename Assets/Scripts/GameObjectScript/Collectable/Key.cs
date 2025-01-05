using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, Collectable
{
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip pickupSound;

    void Update() {
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime * 50, 0));
    }

    public void Collect() {
        GameManager.instance.AddKey(1);
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
    }
}
