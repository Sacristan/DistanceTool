using UnityEngine;
using System.Collections.Generic;

namespace Sacristan.DistanceTool.Runtime
{
    [ExecuteInEditMode]
    public class DistanceTool : MonoBehaviour
    {
        public string distanceToolName = "";
        public Color lineColor = Color.yellow;
        public bool initialized = false;
        public string initialName = "Distance Tool";
        public Vector3 startPoint = Vector3.zero;
        public Vector3 endPoint = Vector3.up;
        public float distance;
        public float gizmoRadius = 0.1f;
        public bool scaleToPixels = false;
        public int pixelPerUnit = 128;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = this.lineColor;
            Gizmos.DrawWireSphere(startPoint, gizmoRadius);
            Gizmos.DrawWireSphere(endPoint, gizmoRadius);
            Gizmos.DrawLine(startPoint, endPoint);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = this.lineColor;
            Gizmos.DrawWireSphere(startPoint, gizmoRadius);
            Gizmos.DrawWireSphere(endPoint, gizmoRadius);
            Gizmos.DrawLine(startPoint, endPoint);
        }
    }
}