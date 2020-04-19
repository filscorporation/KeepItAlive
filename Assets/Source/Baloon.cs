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
        public AudioClip PopAudioEffect;

        public Rope Rope;
        private Rigidbody2D baloonRigidbody;
        private bool alive = true;
        private bool isArmoured = false;

        private Animator baloonAnimator;
        private const string speedAnimatorParam = "Speed";
        private const string deadAnimatorParam = "Dead";
        private const string isArmouredAnimatorParam = "IsArmoured";

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

        public void ArmourUp()
        {
            isArmoured = true;
            baloonAnimator.SetBool(isArmouredAnimatorParam, true);
            baloonRigidbody.mass *= 10;
        }

        public void ToNormal()
        {
            isArmoured = false;
            baloonAnimator.SetBool(isArmouredAnimatorParam, false);
            baloonRigidbody.mass /= 10;
        }

        /// <summary>
        /// Baloon was hit by enemy or projectile
        /// </summary>
        /// <param name="hit"></param>
        public void Hit(GameObject hit)
        {
            if (!alive || isArmoured)
                return;

            GetComponent<AudioSource>().PlayOneShot(PopAudioEffect);
            alive = false;
            baloonAnimator.SetTrigger(deadAnimatorParam);
            baloonRigidbody.gravityScale *= -1;
            
            GameManager.Instance.BaloonDead();
        }
    }
}
