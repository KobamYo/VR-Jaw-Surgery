using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIGa
{
    public class ObservationManager : MonoBehaviour
    {
        public static ObservationManager Instance { get; private set; }

        [SerializeField] private float followRatio = 0.1f;
        [SerializeField] private Transform observationObject;

        [SerializeField] private Vector3 observationPosition = Vector3.zero;
        [SerializeField] private Quaternion observationRotation = Quaternion.identity;
        [SerializeField] private float observationScale = 1.0f;

        public Vector3 ObservationPosition { get => observationPosition; set => observationPosition = value; }
        public Quaternion ObservationRotation { get => observationRotation; set => observationRotation = value; }
        public float ObservationScale { get => observationScale; set => observationScale = value; }

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            observationObject.position = Vector3.Lerp(observationObject.position, ObservationPosition, followRatio);
            observationObject.rotation = Quaternion.Slerp(observationObject.rotation, ObservationRotation, followRatio);
            observationObject.localScale = Vector3.Lerp(observationObject.localScale, Vector3.one * ObservationScale, followRatio);
        }
    }
}