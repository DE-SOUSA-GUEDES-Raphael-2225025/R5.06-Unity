using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;
    [SerializeField] GameObject HUD;
    [SerializeField] TMP_Text winScore;
    [SerializeField] TMP_Text loseScore;

    private Boolean gameEnded = false;
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

    public void RemoveKey() {
        keyCount -= 1;
        keyChangeEvent.Invoke();
    }

    public void winGame() {
        gameEnded = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        HUD.SetActive(false);
        winScreen.SetActive(true);
        winScore.text = "SCORE : " + coinCount.ToString();
    }

    public void LoseGame() {
        gameEnded = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        HUD.SetActive(false);
        loseScreen.SetActive(true);
        loseScore.text = "SCORE : " + coinCount.ToString();
    }

    public bool IsGameEnded() { return gameEnded; }

    public void GoToMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }






}
