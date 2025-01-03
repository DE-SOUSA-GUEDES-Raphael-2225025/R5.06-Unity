using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination; // Le point d'arrivée

    private void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet entrant est le joueur
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            // Si le joueur a un CharacterController, désactive-le temporairement
            if (controller != null)
            {
                controller.enabled = false;
                other.transform.position = destination.position;
                controller.enabled = true;
            }
            else
            {
                // Si pas de CharacterController, téléporte directement
                other.transform.position = destination.position;
            }
        }
    }
}
