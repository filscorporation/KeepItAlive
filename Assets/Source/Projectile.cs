using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Projectile
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        public float Speed = 10F;
        public GameObject Effect;
        public GameObject ExplosionEffect;

        public void Update()
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }

        public void FixedUpdate()
        {
            TryHit();
        }

        public void Initialize(Vector3 target)
        {
            float angleRad = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad;
            transform.rotation = Quaternion.Euler(0, 0, angleDeg);
        }

        public void TryHit()
        {
            Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);

            foreach (Collider2D collision in hits)
            {
                Baloon baloon;
                if ((baloon = collision.gameObject.GetComponent<Baloon>()) != null)
                {
                    baloon.Hit(gameObject);
                }

                // TODO: use layers
                if (collision.gameObject.GetComponent<Enemy>() != null)
                {
                    continue;
                }

                if (collision.gameObject.GetComponent<Projectile>() != null)
                {
                    continue;
                }

                if (Effect != null)
                    Destroy(Instantiate(Effect, transform.position, Quaternion.identity), 2F);
                if (ExplosionEffect != null)
                    Destroy(Instantiate(ExplosionEffect, transform.position, Quaternion.identity), 2F);
                Destroy(gameObject);

                return;
            }
        }
    }
}
