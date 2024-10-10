using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 5f;

    void Update()
    {
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime * 50, 0));
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Coin added");
            Destroy(gameObject);
        }
    }
}
