using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Pirate_Ship
{
    public class PlayerLockPoint : MonoBehaviour
    {
        private static readonly int Running = Animator.StringToHash("run");
        private static readonly int ShipLock = Animator.StringToHash("shipLock");
        private PirateShipMovement _pirateShipMovement;
        private bool _locked;
        private void Awake()
        {
            _pirateShipMovement = gameObject.transform.parent.gameObject.GetComponent<PirateShipMovement>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && !_locked)
            {
                collision.transform.SetParent(transform.parent);
                DisablePlayerMovement(collision.gameObject);
                StartCoroutine(Wait(1f));
                _locked = true;
            }
        }

        private void DisablePlayerMovement(GameObject player)
        {
            if (player != null)
            {
                player.GetComponent<BasicPlayerMovement>().StopMovement();
                player.GetComponent<Animator>().SetBool(Running, false);
                player.GetComponent<Animator>().SetBool(ShipLock, true);
                if (player.GetComponent<BasicPlayerMovement>() != null) player.GetComponent<BasicPlayerMovement>().enabled = false;
                if (player.GetComponent<PlayerAttack>() != null) player.GetComponent<PlayerAttack>().enabled = false;
            }
        }

        private IEnumerator Wait(float time)
        {
            yield return new WaitForSeconds(time);
            _pirateShipMovement.SetAnimatorBool("Wind", true);
            _pirateShipMovement.SetAnimatorBool("Sail", true);
            yield return new WaitForSeconds(time/2);
            _pirateShipMovement.SetMove(true);
        }
    }
}
