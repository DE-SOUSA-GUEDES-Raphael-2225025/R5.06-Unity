using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] private float baseHealth = 20.0f;
    [SerializeField] private Image healthVisual;
    private float health;
    private UnityEvent healthChangeEvent = new UnityEvent();

    private void Start() {
        health = baseHealth;
        healthChangeEvent.AddListener(UpdateVisual);
        
    }

    public void Heal(float healValue) {
        health += healValue;
        healthChangeEvent.Invoke();
    }

    public void Damage(float damageValue) {
        health -= damageValue;
        healthChangeEvent.Invoke();
    }

    private void UpdateVisual() {
        healthVisual.fillAmount = (health/ baseHealth);
    }



}
