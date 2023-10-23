using GIGa;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    [SerializeField] private ObservationManager observationManager;
    [SerializeField] private Transform cursorPivotLeft;
    [SerializeField] private Transform cursorPivotRight;
    [SerializeField] private Transform cursorPivotCenter;

    [SerializeField] private LineRenderer pivotConnector;

    [SerializeField] private float moveSensitivity;
    [SerializeField] private float zoomSensitivity;

    [SerializeField] private bool isPivoting;

    [SerializeField] private Vector3 initialCursorPivotLeftPosition;
    [SerializeField] private Vector3 initialCursorPivotRightPosition;
    [SerializeField] private Vector3 initialCursorPivotCenterPosition;
    [SerializeField] private Quaternion initialCursorPivotCenterRotation;

    private Vector3 startupObservationPosition;
    private Quaternion startupObservationRotation;
    private float startupObservationScale;

    private Vector3 initialObservationPosition;
    private Quaternion initialObservationRotation;
    private float initialObservationScale;

    private void Start()
    {
        startupObservationPosition = observationManager.ObservationPosition;
        startupObservationRotation = observationManager.ObservationRotation;
        startupObservationScale = observationManager.ObservationScale;

        cursorPivotLeft.position = initialCursorPivotLeftPosition;
        cursorPivotRight.position = initialCursorPivotRightPosition;
    }

    private void Update()
    {
        UpdateCursorPivotCenter();

        if (isPivoting)
        {
            Moving();
            Rotating();
            Zooming();

            pivotConnector.enabled = true;
        } else
        {
            pivotConnector.enabled = false;
        }

        pivotConnector.SetPosition(0, cursorPivotLeft.position);
        pivotConnector.SetPosition(1, cursorPivotRight.position);
    }

    public void InititatePivot()
    {
        initialObservationPosition = observationManager.ObservationPosition;
        initialObservationRotation = observationManager.ObservationRotation;
        initialObservationScale = observationManager.ObservationScale;

        cursorPivotLeft.position = initialCursorPivotLeftPosition;
        cursorPivotRight.position = initialCursorPivotRightPosition;

        initialCursorPivotCenterPosition = (initialCursorPivotLeftPosition + initialCursorPivotRightPosition) / 2;
        
        cursorPivotCenter.LookAt(cursorPivotRight);
        initialCursorPivotCenterRotation = cursorPivotCenter.rotation;
    }

    public void EndPivot()
    {
        cursorPivotLeft.position = initialCursorPivotLeftPosition;
        cursorPivotRight.position = initialCursorPivotRightPosition;
    }

    private void UpdateCursorPivotCenter()
    {
        cursorPivotCenter.position = (cursorPivotLeft.position + cursorPivotRight.position) / 2;
        cursorPivotCenter.LookAt(cursorPivotRight);
    }

    private void Moving()
    {
        var delta = (cursorPivotCenter.position - initialCursorPivotCenterPosition) * moveSensitivity;
        observationManager.ObservationPosition = initialObservationPosition + delta;
    }

    private void Rotating()
    {
        var diff = Quaternion.Euler(cursorPivotCenter.rotation.eulerAngles) * Quaternion.Inverse(Quaternion.Euler(initialCursorPivotCenterRotation.eulerAngles));
        observationManager.ObservationRotation = diff;
    }

    private void Zooming()
    {
        var delta = (Vector3.Distance(cursorPivotRight.position, cursorPivotLeft.position) - Vector3.Distance(initialCursorPivotRightPosition, initialCursorPivotLeftPosition)) * zoomSensitivity;
        observationManager.ObservationScale = initialObservationScale + delta;
    }

    public void CheckEnablePivot()
    {
        if (isPivoting == false)
        {
            InititatePivot();
        }
        isPivoting = true;
    }

    public void CheckDisablePivot()
    {
        if (isPivoting == true)
        {
            EndPivot();
        }
        isPivoting = false;
    }
}
