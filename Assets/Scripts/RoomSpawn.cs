using UnityEngine;

public class RoomSpawn : MonoBehaviour {
    public enum RoomSide {
        LEFT, RIGHT, TOP, BOTTOM
    }

    [SerializeField] public RoomSide openingDirection;
    [SerializeField] private float roomScale = 3.0f;

    private RoomTemplates templates;
    public bool spawned;

    private void Start() {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.5f); // Légère temporisation pour permettre l'initialisation des salles
    }

    private void Spawn() {
        if (spawned) return; // Vérifie si la salle a déjà été spawnée

        GameObject[] roomList = null;

        // Récupère la liste des salles selon la direction
        switch (openingDirection) {
            case RoomSide.LEFT:
                roomList = templates.getLeftRooms();
                break;
            case RoomSide.RIGHT:
                roomList = templates.getRightRooms();
                break;
            case RoomSide.TOP:
                roomList = templates.getTopRooms();
                break;
            case RoomSide.BOTTOM:
                roomList = templates.getBottomRooms();
                break;
        }

        if (roomList == null || roomList.Length == 0) return;

        GameObject chosenRoom = roomList[Random.Range(0, roomList.Length)];

        // Instancie la salle sans chevauchement
        Instantiate(chosenRoom, new Vector3(transform.position.x - (5*roomScale), 0, transform.position.z - (5 * roomScale)), Quaternion.identity);
        spawned = true;
    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Room")) {
            Destroy(gameObject);
            return;
        }

        if (other.GetComponent<RoomSpawn>() != null) {
            if (GetInstanceID() > other.GetInstanceID() && !spawned) {
                spawned = true; // Marque cette salle comme spawnée
                Destroy(gameObject); // Supprime l'instance du spawn
            }
        }
    }
}
