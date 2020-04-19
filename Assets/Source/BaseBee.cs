using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Source
{
    /// <summary>
    /// Base bee methods
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class BaseBee : Enemy
    {
        public bool Freeze = false;

        protected Vector2 basePosition;
        public float FlyAmplitude = 0.5F;
        private float flyOffset;

        protected Animator beeAnimator;

        public new void Start()
        {
            base.Start();

            basePosition = transform.position;
            flyOffset = Random.Range(0, 1F);
            beeAnimator = GetComponent<Animator>();
        }

        public void Update()
        {
            if (!Freeze)
                Fly();
        }

        protected virtual void Fly()
        {
            transform.position = new Vector2(
                basePosition.x,
                basePosition.y + Mathf.Sin((DateTime.Now.Millisecond / 1000F + flyOffset) * 2 * Mathf.PI) * FlyAmplitude);
        }
    }
}
