using UnityEngine;

public class SC_CreditsMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        // Charger la sc�ne du menu principal
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }
}
