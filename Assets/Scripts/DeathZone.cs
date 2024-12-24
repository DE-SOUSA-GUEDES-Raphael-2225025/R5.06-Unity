using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform respawnPoint; // Point de r�apparition
    public float respawnDelay = 2.0f; // Temps avant le respawn
    private bool isRespawning = false; // Emp�che plusieurs morts simultan�es

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRespawning)
        {
            isRespawning = true;

            // D�clenche l'animation de mort
            Animator animator = other.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("DeathTrigger");
                Debug.Log("Animation de mort d�clench�e.");
            }

            // Affiche l'�cran de mort
            DeathUIManager deathUI = FindObjectOfType<DeathUIManager>();
            if (deathUI != null)
            {
                Debug.Log("Affichage de l'�cran de mort.");
                deathUI.ShowDeathScreen();
            }
            else
            {
                Debug.LogError("DeathUIManager introuvable !");
            }

            // Lance la coroutine pour respawn apr�s un d�lai
            StartCoroutine(RespawnPlayer(other));
        }
    }

    private IEnumerator RespawnPlayer(Collider player)
    {
        yield return new WaitForSeconds(respawnDelay); // Attends le d�lai

        // D�sactiver temporairement le CharacterController
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // R�initialiser l'Animator
        Animator animator = player.GetComponent<Animator>();
        if (animator != null)
        {
            animator.ResetTrigger("DeathTrigger");
            animator.Play("Idle"); // Force le retour � l'�tat Idle
        }

        // T�l�porte le joueur
        player.transform.position = respawnPoint.position;

        // R�active le CharacterController
        if (controller != null)
        {
            controller.enabled = true;
        }

        Debug.Log("Le joueur a �t� t�l�port� au point de respawn.");

        // Cache l'�cran de mort apr�s le respawn
        DeathUIManager deathUI = FindObjectOfType<DeathUIManager>();
        if (deathUI != null)
        {
            deathUI.HideDeathScreen();
        }

        isRespawning = false;
    }
}
