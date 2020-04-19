using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Bee that flyes straight forward
    /// </summary>
    public class LinearBee : BaseBee
    {
        public float Speed = 6F;

        public new void Start()
        {
            base.Start();

            basePosition = transform.position;
            float angleRad = Mathf.Atan2(
                Player.Baloon.transform.position.y - transform.position.y,
                Player.Baloon.transform.position.x - transform.position.x);
            float angleDeg = (180 / Mathf.PI) * angleRad + 180;
            transform.rotation = Quaternion.Euler(0, 0, angleDeg);
            if (angleDeg > 90 && angleDeg < 270)
            {
                transform.localScale = new Vector3(1, -1, 1);
            }

            Destroy(gameObject, 15F);
        }

        protected override void Fly()
        {
            basePosition += -(Vector2)transform.right * Speed * Time.deltaTime;

            base.Fly();
        }
    }
}
