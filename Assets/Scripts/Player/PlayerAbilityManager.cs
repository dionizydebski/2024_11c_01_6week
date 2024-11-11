using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerAbilityManager : MonoBehaviour
    {
        [SerializeField] private int doubleJumpUnlockIndex;
        [SerializeField] private int meleeUnlockIndex;
        [SerializeField] private int jumpSlamUnlockIndex;
        [SerializeField] private int wallSlideUnlockIndex;
        [SerializeField] private int rangedUnlockIndex;
        private bool _hasSword;

        public bool DoubleJumpUnlocked()
        {
            return SceneManager.GetActiveScene().buildIndex >= doubleJumpUnlockIndex;
        }

        public bool MeleeUnlocked()
        {
            return SceneManager.GetActiveScene().buildIndex >= meleeUnlockIndex && GetHasSword();
        }

        public bool JumpSlamUnlocked()
        {
            return SceneManager.GetActiveScene().buildIndex >= jumpSlamUnlockIndex && GetHasSword();
        }

        public bool WallJumpUnlocked()
        {
            return SceneManager.GetActiveScene().buildIndex >= wallSlideUnlockIndex;
        }

        public bool RangedUnlocked()
        {
            return SceneManager.GetActiveScene().buildIndex >= rangedUnlockIndex && GetHasSword();
        }

        public void SetHasSword(bool value)
        {
            _hasSword = value;
        }

        public bool GetHasSword()
        {
            if (SceneManager.GetActiveScene().buildIndex > meleeUnlockIndex) return true;
            if (SceneManager.GetActiveScene().buildIndex == meleeUnlockIndex) return _hasSword;
            return false;
        }

    }
}
