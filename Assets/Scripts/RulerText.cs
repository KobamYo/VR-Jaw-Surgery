using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GIGa
{
    public class RulerText : MonoBehaviour
    {
        [SerializeField] private TextMeshPro text;
        [SerializeField] private Ruler ruler;

        [SerializeField] private RulerPoint point1;
        [SerializeField] private RulerPoint point2;

        private void LateUpdate()
        {
            text.text = ruler.Length.ToString("0.000") + " cm";
            transform.position = Vector3.Lerp(point1.transform.position, point2.transform.position, 0.5f);
            transform.LookAt(Camera.main.transform.position);
        }
    }
}