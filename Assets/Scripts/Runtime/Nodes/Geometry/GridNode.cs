
using System.Collections.Generic;
using System;
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
        [Range(0.1f, 600.0f)]
        public float width = 2.0f;
        [SerializeField]
        [Range(0.1f, 600.0f)]
        public float height = 2.0f;
        [SerializeField]
        [Range(1, 600)]
        public uint rows = 3;
        [SerializeField]
        [Range(1, 600)]
        public uint columns = 3;
        [SerializeField]
        public Color colour = Color.blue;


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
            gridMatrix[0, 0].points.Add(indexs[5]);
            gridMatrix[0, 0].points.Add(indexs[4]);

            gridMatrix[0, 1].points.Add(indexs[1]);
            gridMatrix[0, 1].points.Add(indexs[2]);
            gridMatrix[0, 1].points.Add(indexs[6]);
            gridMatrix[0, 1].points.Add(indexs[5]);

            gridMatrix[0, 2].points.Add(indexs[2]);
            gridMatrix[0, 2].points.Add(indexs[3]);
            gridMatrix[0, 2].points.Add(indexs[7]);
            gridMatrix[0, 2].points.Add(indexs[6]);

            gridMatrix[1, 0].points.Add(indexs[4]);
            gridMatrix[1, 0].points.Add(indexs[5]);
            gridMatrix[1, 0].points.Add(indexs[9]);
            gridMatrix[1, 0].points.Add(indexs[8]);

            gridMatrix[1, 1].points.Add(indexs[5]);
            gridMatrix[1, 1].points.Add(indexs[6]);
            gridMatrix[1, 1].points.Add(indexs[10]);
            gridMatrix[1, 1].points.Add(indexs[9]);

            gridMatrix[1, 2].points.Add(indexs[6]);
            gridMatrix[1, 2].points.Add(indexs[7]);
            gridMatrix[1, 2].points.Add(indexs[11]);
            gridMatrix[1, 2].points.Add(indexs[10]);

            gridMatrix[2, 0].points.Add(indexs[8]);
            gridMatrix[2, 0].points.Add(indexs[9]);
            gridMatrix[2, 0].points.Add(indexs[13]);
            gridMatrix[2, 0].points.Add(indexs[12]);

            gridMatrix[2, 1].points.Add(indexs[9]);
            gridMatrix[2, 1].points.Add(indexs[10]);
            gridMatrix[2, 1].points.Add(indexs[14]);
            gridMatrix[2, 1].points.Add(indexs[13]);

            gridMatrix[2, 2].points.Add(indexs[10]);
            gridMatrix[2, 2].points.Add(indexs[11]);
            gridMatrix[2, 2].points.Add(indexs[15]);
            gridMatrix[2, 2].points.Add(indexs[14]);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    m_geometry.AddPrim(gridMatrix[i, j]);
                }
            }
            return m_geometry;
        }


        #endregion
    }
}