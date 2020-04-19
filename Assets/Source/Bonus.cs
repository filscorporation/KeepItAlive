using System;
using UnityEngine;

namespace Assets.Source
{
    public enum BonusType
    {
        BaloonSizeDown,
        BaloonSizeUp,
        Armour,
        RopeLengthUp,
    }

    /// <summary>
    /// Power up and debuff boxes
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bonus : MonoBehaviour
    {
        public BonusType BonusType;
        public float Duration = 10F;

        public GameObject Effect = null;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            Player player;
            if ((player = collision.gameObject.GetComponent<Player>()) != null)
            {
                PickUp(player);

                if (Effect != null)
                    Destroy(Instantiate(Effect, transform.position, Quaternion.identity));
                Destroy(gameObject);
            }
        }

        public void PickUp(Player player)
        {
            player.AddBonus(new ActiveBonus(BonusType, Duration));
        }
    }
}
