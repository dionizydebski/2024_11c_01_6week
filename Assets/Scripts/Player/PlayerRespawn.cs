using UnityEngine;

namespace Player
{
    public class PlayerRespawn : MonoBehaviour
    {
       private Transform _currentCheckpoint;
       private PlayerHealth _playerHealth;

       private void Awake()
       {
           _playerHealth = GetComponent<PlayerHealth>();
       }

       private void CheckpointRespawn()
       {
            transform.position = _currentCheckpoint.position;
            _playerHealth.Respawn();

            //TODO: Move camera to the checkpoint
       }

       private void OnTriggerEnter2D(Collider2D other)
       {
           if (other.CompareTag("Checkpoint"))
           {
               _currentCheckpoint = other.transform;
               other.GetComponent<Collider2D>().enabled = false;
               Debug.Log("Checkpoint enabled");
               //TODO: Add animation or smth for checkpoint unlock
           }
       }

    }
}

