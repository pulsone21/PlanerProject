using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class MeshGenerator
    {
        public static Mesh GenerateRoadMesh(Vector3[] positions, int roadWidth)
        {
            Vector3[] verts = new Vector3[positions.Length * 2];
            Vector2[] uvs = new Vector2[verts.Length];
            int numTris = 2 * (positions.Length - 1);
            int[] tris = new int[numTris * 3];
            int vertIndex = 0;
            int triIndex = 0;

            for (int i = 0; i < positions.Length; i++)
            {
                Vector3 forward = Vector2.zero;
                if (i < positions.Length - 1)
                {
                    forward += positions[(i + 1) % positions.Length] - positions[i];
                }
                if (i > 0)
                {
                    forward += positions[i] - positions[(i - 1 + positions.Length) % positions.Length];
                }

                forward.Normalize();
                Vector3 left = new Vector3(-forward.y, forward.x);

                verts[vertIndex] = positions[i] + left * roadWidth * .5f;
                verts[vertIndex + 1] = positions[i] - left * roadWidth * .5f;

                float completionPercent = i / (float)(positions.Length - 1);
                float v = 1 - Mathf.Abs(2 * completionPercent - 1);
                uvs[vertIndex] = new Vector2(0, v);
                uvs[vertIndex + 1] = new Vector2(1, v);

                if (i < positions.Length - 1)
                {
                    tris[triIndex] = vertIndex;
                    tris[triIndex + 1] = (vertIndex + 2) % verts.Length;
                    tris[triIndex + 2] = vertIndex + 1;

                    tris[triIndex + 3] = vertIndex + 1;
                    tris[triIndex + 4] = (vertIndex + 2) % verts.Length;
                    tris[triIndex + 5] = (vertIndex + 3) % verts.Length;
                }

                vertIndex += 2;
                triIndex += 6;
            }

            Mesh mesh = new Mesh();
            mesh.vertices = verts;
            mesh.triangles = tris;
            mesh.uv = uvs;

            return mesh;
        }

    }
}
