using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.collider.GetComponent<PlayerHealthManager>() != null) {

        } else {
            Debug.Log("Collision with " + collision.collider.name);
        }
    }
}
