using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Animations;
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

        private Animator baloonAnimator;
        private const string speedAnimatorParam = "Speed";

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
            Debug.Log("Dead(");
        }
    }
}
