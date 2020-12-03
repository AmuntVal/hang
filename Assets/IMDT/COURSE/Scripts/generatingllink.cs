
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatinglink : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        drawCylinder(obj1.transform.position, obj2.transform.position);

    }
    private Dictionary<int, Mesh> _meshMap = new Dictionary<int, Mesh>();
    private void drawCylinder(Vector3 a, Vector3 b)
    {
        float length = (a - b).magnitude;
        
        Graphics.DrawMesh(getCylinderMesh(length),
            Matrix4x4.TRS(a, Quaternion.LookRotation(b - a), Vector3.one), obj3.GetComponent<MeshRenderer>().material
            ,
            gameObject.layer);
    }//通过obj1.GetComponent<MeshRenderer>().material改圆柱材质
    private Mesh getCylinderMesh(float length)
    {
        int lengthKey = Mathf.RoundToInt(length * 100 / 12);

        Mesh mesh;
        if (_meshMap.TryGetValue(lengthKey, out mesh))
        {
            return mesh;
        }

        mesh = new Mesh();
        mesh.name = "GeneratedCylinder";
        mesh.hideFlags = HideFlags.DontSave;

        List<Vector3> verts = new List<Vector3>();
        List<Color> colors = new List<Color>();
        List<int> tris = new List<int>();

        Vector3 p0 = Vector3.zero;
        Vector3 p1 = Vector3.forward * length;
        for (int i = 0; i < 12; i++)
        {
            float angle = (Mathf.PI * 2.0f * i) / 12;
            float dx = 0.04f * Mathf.Cos(angle);//通过更改0.2数值更改x方向半径
            float dy = 0.04f * Mathf.Sin(angle);//通过更改0.2数值更改y方向半径

            Vector3 spoke = new Vector3(dx, dy, 0);

            verts.Add((p0 + spoke) * transform.lossyScale.x);
            verts.Add((p1 + spoke) * transform.lossyScale.x);

            colors.Add(Color.red);
            colors.Add(Color.red);

            int triStart = verts.Count;
            int triCap = 12 * 2;

            tris.Add((triStart + 0) % triCap);
            tris.Add((triStart + 2) % triCap);
            tris.Add((triStart + 1) % triCap);

            tris.Add((triStart + 2) % triCap);
            tris.Add((triStart + 3) % triCap);
            tris.Add((triStart + 1) % triCap);
        }
        

        mesh.SetVertices(verts);
        mesh.SetIndices(tris.ToArray(), MeshTopology.Triangles, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.UploadMeshData(true);

        _meshMap[lengthKey] = mesh;

        return mesh;
    }
}