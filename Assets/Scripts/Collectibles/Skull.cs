using System.Collections;
using System.Collections.Generic;
using Collectibles;
using Player;
using UnityEngine;

public class Skull : Collectible
{
    private Animator _animator;

    private void Awake()
    {
        //_animator = GetComponent<Animator>();
    }

    protected override void Collect()
    {
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.AddSkull();
            //_animator.SetBool("isPickedUp", true);
            Invoke(nameof(DestroyCoin), 0.5f);
        }
        else
        {
            Debug.LogError("PlayerInventory not found! Coin collection failed.");
        }
    }
        
    private void DestroyCoin()
    {
        Destroy(gameObject);
    } 
}
