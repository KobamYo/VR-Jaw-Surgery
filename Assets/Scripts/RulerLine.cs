using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIGa
{
    public class RulerLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private RulerPoint point1;
        [SerializeField] private RulerPoint point2;
        [SerializeField] private float lineWidth = 0.01f;

        private void Update()
        {
            lineRenderer.SetPosition(0, point1.transform.position);
            lineRenderer.SetPosition(1, point2.transform.position);

            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
        }
    }
}