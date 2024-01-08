using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    private Rigidbody2D _rb;

    public HealthBar healthBar;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void Heal(int healthRestored)
    {
        currentHealth += healthRestored;
        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        _rb.bodyType = RigidbodyType2D.Static;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            if (currentHealth == 0)
            {
                Die();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            Heal(maxHealth - currentHealth);
        }
    }
}
