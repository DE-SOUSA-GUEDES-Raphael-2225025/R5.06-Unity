using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private string name;
    [SerializeField] private double maxHealth;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image healthBar;

    private double health;
    private UnityEvent OnHealthChangeEvent = new UnityEvent();



    public void Kill() {
        Destroy(gameObject);
    }

    public void OnTakeDamage() {
        throw new System.NotImplementedException();
    }

    public void Damage(double value) {
        health = health - value;
        OnHealthChangeEvent.Invoke();

        if (health <= 0) Kill();
    }

    public void Heal(double value) {
        health = health + value;
        if (health > maxHealth) health = maxHealth;
        OnHealthChangeEvent.Invoke();
    }

    public void UpdateHealthVisual() {
        healthBar.fillAmount = (float) (health / maxHealth);
    }

    void Start()
    {
        health = maxHealth;
        nameText.text = name;

        OnHealthChangeEvent.AddListener(UpdateHealthVisual);
    }

    
}
