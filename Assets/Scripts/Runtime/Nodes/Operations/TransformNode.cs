
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
                Vector3 translation = new Vector3(100, 0, 0);
                Geometry parent_geometry = parents[0].GetGeometry();
                m_geometry.Copy(parent_geometry);
                Prim triangle = m_geometry.prims[0];
                List<Point> trianglePoints = m_geometry.points;
                for (int i = 0; i < trianglePoints.Count; i++)
                {
                    trianglePoints[i].position = Quaternion.Euler(rotation) * trianglePoints[i].position;
                    trianglePoints[i].position = trianglePoints[i].position + translation;
                    trianglePoints[i].position = Vector3.Scale(trianglePoints[i].position, scale);
                }
                
            }

            return m_geometry;
        }


        #endregion
    }
}