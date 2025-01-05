using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, Interactable
{
    [SerializeField] GameObject coinObject;
    [SerializeField] Animator chestAnimator;
    [SerializeField] GameObject chestLight;

    public void playInteraction() {
        if (GameManager.instance.GetKeyCount() == 0) return;
        GameManager.instance.RemoveKey();
        StartCoroutine(dropCoin());
    }

    public IEnumerator dropCoin() {

        chestAnimator.SetTrigger("Open");
        yield return new WaitForSeconds(2f);

        chestLight.SetActive(false);

        for (int i = 0; i < 10; i++) {
            GameObject coin = Instantiate(coinObject, transform.position + new Vector3(0,1,0), Quaternion.identity);
            coin.GetComponent<Rigidbody>().AddForce(new Vector3 (0,10,0));
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
    
}
