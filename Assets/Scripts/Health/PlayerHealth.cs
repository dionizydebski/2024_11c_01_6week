using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    
    private SpriteRenderer spriteRend;
    private Animator anim;
    private bool dead;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            //Destroy(gameObject);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Deactivate all attached component classes
                if (GetComponentInParent<PlayerAttack>() != null)
                {
                    GetComponentInParent<PlayerAttack>().enabled = false;
                }

                if (GetComponent<BasicPlayerMovement>() != null)
                {
                    GetComponent<BasicPlayerMovement>().enabled = false;
                }

                dead = true;
                //Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
}
