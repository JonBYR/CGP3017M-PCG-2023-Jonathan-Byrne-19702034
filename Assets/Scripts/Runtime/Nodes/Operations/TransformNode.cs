
using System.Collections.Generic;
using UnityEngine;

namespace MiniDini.Nodes
{
    /// <summary>
    /// <see cref="Node"/> that transforms geometry
    /// </summary>
    [System.Serializable]
    public class TransformNode : Node
    {
        [SerializeField]
        public Vector3 translation = new Vector3(0, 0, 0);

        [SerializeField]
        public Vector3 rotation = new Vector3(0, 0, 0);

        [SerializeField]
        public Vector3 scale = new Vector3(1, 1, 1);



        #region Overrides of Node

        public override string GetDescription() { return "A node that transforms selected geometry"; }

        /// <summary>
        /// Get the geometry for this Node.
        /// </summary>
        /// <returns>A geometry object</returns>
        public override Geometry GetGeometry()
        {
            if (m_geometry == null)
            {
                Debug.Log("TransformNode:Geometry was null in GetGeometry, so creating");
                // create new geometry container
                m_geometry = new Geometry();
            }

            m_geometry.Empty();

            // here is where we construct the geometry 
            List<Node> parents = GetParents();

            if (parents.Count > 0)
            {
                Quaternion rotation = Quaternion.Euler(90, 0, 0);
                Geometry parent_geometry = parents[0].GetGeometry();
                m_geometry.Copy(parent_geometry);
                Prim triangle = m_geometry.prims[0];
                List<Point> trianglePoints = m_geometry.points;
                /*
                trianglePoints[0].position = (trianglePoints[0].position + new Vector3(100, 0, 0));
                trianglePoints[1].position = (trianglePoints[1].position + new Vector3(100, 0, 0));
                trianglePoints[2].position = (trianglePoints[2].position + new Vector3(100, 0, 0));
                */
                for (int i = 0; i < trianglePoints.Count; i++)
                {
                    trianglePoints[i].position = (rotation * trianglePoints[i].position) + new Vector3(100, 0, 0);
                    Debug.Log(trianglePoints[i].position);
                    //trianglePoints[i].position = trianglePoints[i].position + new Vector3(100, 0, 0);
                }
                
            }

            return m_geometry;
        }


        #endregion
    }
}