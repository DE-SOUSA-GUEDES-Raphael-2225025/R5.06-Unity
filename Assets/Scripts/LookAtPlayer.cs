using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    void LateUpdate()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            transform.LookAt(players[0].transform);
            transform.Rotate(0, 180, 0);
            transform.rotation = new Quaternion(
                0f,
                transform.rotation.y,
                0f,
                transform.rotation.w
                );
        }
        else
        {
            Debug.LogWarning("Aucun objet avec le tag 'Player' n'a été trouvé.");
        }
    }
}
