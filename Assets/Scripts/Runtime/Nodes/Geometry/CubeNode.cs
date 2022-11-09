
using System.Collections.Generic;
using UnityEngine;

namespace MiniDini.Nodes
{
    /// <summary>
    /// <see cref="Node"/> that has a list of children.
    /// </summary>
    [System.Serializable]
    public class CubeNode : Node
    {

        #region Overrides of Node

        [SerializeField]
        protected ConstructionPlane editplane = new ConstructionPlane();
        [SerializeField]
        protected float size = 1.0f;
        [SerializeField]
        public Color colour = Color.red;

        public override string GetDescription() { return "A single cube"; }

        /// <summary>
        /// Get the geometry for this Node.
        /// </summary>
        /// <returns>A geometry object</returns>
        public override Geometry GetGeometry()
        {
            if (m_geometry == null)
            {
                Debug.Log("CubeNode:Geometry was null in GetGeometry, so creating");
                // create new geometry container
                m_geometry = new Geometry();
            }

            m_geometry.Empty();
            Point a = new Point();
            Point b = new Point();
            Point c = new Point();
            Point d = new Point();
            Point e = new Point();
            Point f = new Point();
            Point g = new Point();
            Point h = new Point();
            List<Prim> prims = new List<Prim>();
            for (int i = 0; i < 6; i++)
            {
                Prim p = new Prim();
                prims.Add(p);
            }
            a.position = editplane.up * size;
            b.position = editplane.left * size;
            c.position = editplane.down * size;
            d.position = editplane.right * size;
            e.position = editplane.up * size;
            f.position = editplane.left * size;
            g.position = editplane.down * size;
            h.position = editplane.right * size;
            int index1 = m_geometry.AddPoint(a);
            int index2 = m_geometry.AddPoint(b);
            int index3 = m_geometry.AddPoint(c);
            int index4 = m_geometry.AddPoint(d);
            int index5 = m_geometry.AddPoint(e);
            int index6 = m_geometry.AddPoint(f);
            int index7 = m_geometry.AddPoint(g);
            int index8 = m_geometry.AddPoint(h);
            for (int i = 0; i < prims.Count; i++)
            {
                if (i % 2 == 0)
                {
                    prims[i].points.Add(index1);
                    prims[i].points.Add(index2);
                    prims[i].points.Add(index3);
                    prims[i].points.Add(index4);
                }
                else
                {
                    prims[i].points.Add(index5);
                    prims[i].points.Add(index6);
                    prims[i].points.Add(index7);
                    prims[i].points.Add(index8);
                }
                m_geometry.AddPrim(prims[i]);
            }
            
            // here is where we construct the geometry for a cube


            return m_geometry;
        }

        #endregion
    }
}