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
        private const string groundLayerName = "Ground";
        private const string enemiesLayerName = "Enemies";
        private int layerMask;

        public float JumpForce = 10F;
        public float Speed = 5F;
        public float MinSpeedToKill = 2F;

        public bool Freeze = false;
        private bool isInTheAir = false;
        private bool wasInTheAir = false;
        private float jumpTimeout = 0.1F;
        private float jumpTimeoutTimer = 0F;

        private Rigidbody2D characterRigidbody;
        public Transform GroundedChecker;

        private Animator playerAnimator;
        private const string isInAirAnimatorParam = "IsInAir";
        private const string landingAnimatorParam = "Landing";
        private const string deadAnimatorParam = "Dead";

        public void Awake()
        {
            playerAnimator = GetComponent<Animator>();
            characterRigidbody = GetComponent<Rigidbody2D>();
            layerMask = LayerMask.GetMask(groundLayerName, enemiesLayerName);
        }

        public void FixedUpdate()
        {
            jumpTimeoutTimer = Mathf.Max(0, jumpTimeoutTimer - Time.fixedDeltaTime);

            DetectGround();

            if (Freeze)
                return;
            ProcessInput();
        }

        private void ProcessInput()
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += new Vector3(-Speed * Time.fixedDeltaTime, 0);
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(Speed * Time.fixedDeltaTime, 0);
                transform.localScale = new Vector3(1, 1, 1);
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
            wasInTheAir = true;
            // Don't let player jump multiple frames in a row
            jumpTimeoutTimer = jumpTimeout;
        }

        private void DetectGround()
        {
            Collider2D[] hits;
            if ((hits = Physics2D.OverlapBoxAll(GroundedChecker.position, new Vector2(0.1F, 0.01F), 0, layerMask)).Any())
            {
                if (!wasInTheAir)
                    return;

                float force = characterRigidbody.velocity.y;
                // Remove vertical velocity before jump to make it correct after fall and not let it stack
                characterRigidbody.velocity = new Vector2(characterRigidbody.velocity.x, 0F);
                isInTheAir = false;
                wasInTheAir = false;
                playerAnimator.SetBool(isInAirAnimatorParam, false);
                playerAnimator.SetTrigger(landingAnimatorParam);

                Enemy enemy = hits.FirstOrDefault(h => h.GetComponent<Enemy>() != null)?.GetComponent<Enemy>();
                if (enemy != null)
                {
                    TryAttackOnLanding(enemy, force);
                }
            }
            else
            {
                if (wasInTheAir)
                    return;
                isInTheAir = true;
                wasInTheAir = true;
                playerAnimator.SetBool(isInAirAnimatorParam, true);
            }
        }

        private void TryAttackOnLanding(Enemy enemy, float force)
        {
            if (force < -MinSpeedToKill)
            {
                Jump();
                enemy.TryDie();
            }
        }

        public void AnimateDead()
        {
            playerAnimator.SetTrigger(deadAnimatorParam);
        }
    }
}
