using UnityEngine;

public class SC_CreditsMenu : MonoBehaviour
{
    public void BackToMainMenu()
    {
        // Charger la scène du menu principal
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }
}
