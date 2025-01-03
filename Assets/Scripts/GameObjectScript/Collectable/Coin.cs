using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, Collectable
{
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private GameManager gameManager;
 
    void Update()
    {
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime * 50, 0));
    }

    public void Collect() {
        GameManager.instance.AddCoin(1);
    }
}
