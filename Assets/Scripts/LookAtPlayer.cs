using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    
    private void LateUpdate() {
        transform.LookAt(GameObject.FindGameObjectsWithTag("Player")[0].transform);
        transform.Rotate(0, 180, 0);
    }
}
