using System;
using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Controlls camera movement
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        public float MaxOffset = 0.5F;
        private float shakeDeduction = 0.6F;
        private Vector3 basePosition;

        private bool isShaking = false;
        private float shakeForce;
        private float seed;
        private DateTime shakeStart;

        public void Start()
        {
            basePosition = transform.position;
        }

        public void Update()
        {
            if (!isShaking)
                return;

            float offsetX = MaxOffset * shakeForce * Mathf.PerlinNoise(seed + 0.1F, (float)(DateTime.Now - shakeStart).TotalMilliseconds);
            float offsetY = MaxOffset * shakeForce * Mathf.PerlinNoise(seed + 0.2F, (float)(DateTime.Now - shakeStart).TotalMilliseconds);

            transform.position = new Vector3(basePosition.x + offsetX, basePosition.y + offsetY, basePosition.z);
            shakeForce -= Time.deltaTime * shakeDeduction;
            if (shakeForce <= 0)
                isShaking = false;
        }

        /// <summary>
        /// Shakes camera
        /// </summary>
        /// <param name="force"></param>
        public void Shake(float force)
        {
            isShaking = true;
            shakeForce = force;
            seed = UnityEngine.Random.Range(0, 1);
            shakeStart = DateTime.Now;
        }
    }
}
