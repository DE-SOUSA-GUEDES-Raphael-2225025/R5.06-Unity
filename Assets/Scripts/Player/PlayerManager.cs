using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float baseHealth = 20.0f;
    [SerializeField] private Image healthVisual;
    [SerializeField] private float baseShield = 20.0f;
    [SerializeField] private Image shieldVisual;
    [SerializeField] private float timeBeforeShieldRestore = 5f;

    private float timeLastHit = 0f;
    private float shield;
    private float health;
    private int coins { get; set; }
    private int keys { get; set; }

    private UnityEvent healthChangeEvent = new UnityEvent();

    private void Update() {
        if (timeLastHit < timeBeforeShieldRestore) {
            timeLastHit += Time.deltaTime;
        } else {
            if (shield < baseShield) {
                shield += 0.05f;
                UpdateVisual();
            }
        }
    }
    private void Start() {
        health = baseHealth;
        shield = baseShield;
        healthChangeEvent.AddListener(UpdateVisual);
    }

    public void Heal(float healValue) {
        health += healValue;
        healthChangeEvent.Invoke();
    }

    public void Damage(float damageValue)
    {
        timeLastHit = 0;

        if (shield < damageValue) {
            shield = 0f;
            health -= (damageValue - shield);
        } else {
            shield -= damageValue;
        }

        healthChangeEvent.Invoke();


        if (health <= 0)
        {
            OnDeath();
        }
    }

    private void UpdateVisual() {
        healthVisual.fillAmount = (health/ baseHealth);
        shieldVisual.fillAmount = (shield/ baseShield);
    }

    public void OnDeath() {
        GameManager.instance.LoseGame();
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
