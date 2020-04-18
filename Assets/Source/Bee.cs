using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Animations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Source
{
    /// <summary>
    /// Bee
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class Bee : Enemy
    {
        public bool Freeze = false;
        public GameObject BeeProjectile;

        private Vector2 basePosition;
        private const float flyAmplitude = 0.5F;
        public float Speed = 1F;
        public float MinDistance = 5F;
        public float AttackTimeout = 4F;
        private float attackTimeoutTimer = 0F;
        private float flyOffset;

        private Animator beeAnimator;
        private const string attackAnimatorParam = "Attack";

        public new void Start()
        {
            base.Start();

            basePosition = transform.position;
            flyOffset = Random.Range(0, 1F);
            AttackTimeout += Random.Range(0, 1F);
            MinDistance += Random.Range(-1F, 1F);
            attackTimeoutTimer = AttackTimeout;
            beeAnimator = GetComponent<Animator>();
        }

        public void Update()
        {
            attackTimeoutTimer = Mathf.Max(0, attackTimeoutTimer - Time.deltaTime);

            TryAttack();

            if (!Freeze)
                Fly();
        }

        private void TryAttack()
        {
            if (Mathf.Abs(attackTimeoutTimer) < Mathf.Epsilon)
            {
                beeAnimator.SetTrigger(attackAnimatorParam);
                attackTimeoutTimer = AttackTimeout;
                Projectile p = Instantiate(BeeProjectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
                p.Initialize(Player.Baloon.transform.position);
            }
        }

        private void Fly()
        {
            if (Vector2.Distance(Player.transform.position, basePosition) > MinDistance)
            {
                basePosition = Vector2.MoveTowards(basePosition, Player.transform.position, Speed * Time.deltaTime);
            }

            transform.position = new Vector2(
                basePosition.x,
                basePosition.y + Mathf.Sin((DateTime.Now.Millisecond / 1000F + flyOffset) * 2 * Mathf.PI) * flyAmplitude);

            RotateToPlayer();
        }
    }
}
