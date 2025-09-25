using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float currentHealth;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < currentHealth)
        {
            currentHealth = health;
            anim.SetTrigger("Attacked");
        }

        if(health <= 0)
        {
            anim.SetBool("isDead", true);
            Destroy(gameObject);
        }
    }
}
