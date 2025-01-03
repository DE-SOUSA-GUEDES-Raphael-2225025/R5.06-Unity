using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItem {
    public GameObject itemPrefab; 
    public int minCount;         
    public int maxCount;          
    [Range(0f, 1f)]
    public float dropChance;      
}

public class LootDrop : MonoBehaviour {
    [SerializeField]
    private LootItem[] lootTable; 

    public void Drop() {
        foreach (LootItem loot in lootTable) {
                int dropCount = Random.Range(loot.minCount, loot.maxCount + 1);

                Debug.Log(dropCount);
                if (Random.value <= loot.dropChance) {
                    for (int i = 0; i < dropCount; i++) {
                    if (loot.itemPrefab != null) {
                        Vector3 dropPosition = transform.position + new Vector3(
                            Random.Range(-1f, 1f),  
                            1,                     
                            Random.Range(-1f, 1f)  
                        );

                        Instantiate(loot.itemPrefab, dropPosition, Quaternion.identity);
                    }
                }
            }
        }
    }
}
