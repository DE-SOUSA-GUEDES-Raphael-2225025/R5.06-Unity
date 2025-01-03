using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUIManager : MonoBehaviour
{
    public GameObject deathCanvas; // Référence au Canvas
    public GameObject player;      // Référence au joueur

    // Affiche l'écran de mort
        public void ShowDeathScreen()
        {
            Debug.Log("ShowDeathScreen appelé."); // Confirme l'appel

            if (deathCanvas != null)
            {
                deathCanvas.SetActive(true); // Active le Canvas
                Debug.Log("Canvas activé : " + deathCanvas.activeSelf); // Vérifie si le Canvas est activé
            }
            else
            {
                Debug.LogError("Death Canvas n'est pas assigné !");
            }

            Time.timeScale = 0; // Met le jeu en pause

            if (player != null)
            {
                player.SetActive(false); // Désactive le joueur
            }
            else
            {
                Debug.LogError("Player n'est pas assigné !");
            }
        }


        // Cache l'écran de mort
        public void HideDeathScreen()
    {
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(false); // Cache le Canvas
            Debug.Log("Canvas caché.");
        }

        Time.timeScale = 1; // Remet le jeu en marche

        if (player != null)
        {
            player.SetActive(true); // Réactive le joueur
        }
    }

    // Recharge la scène actuelle
    public void RetryGame()
    {
        Time.timeScale = 1; // Remet le temps à la normale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recharge la scène actuelle
    }

    // Retourne au menu principal
    public void GoToMenu()
    {
        Time.timeScale = 1; // Remet le temps à la normale
        SceneManager.LoadScene("MainMenu"); // Remplace "MainMenu" par le nom de ta scène de menu
    }
}
