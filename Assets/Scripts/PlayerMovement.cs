using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private InputAction moveAction;
    private InputAction jumpAction;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;

    private int jumpsRemaining = 2; // Nombre de sauts disponibles (1 saut normal + 1 double saut)
    private bool isGrounded = false; // Indique si le joueur est au sol

    void OnEnable() {
        moveAction = GetComponent<PlayerInput>().actions.FindAction("Move", true);
        jumpAction = GetComponent<PlayerInput>().actions.FindAction("Jump", true);
        jumpAction.performed += Jump;
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(inputVector.x, 0, inputVector.y) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);
    }

    void Jump(InputAction.CallbackContext context) {
        // Si des sauts sont disponibles
        if (jumpsRemaining > 0) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsRemaining--; // Diminue le nombre de sauts restants
        }
    }

    // Détection de la collision avec le sol
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) // Vérifie que c'est bien le sol
        {
            isGrounded = true;
            jumpsRemaining = 2; // Réinitialise les sauts quand on touche le sol
        }
    }

    // Quand le joueur quitte le sol
    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = false; // Indique que le joueur n'est plus au sol
        }
    }

    void OnDisable() {
        jumpAction.performed -= Jump;
    }
}