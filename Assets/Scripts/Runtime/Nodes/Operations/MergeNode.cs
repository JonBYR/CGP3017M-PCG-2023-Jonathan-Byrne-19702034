
using System.Collections.Generic;
using UnityEngine;

namespace MiniDini.Nodes
{
    /// <summary>
    /// <see cref="Node"/> that has a list of children.
    /// </summary>
    [System.Serializable]
    public class MergeNode : Node
    {

        #region Overrides of Node

        public override string GetDescription() { return "A node that merges multiple geometries into one"; }


        /// <summary>
        /// Get the geometry for this Node.
        /// </summary>
        /// <returns>A geometry object</returns>
        public override Geometry GetGeometry()
        {
            if (m_geometry == null)
            {
                Debug.Log("MergeNode:Geometry was null in GetGeometry, so creating");
                // create new geometry container
                m_geometry = new Geometry();
            }

            m_geometry.Empty();

            // here is where we construct the geometry 
            List<Node> parents = GetParents();
            if (parents.Count > 0)
            {
                /*
                List<Geometry> parent_geometries = new List<Geometry>();
                for(int i = 0; i < parents.Count; i++)
                {
                    parent_geometries.Add(parents[i].GetGeometry());
                }
                for(int i = 0; i < parent_geometries.Count; i++)
                {
                    for (int j = 0; j < parent_geometries[i].points.Count; j++)
                    {
                        m_geometry.points.Add(parent_geometries[i].points[j]);
                    }
                    for (int j = 0; i < parent_geometries[i].prims.Count; j++)
                    {
                        m_geometry.prims.Add(parent_geometries[i].prims[j]);
                    }
                }
                */ //not sure why this doesn't work
                Debug.Log(m_geometry.points);
                Debug.Log(m_geometry.prims);
                Geometry parent_geometry_1 = parents[0].GetGeometry();
                Geometry parent_geometry_2 = parents[1].GetGeometry();
                if(parents.Count == 4)
                {
                    Geometry parent_geometry_3 = parents[2].GetGeometry();
                    Geometry parent_geometry_4 = parents[3].GetGeometry();
                    for (int i = 0; i < parent_geometry_3.points.Count; i++)
                    {
                        m_geometry.points.Add(parent_geometry_3.points[i]);
                    }
                    for (int i = 0; i < parent_geometry_3.prims.Count; i++)
                    {
                        m_geometry.prims.Add(parent_geometry_3.prims[i]);
                    }
                    for (int i = 0; i < parent_geometry_4.points.Count; i++)
                    {
                        m_geometry.points.Add(parent_geometry_4.points[i]);
                    }
                    for (int i = 0; i < parent_geometry_4.prims.Count; i++)
                    {
                        m_geometry.prims.Add(parent_geometry_4.prims[i]);
                    }
                }
                
                for(int i = 0; i < parent_geometry_1.points.Count; i++)
                {
                    m_geometry.points.Add(parent_geometry_1.points[i]);
                }
                for (int i = 0; i < parent_geometry_2.points.Count; i++)
                {
                    m_geometry.points.Add(parent_geometry_2.points[i]);
                }
                for(int i = 0; i < parent_geometry_1.prims.Count; i++)
                {
                    m_geometry.prims.Add(parent_geometry_1.prims[i]);
                }
                for (int i = 0; i < parent_geometry_2.prims.Count; i++)
                {
                    m_geometry.prims.Add(parent_geometry_2.prims[i]);
                }
            }

            return m_geometry;
        }



        #endregion
    }
}