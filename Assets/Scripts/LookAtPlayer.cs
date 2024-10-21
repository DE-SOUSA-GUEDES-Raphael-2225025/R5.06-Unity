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
        }
        else
        {
            Debug.LogWarning("Aucun objet avec le tag 'Player' n'a �t� trouv�.");
        }
    }
}
