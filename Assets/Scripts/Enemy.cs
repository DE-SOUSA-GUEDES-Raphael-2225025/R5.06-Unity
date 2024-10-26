using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private string enemyName;
    [SerializeField] private double maxHealth;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image healthBar;

    private double health;
    private UnityEvent OnHealthChangeEvent = new UnityEvent();

    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        health = maxHealth;
        nameText.text = enemyName;

        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator component missing from " + gameObject.name);
        }
        else
        {
            animator.SetBool("Death", false); // Définit explicitement "Death" à false
            Debug.Log("Valeur initiale de Death dans l'Animator après réinitialisation : " + animator.GetBool("Death"));
        }

        OnHealthChangeEvent.AddListener(UpdateHealthVisual);
    }


    public void Damage(double value)
    {
        if (isDead) return; // Vérifie que l'ennemi est vivant avant d'infliger des dégâts

        health -= value;
        Debug.Log($"{enemyName} a pris {value} de dégâts, santé restante : {health}");
        OnHealthChangeEvent.Invoke();

        if (health <= 0)
        {
            Kill();
        }
    }

    public void Heal(double value)
    {
        if (isDead) return;

        health += value;
        if (health > maxHealth) health = maxHealth;
        Debug.Log($"{enemyName} a été soigné de {value}, santé actuelle : {health}");
        OnHealthChangeEvent.Invoke();
    }

    public void Kill()
    {
        if (isDead) return; // Empêche plusieurs exécutions de Kill si l'ennemi est déjà mort

        isDead = true;
        Debug.Log($"{enemyName} est mort.");

        if (animator != null)
        {
            animator.SetBool("Death", true); // Définit "Death" à true pour activer l'animation de mort
        }
        else
        {
            Debug.LogWarning("Animator not found on " + gameObject.name);
        }

        Destroy(gameObject, 2f); // Délai pour laisser le temps à l'animation de jouer
    }

    public void UpdateHealthVisual()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)(health / maxHealth);
        }
    }

    public void OnTakeDamage()
    {
        // Ici tu peux ajouter des effets ou animations de réaction aux dégâts
    }
}
