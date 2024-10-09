using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

    [SerializeField] private GameObject[] bottomRooms;
    [SerializeField] private GameObject[] topRooms;
    [SerializeField] private GameObject[] rightRooms;
    [SerializeField] private  GameObject[] leftRooms;
    [SerializeField] private GameObject closedRoom;

    public GameObject[] getTopRooms() { return topRooms; }

    public GameObject[] getBottomRooms() { return bottomRooms; }

    public GameObject[] getLeftRooms() { return leftRooms; }

    public GameObject[] getRightRooms() { return rightRooms; }

    public GameObject getClosedRoom() { return closedRoom; }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Room") && other.GetInstanceID() > GetInstanceID()) {
            Debug.Log(gameObject.name + " collision with " + other.name);
            Destroy(gameObject);
            return;
        }
    }

}
