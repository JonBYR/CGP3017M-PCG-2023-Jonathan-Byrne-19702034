
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
            List<Point> points = new List<Point>();
            List<int> indexs = new List<int>();
            int pos = 0;
            for (int i = 0; i < 16; i++)
            {
                Point p = new();
                points.Add(p);
                indexs.Add(0);
                if (pos % 4 == 0) points[i].position = editplane.up * width;
                else if (pos % 4 == 1) points[i].position = editplane.right * width;
                else if (pos % 4 == 2) points[i].position = editplane.down * width;
                else if (pos % 4 == 3) points[i].position = editplane.left * width;
                pos++;
            }
            for (int i = 0; i < 16; i++)
            {
                indexs[i] = m_geometry.AddPoint(points[i]);
            }
            Prim[,] gridMatrix = new Prim[rows, columns];
            int numOfPoints = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    gridMatrix[i, j] = new Prim();
                    for (int k = numOfPoints; k < numOfPoints + 4; k++)
                    {
                        gridMatrix[i, j].points.Add(indexs[k]);
                    }
                    m_geometry.AddPrim(gridMatrix[i, j]);
                    numOfPoints = numOfPoints + 4;
                    if (numOfPoints == 16) numOfPoints = 0;
                }
            }
            // here is where we construct the geometry for a grid

            return m_geometry;
        }


        #endregion
    }
}