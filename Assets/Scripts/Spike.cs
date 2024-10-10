using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private float upTime = 2.0f;
    [SerializeField] private float downTime = 3.0f;
    [SerializeField] private GameObject spikeModel;
    [SerializeField] private BoxCollider boxCollider;

    private bool isUp = true;
    private float time = 0f;

    private void Update() {
        HandleUpAndDown();
    }

    private void HandleUpAndDown() {
        time += Time.deltaTime;
        if (isUp && time > upTime) {
            isUp = false;
            time = 0;
            spikeModel.SetActive(false);
            boxCollider.enabled = false;
        }
        if (!isUp && time > downTime) {
            isUp = true;
            time = 0;
            spikeModel.SetActive(true);
            boxCollider.enabled = true;
        }
    }
}
