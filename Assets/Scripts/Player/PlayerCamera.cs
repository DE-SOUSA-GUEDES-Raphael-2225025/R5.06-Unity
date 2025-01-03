using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class PlayerCamera : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Vector2 _move;
    public Vector2 _look;
    public float aimValue;
    public float fireValue;

    public Vector3 nextPosition;
    public Quaternion nextRotation;

    public float rotationPower = 3f;
    public float rotationLerp = 0.5f;

    public float speed = 1f;
    public Camera camera;

    public GameObject followTransform;

    void Start()
    {
        
    }

    void Update() {
        // Gestion de l'entrée utilisateur
        _look = new Vector2(Input.GetAxis("Mouse X") / 5, -Input.GetAxis("Mouse Y") / 5);

        // Appliquer la rotation horizontale (yaw) sur followTransform
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.x * rotationPower, Vector3.up);

        // Appliquer la rotation verticale (pitch) sur followTransform
        followTransform.transform.rotation *= Quaternion.AngleAxis(_look.y * rotationPower, Vector3.right);

        // Clamp de la rotation verticale
        Vector3 followAngles = followTransform.transform.localEulerAngles;
        followAngles.z = 0; // Verrouiller l'axe Z pour éviter les inclinaisons
        float clampedX = followAngles.x > 180 ? Mathf.Clamp(followAngles.x, 340, 360) : Mathf.Clamp(followAngles.x, 0, 40);
        followAngles.x = clampedX;

        followTransform.transform.localEulerAngles = followAngles;

        // Synchroniser transform.rotation avec followTransform, mais uniquement pour l'axe Y (yaw)
        transform.rotation = Quaternion.Euler(0, followTransform.transform.eulerAngles.y, 0);

        // Réinitialiser le yaw de followTransform pour qu'il reste indépendant
        followTransform.transform.localEulerAngles = new Vector3(followAngles.x, 0, 0);

        // Gestion du déplacement (si nécessaire)
        if (_move.x != 0 || _move.y != 0) {
            float moveSpeed = speed / 100f;
            Vector3 moveDirection = (transform.forward * _move.y * moveSpeed) + (transform.right * _move.x * moveSpeed);
            nextPosition = transform.position + moveDirection;
        } else {
            nextPosition = transform.position;
        }
    }



    /*float inputX = Input.GetAxis("Mouse X") * sensivity;
    float inputY = Input.GetAxis("Mouse Y") * sensivity;

    // Rotation horizontale
    transform.Rotate(Vector3.up, inputX);

    // Rotation verticale (pitch)
    _pitch -= inputY;
    _pitch = Mathf.Clamp(_pitch, -90f, 90f);

    _yaw -= inputX;
    // Appliquer le pitch uniquement à la caméra
    transform.rotation = Quaternion.Euler(_pitch, _yaw, transform.eulerAngles.z);
    Camera.main.transform.localEulerAngles = new Vector3(_pitch, 0f, 0f);+*/
}
