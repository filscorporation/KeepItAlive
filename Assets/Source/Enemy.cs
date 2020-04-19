using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Enemy
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        public Player Player;
        public bool DeadFromPlayerJump = true;
        public GameObject OnDeathEffect;
        public int Cost = 10;
        public float PowerUpChance = 0.2F;

        private bool leftRotation = true;

        public void Start()
        {
            Player = FindObjectOfType<Player>();
        }

        protected void RotateToPlayer()
        {
            if (leftRotation && Player.transform.position.x > transform.position.x)
            {
                leftRotation = false;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (!leftRotation && Player.transform.position.x < transform.position.x)
            {
                leftRotation = true;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            Baloon baloon;
            if ((baloon = collision.gameObject.GetComponent<Baloon>()) != null)
            {
                baloon.Hit(gameObject);
            }
        }

        public void TryDie()
        {
            if (!DeadFromPlayerJump)
                return;

            Die();
        }

        public void Die(bool spawnPowerUp = true)
        {
            GameManager.Instance.AddScore(Cost);
            if (spawnPowerUp)
                SpawnManager.Instance.SpawnPowerUp(PowerUpChance);

            if (OnDeathEffect != null)
                Destroy(Instantiate(OnDeathEffect, transform.position, Quaternion.identity), 5F);
            Destroy(gameObject);
        }
    }
}
