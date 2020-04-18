using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Hedgehog
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hedgehog : Enemy
    {
        public float Speed = 3F;
        private int direction;
        private Rigidbody2D hedgehogRigidbody;

        public new void Start()
        {
            base.Start();

            direction = transform.position.x > 0 ? -1 : 1;
            hedgehogRigidbody = GetComponent<Rigidbody2D>();
        }

        public void FixedUpdate()
        {
            hedgehogRigidbody.velocity = new Vector2(Speed * direction * Time.fixedDeltaTime, hedgehogRigidbody.velocity.y);
        }
    }
}
