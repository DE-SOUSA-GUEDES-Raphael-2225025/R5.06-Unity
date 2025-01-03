using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, Collectable
{
    [SerializeField] private GameManager gameManager;

    public void Collect() {
        GameManager.instance.AddKey(1);
    }
}
