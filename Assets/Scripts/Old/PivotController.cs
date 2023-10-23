using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIGa.Old
{
    public class PivotController : MonoBehaviour
    {
        [SerializeField] private ObservationManager observationManager;
        [SerializeField] private Transform handPivot;

        [SerializeField] private bool isMoving;
        [SerializeField] private bool isRotating;
        [SerializeField] private bool isZooming;

        [SerializeField] private float stopToleranceDistance = -0.05f;

        [SerializeField] private float moveSensitivity;
        [SerializeField] private float rotationSensitivity;
        [SerializeField] private float zoomSensitivity;

        [SerializeField] private GameObject moveButton;
        [SerializeField] private GameObject rotateButton;
        [SerializeField] private GameObject zoomButton;
        [SerializeField] private GameObject resetButton;

        [SerializeField] private GameObject moveOnButton;
        [SerializeField] private GameObject moveOffButton;
        [SerializeField] private GameObject rotateOnButton;
        [SerializeField] private GameObject rotateOffButton;
        [SerializeField] private GameObject zoomOnButton;
        [SerializeField] private GameObject zoomOffButton;

        [SerializeField] private GameObject moveTextUI;
        [SerializeField] private GameObject rotateTextUI;
        [SerializeField] private GameObject zoomTextUI;

        private Vector3 initialHandPosition;

        private Vector3 startupObservationPosition;
        private Vector3 startupObservationRotation;
        private float startupObservationScale;

        private Vector3 initialObservationPosition;
        private Quaternion initialObservationRotation;
        private float initialObservationScale;

        private void Start()
        {
            startupObservationPosition = observationManager.ObservationPosition;
            startupObservationRotation = observationManager.ObservationRotation.eulerAngles;
            startupObservationScale = observationManager.ObservationScale;
        }

        private void Update()
        {
            if (isMoving)
            {
                Moving();
                CheckingStop(stopToleranceDistance * 10);
            }
            else if (isRotating)
            {
                Rotating();
                CheckingStop(stopToleranceDistance);
            }
            else if (isZooming)
            {
                Zooming();
                CheckingStop(stopToleranceDistance);
            }
        }

        private void Moving()
        {
            var vector = handPivot.position - initialHandPosition;
            var deltaValue = vector * moveSensitivity;
            observationManager.ObservationPosition = initialObservationPosition + new Vector3(deltaValue.x, deltaValue.y, deltaValue.z);
        }

        private void Rotating()
        {
            var vector = handPivot.position - initialHandPosition;
            var deltaValue = vector * rotationSensitivity;
            observationManager.ObservationRotation = Quaternion.Euler(initialObservationRotation.eulerAngles + Vector3.up * -deltaValue.x);
        }

        private void Zooming()
        {
            var vector = handPivot.position - initialHandPosition;
            var deltaValue = vector * zoomSensitivity;
            observationManager.ObservationScale = initialObservationScale + deltaValue.x;
        }

        private void CheckingStop(float tolerance)
        {
            var vector = handPivot.position - initialHandPosition;
            if (vector.z < tolerance)
            {
                StopMoving();
                StopRotating();
                StopZooming();
            }
        }

        public void SetIsMoving(bool enable)
        {
            if (enable && !isMoving)
            {
                initialHandPosition = handPivot.position;
                initialObservationPosition = observationManager.ObservationPosition;

                // Disable Unused Button
                moveButton.SetActive(false);
                rotateButton.SetActive(false);
                zoomButton.SetActive(false);
                resetButton.SetActive(false);

                moveTextUI.SetActive(true);
            }
            isMoving = enable;
        }

        public void SetIsRotating(bool enable)
        {
            if (enable && !isRotating)
            {
                initialHandPosition = handPivot.position;
                initialObservationRotation = observationManager.ObservationRotation;

                // Disable Unused Button
                moveButton.SetActive(false);
                rotateButton.SetActive(false);
                zoomButton.SetActive(false);
                resetButton.SetActive(false);

                rotateTextUI.SetActive(true);
            }
            isRotating = enable;
        }

        public void SetIsZooming(bool enable)
        {
            if (enable && !isZooming)
            {
                initialHandPosition = handPivot.position;
                initialObservationScale = observationManager.ObservationScale;

                // Disable Unused Button
                moveButton.SetActive(false);
                rotateButton.SetActive(false);
                zoomButton.SetActive(false);
                resetButton.SetActive(false);

                zoomTextUI.SetActive(true);
            }
            isZooming = enable;
        }

        public void StopMoving()
        {
            SetIsMoving(false);
            ResetButton();
            moveOnButton.SetActive(false);
            moveOffButton.SetActive(true);
        }

        public void StopRotating()
        {
            SetIsRotating(false);
            ResetButton();
            rotateOnButton.SetActive(false);
            rotateOffButton.SetActive(true);
        }

        public void StopZooming()
        {
            SetIsZooming(false);
            ResetButton();
            zoomOnButton.SetActive(false);
            zoomOffButton.SetActive(true);
        }

        private void ResetButton()
        {
            moveButton.SetActive(true);
            rotateButton.SetActive(true);
            zoomButton.SetActive(true);
            resetButton.SetActive(true);

            moveTextUI.SetActive(false);
            rotateTextUI.SetActive(false);
            zoomTextUI.SetActive(false);
        }

        public void ResetPivot()
        {
            observationManager.ObservationPosition = startupObservationPosition;
            observationManager.ObservationRotation = Quaternion.Euler(startupObservationRotation);
            observationManager.ObservationScale = startupObservationScale;

            ResetButton();
        }
    }
}