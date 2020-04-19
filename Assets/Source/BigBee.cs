using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    ///  Big bee
    /// </summary>
    public class BigBee : BaseBee
    {
        public GameObject BeeProjectile;
        public AudioClip ShootAudioEffect;

        public float Speed = 0.5F;
        private float minDistanceFrom;
        public float MinDistanceFrom = 3F;
        public float MinDistanceTo = 5F;
        public float AttackTimeout = 6F;
        private float attackTimeoutTimer = 0F;

        //private const string attackAnimatorParam = "Attack";

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
                GetComponent<AudioSource>().PlayOneShot(ShootAudioEffect);
                attackTimeoutTimer = AttackTimeout;
                for (int i = 0; i < 5; i++)
                {
                    //beeAnimator.SetTrigger(attackAnimatorParam);
                    Projectile p = Instantiate(BeeProjectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
                    p.Initialize(Player.Baloon.transform.position + new Vector3(0, i - 2, 0));
                }
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
