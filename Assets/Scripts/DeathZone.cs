using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform respawnPoint; // Point de réapparition
    public float respawnDelay = 0.5f; // Temps avant le respawn
    private bool isRespawning = false; // Empêche plusieurs morts simultanées

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isRespawning)
        {
            isRespawning = true;

            // Déclenche immédiatement l'animation de mort
            Animator animator = other.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger("DeathTrigger");
                Debug.Log("Animation de mort déclenchée.");
            }

            // Lance le respawn après un délai
            StartCoroutine(RespawnPlayer(other));
        }
    }

    private IEnumerator RespawnPlayer(Collider player)
    {
        yield return new WaitForSeconds(respawnDelay); // Attends la fin de l'animation

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
            animator.ResetTrigger("DeathTrigger"); // Réinitialise le trigger
            animator.Play("Idle"); // Force le retour à l'état Idle
        }

        // Téléporte le joueur
        player.transform.position = respawnPoint.position;

        // Réactiver le CharacterController
        if (controller != null)
        {
            controller.enabled = true;
        }

        Debug.Log("Le joueur a été téléporté au point de respawn.");
        isRespawning = false;
    }
}
