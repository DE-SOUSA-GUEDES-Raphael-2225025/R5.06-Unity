using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinCountText;
    [SerializeField] private TMP_Text keyCountText;
    public static UIManager instance { get; private set; }

    public void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        instance = this;
    }

    private void Start() {
        GameManager.instance.keyChangeEvent.AddListener(UpdateKey);
        GameManager.instance.coinChangeEvent.AddListener(UpdateCoin);
    }

    public void UpdateCoin() {
        coinCountText.text = GameManager.instance.GetCoinCount().ToString();
    }

    public void UpdateKey() {
        keyCountText.text = GameManager.instance.GetKeyCount().ToString();
    }

}
