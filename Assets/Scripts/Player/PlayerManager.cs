using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float baseHealth = 20.0f;
    [SerializeField] private Image healthVisual;
    private float health;
    private int coins { get; set; }
    private int keys { get; set; }

    private UnityEvent healthChangeEvent = new UnityEvent();

    private void Start() {
        health = baseHealth;
        healthChangeEvent.AddListener(UpdateVisual);
    }

    public void Heal(float healValue) {
        health += healValue;
        healthChangeEvent.Invoke();
    }

    public void Damage(float damageValue)
    {
        health -= damageValue;
        healthChangeEvent.Invoke();

        if (health <= 0)
        {
            OnDeath();
        }
    }

    private void UpdateVisual() {
        healthVisual.fillAmount = (health/ baseHealth);
    }

    public void OnDeath() {
        Debug.Log("Vous etes mort");
    }

    public void OnTakeDamage() {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Collectable>() != null) {
            other.GetComponent<Collectable>().Collect();
            Destroy(other.gameObject);
        }
    }
}
