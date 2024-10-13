using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MainMenu : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreditsMenu;
    public GameObject CreditsText;
    public GameObject BackButton;

    // Start is called before the first frame update
    void Start()
    {
        MainMenuButton(); // Affiche le menu principal au démarrage
    }

    public void PlayNowButton()
    {
        Debug.Log("PlayNow button clicked.");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
    public void MainMenuButton()
    {
        // Afficher le menu principal
        MainMenu.SetActive(true);
        BackButton.SetActive(false);
        CreditsText.SetActive(false);

    }
    public void CreditsButton()
    {
        // Show Credits Menu
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
        CreditsText.SetActive(true);
        BackButton.SetActive(true);

    }

    public void BackToMainMenu()
    {
        // Charger la scène du menu principal
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }

    public void QuitButton()
    {
        // Quitter le jeu
        Application.Quit();
    }
}