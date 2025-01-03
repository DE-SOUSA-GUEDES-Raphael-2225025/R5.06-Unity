using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private int keyCount;
    private int coinCount;

    public static GameManager instance { get; private set; }

    public UnityEvent keyChangeEvent = new UnityEvent();
    public UnityEvent coinChangeEvent = new UnityEvent();

    public void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        instance = this;
    }

    public int GetKeyCount() {
        return keyCount;
    }

    public int GetCoinCount() {
        return coinCount;
    }

    public void AddCoin(int count) {
        coinCount += count;
        coinChangeEvent.Invoke();
    }

    public void AddKey(int count) {
        keyCount += count;
        keyChangeEvent.Invoke();
    }

    



    
}
