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
    [SerializeField] private AudioSource collectSoundEffect;
    [SerializeField] private AudioSource collisionSoundEffect;
    [SerializeField] private AudioSource deathSoundEffect;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void TakeDamage(int damage)
    {
        collisionSoundEffect.Play();
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
        deathSoundEffect.Play();
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
            
            //  Destroy objects like enemy projectiles on collision
            if (collision.gameObject.HasCustomTag("DestroyOnPlayerContact"))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            collectSoundEffect.Play();
            Heal(maxHealth - currentHealth);
        }
        //  Projectiles shouldn't have blocking collision, so I copied this code to OnTriggerEnter2D as well
        else
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //  Destroy objects like enemy projectiles on collision
            if (collision.gameObject.HasCustomTag("DestroyOnPlayerContact"))
            {
                Destroy(collision.gameObject);
            }

            TakeDamage(1);
            if (currentHealth == 0)
            {
                Die();
            }
        }
    }
}
