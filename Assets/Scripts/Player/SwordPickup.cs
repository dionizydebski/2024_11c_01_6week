using UnityEngine;

namespace Player
{
    public class SwordPickup : MonoBehaviour
    {
        private static readonly int WithSword = Animator.StringToHash("withSword");

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Animator otherAnimator = other.gameObject.GetComponent<Animator>();
                otherAnimator.Play("Idle with sword");
                otherAnimator.SetBool(WithSword, true);
                other.gameObject.GetComponent<PlayerAbilityManager>().SetHasSword(true);
                Destroy(gameObject);
            }
        }
    }
}
