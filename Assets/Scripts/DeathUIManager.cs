using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUIManager : MonoBehaviour
{
    public GameObject deathCanvas; // R�f�rence au Canvas
    public GameObject player;      // R�f�rence au joueur

    // Affiche l'�cran de mort
        public void ShowDeathScreen()
        {
            Debug.Log("ShowDeathScreen appel�."); // Confirme l'appel

            if (deathCanvas != null)
            {
                deathCanvas.SetActive(true); // Active le Canvas
                Debug.Log("Canvas activ� : " + deathCanvas.activeSelf); // V�rifie si le Canvas est activ�
            }
            else
            {
                Debug.LogError("Death Canvas n'est pas assign� !");
            }

            Time.timeScale = 0; // Met le jeu en pause

            if (player != null)
            {
                player.SetActive(false); // D�sactive le joueur
            }
            else
            {
                Debug.LogError("Player n'est pas assign� !");
            }
        }


        // Cache l'�cran de mort
        public void HideDeathScreen()
    {
        if (deathCanvas != null)
        {
            deathCanvas.SetActive(false); // Cache le Canvas
            Debug.Log("Canvas cach�.");
        }

        Time.timeScale = 1; // Remet le jeu en marche

        if (player != null)
        {
            player.SetActive(true); // R�active le joueur
        }
    }

    // Recharge la sc�ne actuelle
    public void RetryGame()
    {
        Time.timeScale = 1; // Remet le temps � la normale
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recharge la sc�ne actuelle
    }

    // Retourne au menu principal
    public void GoToMenu()
    {
        Time.timeScale = 1; // Remet le temps � la normale
        SceneManager.LoadScene("MainMenu"); // Remplace "MainMenu" par le nom de ta sc�ne de menu
    }
}
