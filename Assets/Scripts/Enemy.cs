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

    private Animator animator; // Pour l'animation de mort
    private bool isDead = false; // Emp�cher les multiples ex�cutions de l'animation de mort

    public void Kill()
    {
        if (!isDead)
        {
            isDead = true;

            // V�rifie si l'Animator est attach�
            if (animator != null)
            {
                animator.SetBool("Death", true); // Active l'animation de mort
            }
            else
            {
                Debug.LogWarning("Animator not found on " + gameObject.name);
            }

            // Tu peux d�truire l'objet apr�s un d�lai si n�cessaire
            Destroy(gameObject, 2f); // 2 secondes pour laisser le temps � l'animation de jouer
        }
    }

    public void OnTakeDamage()
    {
        // Ici tu peux ajouter des effets ou animations de r�action aux d�g�ts
    }

    public void Damage(double value)
    {
        if (!isDead)
        {
            health -= value;
            OnHealthChangeEvent.Invoke();

            if (health <= 0)
            {
                Kill();
            }
        }
    }

    public void Heal(double value)
    {
        if (!isDead)
        {
            health += value;
            if (health > maxHealth) health = maxHealth;
            OnHealthChangeEvent.Invoke();
        }
    }

    public void UpdateHealthVisual()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)(health / maxHealth);
        }
    }

    void Start()
    {
        health = maxHealth;
        nameText.text = enemyName;

        animator = GetComponent<Animator>(); // R�cup�re l'Animator attach� au GameObject

        if (animator == null)
        {
            Debug.LogWarning("Animator component missing from " + gameObject.name);
        }

        OnHealthChangeEvent.AddListener(UpdateHealthVisual); // Abonne la m�thode � l'�v�nement de changement de vie
    }
}