using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Controlls player movement
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController : MonoBehaviour
    {
        public float JumpForce = 10F;
        public float Speed = 5F;

        public bool Freeze = false;
        private bool isInTheAir = false;
        private float jumpTimeout = 0.1F;
        private float jumpTimeoutTimer = 0F;

        private Rigidbody2D characterRigidbody;
        public Transform GroundedChecker;

        public void Awake()
        {
            characterRigidbody = GetComponent<Rigidbody2D>();
        }

        public void FixedUpdate()
        {
            jumpTimeoutTimer = Mathf.Max(0, jumpTimeoutTimer - Time.fixedDeltaTime);

            if (isInTheAir)
                StartCoroutine(DetectGround());

            if (Freeze)
                return;
            ProcessInput();
        }

        private void ProcessInput()
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-Speed * Time.fixedDeltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(Speed * Time.fixedDeltaTime, 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                Jump();
            }
        }

        private void Jump()
        {
            if (isInTheAir)
                return;

            if (Mathf.Abs(jumpTimeoutTimer) > Mathf.Epsilon)
                return;

            characterRigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            isInTheAir = true;
            // Don't let player jump multiple frames in a row
            jumpTimeoutTimer = jumpTimeout;
        }

        private IEnumerator DetectGround()
        {
            if (Physics2D.OverlapBoxAll(GroundedChecker.position, new Vector2(0.3F, 0.01F), 0).Length > 1)
            {
                // Waiting for next physics frame to let is recalculate our vertical velocity
                yield return new WaitForFixedUpdate();
                // Remove vertical velocity before jump to make it correct after fall and not let it stack
                characterRigidbody.velocity = new Vector2(characterRigidbody.velocity.x, 0F);
                isInTheAir = false;
            }
        }
    }
}
