using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIGa
{
    public class Ruler : MonoBehaviour
    {
        [SerializeField] private ObservationManager observationManager;

        [SerializeField] private float length;
        [SerializeField] private float lengthScale = 1;

        [SerializeField] private RulerPoint point1;
        [SerializeField] private RulerPoint point2;

        public float Length { get => length; }

        private void Start()
        {
            if (observationManager == null)
            {
                observationManager = ObservationManager.Instance;
            }
        }

        private void Update()
        {
            length = Vector3.Distance(point1.transform.position, point2.transform.position) / observationManager.ObservationScale * lengthScale;
        }

        public void DeleteRuler()
        {
            Destroy(gameObject);
        }
    }
}