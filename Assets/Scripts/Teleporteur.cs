using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform destination; // Le point d'arriv�e

    private void OnTriggerEnter(Collider other)
    {
        // V�rifie si l'objet entrant est le joueur
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            // Si le joueur a un CharacterController, d�sactive-le temporairement
            if (controller != null)
            {
                controller.enabled = false;
                other.transform.position = destination.position;
                controller.enabled = true;
            }
            else
            {
                // Si pas de CharacterController, t�l�porte directement
                other.transform.position = destination.position;
            }
        }
    }
}
