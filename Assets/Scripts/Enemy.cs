using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private string enemyName;
    [SerializeField] private double maxHealth;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image damageBar;
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private GameObject damageParticleEffect; // Syst�me de particules pour les d�g�ts
    [SerializeField] private Transform player;
    
    private NavMeshAgent agent;
    private double health;
    private UnityEvent OnHealthChangeEvent = new UnityEvent();
    private float timeWithoutDamage = 0f;

    private Animator animator;
    private bool isDead = false;

    void Start()
    {
        health = maxHealth;
        nameText.text = enemyName;
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogWarning("Animator component missing from " + gameObject.name);
        }
        else
        {
            animator.SetBool("Death", false); // D�finit explicitement "Death" � false
            Debug.Log("Valeur initiale de Death dans l'Animator apr�s r�initialisation : " + animator.GetBool("Death"));
        }

        OnHealthChangeEvent.AddListener(UpdateHealthVisual);
    }

    public void Damage(double value)
    {
        if (isDead) return; // V�rifie que l'ennemi est vivant avant d'infliger des d�g�ts

        health -= value;

        // Affiche le texte flottant des d�g�ts

        timeWithoutDamage = 0;
        Vector3 randomOffset = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(0.0f, 1.0f), 0);
        GameObject floatingText = Instantiate(floatingTextPrefab, transform.position + new Vector3(0, 3.5f, 0) + randomOffset, transform.rotation);
        floatingText.GetComponent<TMP_Text>().text = value.ToString();
        Destroy(floatingText, 2);

        // Instancie les particules de d�g�ts
        if (damageParticleEffect != null)
        {
            GameObject damageParticle = Instantiate(damageParticleEffect, transform.position + randomOffset, Quaternion.identity);
            Destroy(damageParticle, 2);
        }

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
        Debug.Log($"{enemyName} a �t� soign� de {value}, sant� actuelle : {health}");
        OnHealthChangeEvent.Invoke();
    }

    public void Kill()
    {
        if (isDead) return; 

        isDead = true;
        Debug.Log($"{enemyName} est mort.");

        if (GetComponent<LootDrop>() != null) {
            GetComponent<LootDrop>().Drop();
        }

        if (animator != null)
        {
            animator.SetBool("Death", true); // D�finit "Death" � true pour activer l'animation de mort
        }
        else
        {
            Debug.LogWarning("Animator not found on " + gameObject.name);
        }

        Destroy(gameObject, 2f); // D�lai pour laisser le temps � l'animation de jouer
    }

    public void UpdateHealthVisual()
    {
        
        if (healthBar != null)
        {
            healthBar.fillAmount = (float)(health / maxHealth);
        }
    }

    public void Update() {
        if (timeWithoutDamage < 1) timeWithoutDamage += Time.deltaTime;

        if (damageBar.fillAmount > healthBar.fillAmount && timeWithoutDamage >= 1) {
            damageBar.fillAmount -= 0.01f;
        }

        agent.destination = player.position;
    }

    public void OnTakeDamage()
    {
        // Ici tu peux ajouter des effets ou animations de r�action aux d�g�ts
    }
}
