using UnityEngine;

namespace Player
{
    public class SwordPickup : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Animator otherAnimator = other.gameObject.GetComponent<Animator>();
            otherAnimator.Play("Idle with sword");
            otherAnimator.SetBool("withSword", true);
            //TODO:Open Text message with tutorial about sword usage
            //TODO: Unlock melee attacks
            Destroy(gameObject);
        }
    }
}
