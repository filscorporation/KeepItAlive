using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Player
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        public CharacterController PlayerCharacterController { get; private set; }

        public Transform Hand;
        public Baloon Baloon;

        public void Start()
        {
            PlayerCharacterController = GetComponent<CharacterController>();
        }

        public void Dead()
        {
            PlayerCharacterController.Dead();
        }
    }
}
