using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Ballon
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Baloon : MonoBehaviour
    {
        public Player Player;
        public Color RopeColor = Color.black;

        public Rope Rope;
        private Rigidbody2D baloonRigidbody;
        private bool alive = true;

        private Animator baloonAnimator;
        private const string speedAnimatorParam = "Speed";
        private const string deadAnimatorParam = "Dead";

        public static int BaloonLayerMask;
        private const string baloonLayerName = "Baloon";

        private void Awake()
        {
            BaloonLayerMask = LayerMask.GetMask(baloonLayerName);

            baloonRigidbody = GetComponent<Rigidbody2D>();
            baloonAnimator = GetComponent<Animator>();
        }

        public void LateUpdate()
        {
            AnimateStretch();
        }

        private void AnimateStretch()
        {
            baloonAnimator.SetFloat(speedAnimatorParam, baloonRigidbody.velocity.magnitude);
        }

        /// <summary>
        /// Baloon was hit by enemy or projectile
        /// </summary>
        /// <param name="hit"></param>
        public void Hit(GameObject hit)
        {
            return;
            if (!alive)
                return;

            alive = false;
            baloonAnimator.SetTrigger(deadAnimatorParam);
            baloonRigidbody.gravityScale *= -1;
            
            GameManager.Instance.BaloonDead();
        }
    }
}
