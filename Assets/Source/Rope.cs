using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source
{
    /// <summary>
    /// Rope
    /// </summary>
    public class Rope : MonoBehaviour
    {
        public Transform StartPoint;
        public Transform EndPoint;

        private LineRenderer lineRenderer;
        private readonly List<RopeSegment> ropeSegments = new List<RopeSegment>();
        public float RopeLength = 1.5F;
        private int segmentsCount = 50;
        private float segmentLength;
        private float lineWidth = 0.05f;
        
        public void Start()
        {
            segmentLength = RopeLength / segmentsCount;

            lineRenderer = GetComponent<LineRenderer>();
            Vector3 ropeStartPoint = StartPoint.position;

            for (int i = 0; i < segmentsCount; i++)
            {
                ropeSegments.Add(new RopeSegment(ropeStartPoint));
                ropeStartPoint.y -= segmentLength;
            }
        }

        public void Update()
        {
            DrawRope();
        }

        public void FixedUpdate()
        {
            Simulate();
        }

        /// <summary>
        /// Add gravity to rope and velocity to rope and recalc its segments positions
        /// </summary>
        private void Simulate()
        {
            Vector2 forceGravity = new Vector2(0f, -1f);

            for (int i = 1; i < segmentsCount; i++)
            {
                RopeSegment firstSegment = ropeSegments[i];
                Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
                firstSegment.posOld = firstSegment.posNow;
                firstSegment.posNow += velocity;
                firstSegment.posNow += forceGravity * Time.fixedDeltaTime;
                ropeSegments[i] = firstSegment;
            }
            
            for (int i = 0; i < 50; i++)
            {
                ApplyConstraint();
            }
        }

        /// <summary>
        /// Recalc segments potiosions according to their new position and constrains
        /// </summary>
        private void ApplyConstraint()
        {
            RopeSegment firstSegment = ropeSegments[0];
            firstSegment.posNow = StartPoint.position;
            ropeSegments[0] = firstSegment;
            
            RopeSegment endSegment = ropeSegments[ropeSegments.Count - 1];
            endSegment.posNow = EndPoint.position;
            ropeSegments[ropeSegments.Count - 1] = endSegment;

            for (int i = 0; i < segmentsCount - 1; i++)
            {
                RopeSegment firstSeg = ropeSegments[i];
                RopeSegment secondSeg = ropeSegments[i + 1];

                float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
                float error = Mathf.Abs(dist - segmentLength);
                Vector2 changeDir = Vector2.zero;

                if (dist > segmentLength)
                {
                    changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
                }
                else if (dist < segmentLength)
                {
                    changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
                }

                Vector2 changeAmount = changeDir * error;
                if (i != 0)
                {
                    firstSeg.posNow -= changeAmount * 0.5f;
                    ropeSegments[i] = firstSeg;
                    secondSeg.posNow += changeAmount * 0.5f;
                    ropeSegments[i + 1] = secondSeg;
                }
                else
                {
                    secondSeg.posNow += changeAmount;
                    ropeSegments[i + 1] = secondSeg;
                }
            }
        }

        /// <summary>
        /// Draw rope segment by segment
        /// </summary>
        private void DrawRope()
        {
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            Vector3[] ropePositions = new Vector3[segmentsCount];
            for (int i = 0; i < segmentsCount; i++)
            {
                ropePositions[i] = ropeSegments[i].posNow;
            }

            lineRenderer.positionCount = ropePositions.Length;
            lineRenderer.SetPositions(ropePositions);
        }

        private struct RopeSegment
        {
            public Vector2 posNow;
            public Vector2 posOld;

            public RopeSegment(Vector2 pos)
            {
                posNow = pos;
                posOld = pos;
            }
        }
    }
}
