using UnityEngine;

public class Teleporter : MonoBehaviour {
    public Transform destination;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {

            CharacterController characterController = other.GetComponent<CharacterController>();
            if (characterController != null) {
                characterController.enabled = false;
                other.transform.position = destination != null ? destination.position : new Vector3(0, 0, 0);
                characterController.enabled = true;

            }
        }
    }
}