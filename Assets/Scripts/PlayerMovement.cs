using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float sensivity = 3.0f;
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    private float _pitch;

    CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    private float cameraVerticalRotation = 0;

    [HideInInspector]
    public bool canMove = true;

    // Ajout du composant Animator
    public Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) {
            moveDirection.y = jumpSpeed;
        } else {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded) {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);


        float inputX = Input.GetAxis("Mouse X") * sensivity;
        float inputY = Input.GetAxis("Mouse Y") * sensivity;

        transform.Rotate(Vector3.up, inputX * sensivity * Time.deltaTime);
        _pitch -= inputY * sensivity * Time.deltaTime;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        transform.localEulerAngles = new Vector3(_pitch, transform.localEulerAngles.y, 0f);
        HandleAnimation(curSpeedX, curSpeedY, isRunning);
    }

    public void HandleAnimation(float curSpeedX, float curSpeedY, bool isRunning) {
        if (curSpeedX > 0) // Avancer
        {
            if (isRunning) {
                animator.SetBool("RunForward", true);
                animator.SetBool("WalkForward", false);
            } else {
                animator.SetBool("WalkForward", true);
                animator.SetBool("RunForward", false);
            }
        } else if (curSpeedX < 0) // Reculer
          {
            if (isRunning) {
                animator.SetBool("RunBackward", true);
                animator.SetBool("WalkBackward", false);
            } else {
                animator.SetBool("WalkBackward", true);
                animator.SetBool("RunBackward", false);
            }
        } else // Si pas de mouvement sur l'axe Vertical, d�sactiver les animations correspondantes
          {
            animator.SetBool("WalkForward", false);
            animator.SetBool("RunForward", false);
            animator.SetBool("WalkBackward", false);
            animator.SetBool("RunBackward", false);
        }

        if (curSpeedY > 0) // Aller � droite
        {
            if (isRunning) {
                animator.SetBool("RunRight", true);
                animator.SetBool("WalkRight", false);
            } else {
                animator.SetBool("WalkRight", true);
                animator.SetBool("RunRight", false);
            }
        } else if (curSpeedY < 0) // Aller � gauche
          {
            if (isRunning) {
                animator.SetBool("RunLeft", true);
                animator.SetBool("WalkLeft", false);
            } else {
                animator.SetBool("WalkLeft", true);
                animator.SetBool("RunLeft", false);
            }
        } else // Si pas de mouvement sur l'axe Horizontal, d�sactiver les animations correspondantes
          {
            animator.SetBool("WalkRight", false);
            animator.SetBool("RunRight", false);
            animator.SetBool("WalkLeft", false);
            animator.SetBool("RunLeft", false);
        }

        // G�rer l'animation de saut
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) {
            // Appliquer le saut et d�clencher l'animation de saut
            moveDirection.y = jumpSpeed;
            animator.SetTrigger("JumpTrigger"); // Activer le Trigger pour l'animation de saut
        }
    }
}
