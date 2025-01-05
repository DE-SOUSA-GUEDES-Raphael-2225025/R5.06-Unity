using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour, Interactable {

    public void playInteraction() {
        GameManager.instance.winGame();
    }
}
