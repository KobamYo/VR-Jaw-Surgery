using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GIGa
{
    public class VRPivot : MonoBehaviour
    {
        [SerializeField] private float followRatio = 0.1f;
        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private Transform trackingPlayer;

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, positionOffset + trackingPlayer.position, followRatio);
            // transform.rotation = Quaternion.Euler(0, , 0);
        }
    }
}