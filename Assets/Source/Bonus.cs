using System;
using UnityEngine;

namespace Assets.Source
{
    public enum BonusType
    {
        BaloonSizeDown,
        BaloonSizeUp,
        Armour,
        Bomb,
    }

    /// <summary>
    /// Power up and debuff boxes
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bonus : MonoBehaviour
    {
        public BonusType BonusType;
        public float Duration = 10F;
        public float Lifetime = 8F;
        private float lifetimeTimer = 0;

        public GameObject Effect = null;
        private SpriteRenderer thisSpriteRenderer;

        public void Start()
        {
            thisSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Update()
        {
            lifetimeTimer += Time.deltaTime;
            float a = (Lifetime - lifetimeTimer) / Lifetime * 5;
            a = a > 1 ? 1 : a;
            thisSpriteRenderer.color = new Color(1, 1, 1, a);
            if (lifetimeTimer > Lifetime)
                Destroy(gameObject);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            Player player;
            if ((player = collision.gameObject.GetComponent<Player>()) != null)
            {
                PickUp(player);

                if (Effect != null)
                    Destroy(Instantiate(Effect, transform.position, Quaternion.identity), 3F);
                Destroy(gameObject);
            }
        }

        public void PickUp(Player player)
        {
            player.AddBonus(new ActiveBonus(BonusType, Duration));
        }
    }
}
