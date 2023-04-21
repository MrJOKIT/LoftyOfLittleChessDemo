using System;
using UnityEngine;
using System.Collections;

namespace Micosmo.SensorToolkit.Example
{
    public class CameraFollow : MonoBehaviour
    {
        public GameObject ToFollow;
        public Vector2 followOffset;
        public float speed = 3f;
        private Vector2 threshold;

        void Start()
        {
            threshold = CalculateThreshold();
        }

        private void FixedUpdate()
        {
            Vector2 follow = ToFollow.transform.position;
            float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
            float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

            Vector3 newPosition = transform.position;
            if (Mathf.Abs(xDifference) >= threshold.x)
            {
                newPosition.x = follow.x;
            }

            if (Mathf.Abs(yDifference) >= threshold.y)
            {
                newPosition.y = follow.y;
            }

            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        }

        private Vector3 CalculateThreshold()
        {
            Rect aspect = Camera.main.pixelRect;
            Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height,
                Camera.main.orthographicSize);
            t.x -= followOffset.x;
            t.y -= followOffset.y;
            return t;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            ;
            Vector2 border = CalculateThreshold();
            Gizmos.DrawWireCube(transform.position,new Vector3(border.x * 2,border.y * 2,1));
        }
    }
}