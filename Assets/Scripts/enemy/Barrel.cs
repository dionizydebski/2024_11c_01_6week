using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public Transform posA; // Punkt startowy
    public Transform posB; // Punkt końcowy
    public float speed; // Prędkość platformy
    
    private float collapseDelay = 1f;
    private float respawnTime = 3f;
    
    private Quaternion initialRotation;
    
    private bool isCollapsing = false;
    
    private Collider2D platformCollider;
    private Renderer platformRenderer;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        initialRotation = transform.rotation;
        
        platformCollider = GetComponent<Collider2D>();
        platformRenderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, posB.position) < 0.1f)
        {
            StartCoroutine(Collapse());
        }
        
        transform.position = Vector3.MoveTowards(transform.position, posB.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
        }
    }
    
    private IEnumerator Collapse()
    {
        yield return new WaitForSeconds(collapseDelay);

        // Wyłącz platformę (ukryj i dezaktywuj collider)
        platformRenderer.enabled = false;
        platformCollider.enabled = false;
            
        yield return new WaitForSeconds(respawnTime);
            
        RespawnPlatform();
    }
        
    private void RespawnPlatform()
    {
        transform.position = posA.position;
        transform.rotation = initialRotation;
            
        platformRenderer.enabled = true;
        platformCollider.enabled = true;

        isCollapsing = false;
    }
}
