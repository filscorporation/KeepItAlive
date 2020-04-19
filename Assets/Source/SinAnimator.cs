using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Source
{
    /// <summary>
    /// Animates bees in main menu
    /// </summary>
    public class SinAnimator : MonoBehaviour
    {
        private Vector2 basePosition;
        public float FlyAmplitude = 0.5F;
        private float flyOffset;

        public void Start()
        {
            basePosition = transform.position;
            flyOffset = Random.Range(0, 1F);
        }

        public void Update()
        {
            Fly();
        }

        private void Fly()
        {
            transform.position = new Vector2(
                basePosition.x,
                basePosition.y + Mathf.Sin((DateTime.Now.Millisecond / 1000F + flyOffset) * 2 * Mathf.PI) * FlyAmplitude);
        }
    }
}
