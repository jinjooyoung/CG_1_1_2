using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class PolygonRender1 : MonoBehaviour
{
    public int polygon = 3;
    public float size = 1.0f;
    public float height = 1.0f;
    public Vector3 offset = new Vector3(0, 0, 0);

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    void OnValidate()
    {
        if (mesh == null)
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }

        if (size > 0 || offset.magnitude > 0 || polygon >= 3 || height > 0)
        {
            setMeshData(size, polygon);
            createProceduralMesh();
        }
    }

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        setMeshData(size, polygon);
        createProceduralMesh();
    }

    void setMeshData(float size, int polygon)
    {
        /* -------------------- ¹Ø¸é -------------------- */

        vertices = new Vector3[(polygon + 1) + (polygon + 1) + (polygon * 4)];

        vertices[0] = new Vector3(0, -height / 2.0f, 0) + offset;
        for (int i = 1; i <= polygon; i++)
        {
            float angle = -i * (Mathf.PI * 2.0f) / polygon;

            vertices[i] = new Vector3(Mathf.Cos(angle) * size, -height / 2.0f, Mathf.Sin(angle) * size) + offset;
        }

        triangles = new int[(3 * polygon) + (3 * polygon) + (6 * polygon)];
        for (int i = 0; i < polygon - 1; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 2;
            triangles[i * 3 + 2] = i + 1;
        }

        triangles[3 * polygon - 3] = 0;
        triangles[3 * polygon - 2] = 1;
        triangles[3 * polygon - 1] = polygon;

        /* -------------------- À­¸é -------------------- */

        int vIdx = polygon + 1;
        vertices[vIdx++] = new Vector3(0, height / 2.0f, 0) + offset;
        for (int i = 1; i <= polygon; i++)
        {
            float angle = -i * (Mathf.PI * 2.0f) / polygon;

            vertices[vIdx++] = new Vector3(Mathf.Cos(angle) * size, height / 2.0f, Mathf.Sin(angle) * size) + offset;
        }

        int tIdx = 3 * polygon;
        for (int i = 0; i < polygon - 1; i++)
        {
            triangles[tIdx++] = polygon + 1;
            triangles[tIdx++] = i + 1 + polygon + 1;
            triangles[tIdx++] = i + 2 + polygon + 1;
        }

        triangles[tIdx++] = polygon + 1;
        triangles[tIdx++] = polygon + polygon + 1;
        triangles[tIdx++] = 1 + polygon + 1;

        /* -------------------- ¿·¸é -------------------- */

        vIdx = 2 * polygon + 2;
        tIdx = 6 * polygon;
        for (int i = 1; i <= polygon - 1; i++)
        {
            float angle = -i * (Mathf.PI * 2.0f) / polygon;
            float nextAngle = -(i + 1) * (Mathf.PI * 2.0f) / polygon;
            vertices[vIdx] = new Vector3(Mathf.Cos(angle) * size, -height / 2.0f, Mathf.Sin(angle) * size) + offset;
            vertices[vIdx + 1] = new Vector3(Mathf.Cos(angle) * size, height / 2.0f, Mathf.Sin(angle) * size) + offset;
            vertices[vIdx + 2] = new Vector3(Mathf.Cos(nextAngle) * size, -height / 2.0f, Mathf.Sin(nextAngle) * size) + offset;
            vertices[vIdx + 3] = new Vector3(Mathf.Cos(nextAngle) * size, height / 2.0f, Mathf.Sin(nextAngle) * size) + offset;

            triangles[tIdx++] = vIdx;
            triangles[tIdx++] = vIdx + 2;
            triangles[tIdx++] = vIdx + 1;

            triangles[tIdx++] = vIdx + 2;
            triangles[tIdx++] = vIdx + 3;
            triangles[tIdx++] = vIdx + 1;

            vIdx += 4;
        }

        {
            float angle = -polygon * (Mathf.PI * 2.0f) / polygon;
            float nextAngle = -(0 + 1) * (Mathf.PI * 2.0f) / polygon;
            vertices[vIdx] = new Vector3(Mathf.Cos(angle) * size, -height / 2.0f, Mathf.Sin(angle) * size) + offset;
            vertices[vIdx + 1] = new Vector3(Mathf.Cos(angle) * size, height / 2.0f, Mathf.Sin(angle) * size) + offset;
            vertices[vIdx + 2] = new Vector3(Mathf.Cos(nextAngle) * size, -height / 2.0f, Mathf.Sin(nextAngle) * size) + offset;
            vertices[vIdx + 3] = new Vector3(Mathf.Cos(nextAngle) * size, height / 2.0f, Mathf.Sin(nextAngle) * size) + offset;

            triangles[tIdx++] = vIdx;
            triangles[tIdx++] = vIdx + 2;
            triangles[tIdx++] = vIdx + 1;

            triangles[tIdx++] = vIdx + 2;
            triangles[tIdx++] = vIdx + 3;
            triangles[tIdx++] = vIdx + 1;
        }
    }

    void createProceduralMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        // Remove the existing MeshCollider if present
        MeshCollider existingCollider = this.GetComponent<MeshCollider>();
        if (existingCollider != null)
        {
            DestroyImmediate(existingCollider);
        }
        // Optional: If you want a MeshCollider, you can uncomment the following line
        // this.gameObject.AddComponent<MeshCollider>();
    }
}
