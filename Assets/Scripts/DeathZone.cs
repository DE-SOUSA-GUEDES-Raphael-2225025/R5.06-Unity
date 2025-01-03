using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform respawnPoint; // Point de réapparition
    public float respawnDelay = 2.0f; // Temps avant le respawn
    private bool isRespawning = false; // Empêche plusieurs morts simultanées

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRespawning)
        {
            isRespawning = true;

            // Déclenche l'animation de mort
            Animator animator = other.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("DeathTrigger");
                Debug.Log("Animation de mort déclenchée.");
            }

            // Affiche l'écran de mort
            DeathUIManager deathUI = FindObjectOfType<DeathUIManager>();
            if (deathUI != null)
            {
                Debug.Log("Affichage de l'écran de mort.");
                deathUI.ShowDeathScreen();
            }
            else
            {
                Debug.LogError("DeathUIManager introuvable !");
            }

            // Lance la coroutine pour respawn après un délai
            StartCoroutine(RespawnPlayer(other));
        }
    }

    private IEnumerator RespawnPlayer(Collider player)
    {
        yield return new WaitForSeconds(respawnDelay); // Attends le délai

        // Désactiver temporairement le CharacterController
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // Réinitialiser l'Animator
        Animator animator = player.GetComponent<Animator>();
        if (animator != null)
        {
            animator.ResetTrigger("DeathTrigger");
            animator.Play("Idle"); // Force le retour à l'état Idle
        }

        // Téléporte le joueur
        player.transform.position = respawnPoint.position;

        // Réactive le CharacterController
        if (controller != null)
        {
            controller.enabled = true;
        }

        Debug.Log("Le joueur a été téléporté au point de respawn.");

        // Cache l'écran de mort après le respawn
        DeathUIManager deathUI = FindObjectOfType<DeathUIManager>();
        if (deathUI != null)
        {
            deathUI.HideDeathScreen();
        }

        isRespawning = false;
    }
}
