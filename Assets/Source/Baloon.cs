using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Ballon
    /// </summary>
    public class Baloon : MonoBehaviour
    {
        public Player Player;
        public Color RopeColor = Color.black;

        private Rope Rope;

        private void Start()
        {
        }

        public void LateUpdate()
        {
        }
    }
}
