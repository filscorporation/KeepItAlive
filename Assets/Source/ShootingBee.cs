using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Source
{
    /// <summary>
    /// Bee that shoots
    /// </summary>
    public class ShootingBee : BaseBee
    {
        public GameObject BeeProjectile;
        
        public float Speed = 1F;
        private float minDistanceFrom;
        public float MinDistanceFrom = 5F;
        public float MinDistanceTo = 5F;
        public float AttackTimeout = 4F;
        private float attackTimeoutTimer = 0F;
        
        private const string attackAnimatorParam = "Attack";

        public new void Start()
        {
            base.Start();

            basePosition = transform.position;
            AttackTimeout += Random.Range(0, 1F);
            minDistanceFrom = Random.Range(MinDistanceFrom, MinDistanceTo);
            attackTimeoutTimer = AttackTimeout;
        }

        public new void Update()
        {
            base.Update();

            attackTimeoutTimer = Mathf.Max(0, attackTimeoutTimer - Time.deltaTime);

            TryAttack();
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

        protected override void Fly()
        {
            if (Vector2.Distance(Player.transform.position, basePosition) > minDistanceFrom)
            {
                basePosition = Vector2.MoveTowards(basePosition, Player.transform.position, Speed * Time.deltaTime);
            }

            base.Fly();

            RotateToPlayer();
        }
    }
}
