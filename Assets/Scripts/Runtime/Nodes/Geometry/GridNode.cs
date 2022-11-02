
using System.Collections.Generic;
using UnityEngine;

namespace MiniDini.Nodes
{
    /// <summary>
    /// <see cref="Node"/> that has a list of children.
    /// </summary>
    [System.Serializable]
    public class GridNode : Node
    {
        [SerializeField]
        public ConstructionPlane editplane = new ConstructionPlane();
        [SerializeField]
        public float width = 2.0f;
        [SerializeField]
        public float height = 2.0f;
        [SerializeField]
        public uint rows = 3;
        [SerializeField]
        public uint columns = 3;

        #region Overrides of Node

        public override string GetDescription() { return "A grid made of NxM quads"; }

        /// <summary>
        /// Get the geometry for this Node.
        /// </summary>
        /// <returns>A geometry object</returns>
        public override Geometry GetGeometry()
        {
            if (m_geometry == null)
            {
                Debug.Log("GridNode:Geometry was null in GetGeometry, so creating");
                // create new geometry container
                m_geometry = new Geometry();
            }

            m_geometry.Empty();
            Vector3 vec = editplane.point;
            float length = width / 3;
            float totalTravelled = 0;
            // here is where we construct the geometry for a grid

            List<int> indexs = new List<int>();
            //List<Prim> prims = new List<Prim>();

            for (int i = 0; i <= columns; i++)
            {
                for (int j = 0; j <= rows; j++)
                {
                    Point p = new();
                    p.position = vec;
                    int index = m_geometry.AddPoint(p);
                    indexs.Add(index);
                    vec += new Vector3(length, 0, 0);
                    totalTravelled += length;
                }
                vec -= new Vector3(totalTravelled, 0, 0);
                vec += new Vector3(0, -length, 0);
                totalTravelled = 0;
            }

            Prim[,] gridMatrix = new Prim[rows, columns];
            int numOfPoints = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    gridMatrix[i, j] = new Prim();
                }
            }
            gridMatrix[0, 0].points.Add(indexs[0]);
            gridMatrix[0, 0].points.Add(indexs[1]);
            gridMatrix[0, 0].points.Add(indexs[4]);

            gridMatrix[0, 0].points.Add(indexs[2]);
            gridMatrix[0, 0].points.Add(indexs[4]);
            gridMatrix[0, 0].points.Add(indexs[5]);

            gridMatrix[0, 0].points.Add(indexs[2]);
            gridMatrix[0, 0].points.Add(indexs[3]);
            gridMatrix[0, 0].points.Add(indexs[6]);
            gridMatrix[0, 0].points.Add(indexs[7]);

            prims[3].points.Add(indexs[4]);
            prims[3].points.Add(indexs[5]);
            prims[3].points.Add(indexs[9]);
            prims[3].points.Add(indexs[8]);

            prims[4].points.Add(indexs[5]);
            prims[4].points.Add(indexs[6]);
            prims[4].points.Add(indexs[10]);
            prims[4].points.Add(indexs[9]);

            prims[5].points.Add(indexs[6]);
            prims[5].points.Add(indexs[7]);
            prims[5].points.Add(indexs[11]);
            prims[5].points.Add(indexs[10]);

            prims[6].points.Add(indexs[8]);
            prims[6].points.Add(indexs[9]);
            prims[6].points.Add(indexs[13]);
            prims[6].points.Add(indexs[12]);

            prims[7].points.Add(indexs[9]);
            prims[7].points.Add(indexs[10]);
            prims[7].points.Add(indexs[14]);
            prims[7].points.Add(indexs[13]);

            prims[8].points.Add(indexs[10]);
            prims[8].points.Add(indexs[11]);
            prims[8].points.Add(indexs[15]);
            prims[8].points.Add(indexs[14]);

            return m_geometry;
        }


        #endregion
    }
}