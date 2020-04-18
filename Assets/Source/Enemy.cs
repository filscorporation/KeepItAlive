using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Enemy
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        public Player Player;

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
    }
}
