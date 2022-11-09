
using System.Collections.Generic;
using UnityEngine;

namespace MiniDini.Nodes
{
    /// <summary>
    /// <see cref="Node"/> that has a list of children.
    /// </summary>
    [System.Serializable]
    public class SelectNode : Node
    {
        public enum SelectionMode
		{
            Inside,
            Outside
		}

        public enum SelectionType
		{
            PointsOnly,
            PointsAndPrims,
            PrimsOnly
		}


        #region Overrides of Node

        // the idea here, is that we can select points within a sphere or outside it
        // we can also select any prim if a point of that prim is within the sphere (or outside if we set that as the mode)

        [SerializeField]
        public Vector3 point = Vector3.zero;
        [SerializeField]
        public float radius = 1.0f;

        [SerializeField]
        public SelectionMode selmode = SelectionMode.Inside;
        [SerializeField]
        public SelectionType seltype = SelectionType.PrimsOnly;




        public override string GetDescription() { return "Select incoming geometry"; }

        /// <summary>
        /// Get the geometry for this Node.
        /// </summary>
        /// <returns>A geometry object</returns>
        public override Geometry GetGeometry()
        {
            if (m_geometry == null)
            {
                Debug.Log("SelectNode:Geometry was null in GetGeometry, so creating");

                // create new geometry container if we don't have one from parent just so we return something
                if(m_geometry == null)
                    m_geometry = new Geometry();
            }

            m_geometry.Empty();

            // here is where we construct the geometry 
            List<Node> parents = GetParents();

            if (parents.Count > 0)
            {
                Geometry parent_geometry = parents[0].GetGeometry();
                // make a copy of first parents geometry (we should only have one parent!)
                m_geometry.Copy(parent_geometry);
				// todo: write the selection code below...
                if (selmode == SelectionMode.Inside)
                {
                    foreach (Point geompoint in m_geometry.points)
                    {
                        if ((geompoint.position - point).magnitude <= radius)
                        {
                            geompoint.selected = true;
                        }
                    }
                    foreach (Prim geomPrim in m_geometry.prims)
                    {
                        foreach (int primPoint in geomPrim.points)
                        {
                            if ((m_geometry.points[primPoint].position - point).magnitude <= radius)
                            {
                                geomPrim.selected = true;
                            }
                        }
                    }
                }
                else
                {
                    foreach (Point geompoint in m_geometry.points)
                    {
                        if ((geompoint.position - point).magnitude > radius)
                        {
                            geompoint.selected = true;
                        }
                    }
                    foreach (Prim geomPrim in m_geometry.prims)
                    {
                        geomPrim.selected = true;
                        foreach (int primPoint in geomPrim.points)
                        {
                            if ((m_geometry.points[primPoint].position - point).magnitude <= radius)
                            {
                                geomPrim.selected = false;
                            }
                        }
                    }
                }
            }


            return m_geometry;
        }


        #endregion
    }
}