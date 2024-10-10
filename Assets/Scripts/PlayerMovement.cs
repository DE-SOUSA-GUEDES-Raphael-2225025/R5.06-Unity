using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction jumpAction;
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    private Rigidbody rb;

    private int jumpsRemaining = 2; // Nombre de sauts disponibles (1 saut normal + 1 double saut)
    private bool isGrounded; // Vérifie si le joueur est au sol

    void OnEnable()
    {
        moveAction = GetComponent<PlayerInput>().actions.FindAction("Move", true);
        jumpAction = GetComponent<PlayerInput>().actions.FindAction("Jump", true);
        jumpAction.performed += Jump;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(inputVector.x, 0, inputVector.y) * moveSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        // Vérifie si le joueur est au sol et réinitialise les sauts
        if (IsGrounded() && !isGrounded) // Si le joueur touche le sol après avoir été en l'air
        {
            isGrounded = true;
            jumpsRemaining = 2; // Réinitialise les sauts uniquement lorsque le joueur touche le sol
        }
        else if (!IsGrounded())
        {
            isGrounded = false;
        }
    }

    void Jump(InputAction.CallbackContext context)
    {
        // Si des sauts sont disponibles et que le joueur ne saute pas trop tôt
        if (jumpsRemaining > 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpsRemaining--; // Diminue le nombre de sauts restants
        }
    }

    bool IsGrounded()
    {

        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void OnDisable()
    {
        jumpAction.performed -= Jump;
    }
}
