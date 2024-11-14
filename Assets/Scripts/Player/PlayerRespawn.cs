using LevelMenager;
using UnityEngine;

namespace Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        private static readonly int Rise = Animator.StringToHash("Rise");
        private Transform _currentCheckpoint;
        private PlayerHealth _playerHealth;

        private AudioManager _audioManager;

        private void Awake()
        {
           _playerHealth = GetComponent<PlayerHealth>();
           _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }

        public void CheckpointRespawn()
        {
            Debug.Log("Checkpoint Respawn");
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
                SimpleSaveSystem.SaveXML();
                _audioManager.PlaySFX(_audioManager.checkpoint);
                other.gameObject.GetComponent<Animator>().SetTrigger(Rise);
                //TODO: Add animation or smth for checkpoint unlock
            }
        }

    }
}

