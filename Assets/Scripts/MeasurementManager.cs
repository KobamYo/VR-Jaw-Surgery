using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIGa
{
    public class MeasurementManager : MonoBehaviour
    {
        [SerializeField] GameObject rulerPrefab;

        [SerializeField] Transform rulerParent;

        public void AddRuler()
        {
            GameObject ne = Instantiate(rulerPrefab, Camera.main.transform.position + Vector3.forward * 0.5f, Quaternion.identity, rulerParent);
        }
    }
}