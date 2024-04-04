using System;
using System.Collections;
using System.Collections.Generic;

using System.IO;

using System.Text;
using UnityEngine;

public class PlotStars : MonoBehaviour
{


    //public ParticleSystem starField;
    public TextAsset textAssetData;

    public TextAsset ConsttextAssetData;

    public GameObject quadPrefab;

    //public Camera mainCamera;

    public GameObject lineRendererPrefab;
    //public ParticleSystem laserParticleSystem; // Assign this in the inspector


    List<string[]> starsData = new List<string[]>();
    List<Vector3> starPositions = new List<Vector3>();
    List<string> constellationsData = new List<string>();
    List<List<Vector3>> constellationsVectorData = new List<List<Vector3>>();

    public float TimeScale = 10.0f;


    // Start is called before the first frame update
    void Start()
    {
        GenerateStars();
        PlotingStars();
        //getconst();
        DrawConstellations("modern");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateStars()
    {
        string[] data = textAssetData.text.Split(new string[] { "\n" }, StringSplitOptions.None);
        for (int i = 1; i < data.Length - 1; i++)
        {
            string[] data2 = data[i].Split(new string[] { "," }, StringSplitOptions.None);
            starPositions.Add(new Vector3((float.Parse(data2[1]) * 1.01236f), (float.Parse(data2[2]) * 1.01236f), (float.Parse(data2[3]) * 1.01236f)));
            starsData.Add(data2);
        }


    }

    Color getColor(string[] spectral_type, float brightness)
    {
        char spectral_color = spectral_type[0][0];
        Debug.Log(brightness + " spectral_type: " + spectral_color);
        Color[] col = new Color[9];
        col[0] = IntColour(0x5c, 0x7c, 0xff, brightness); // O1
        col[1] = IntColour(0x5d, 0x7e, 0xff, brightness); // B0.5
        col[2] = IntColour(0x79, 0x96, 0xff, brightness); // A0
        col[3] = IntColour(0xb8, 0xc5, 0xff, brightness); // F0
        col[4] = IntColour(0xff, 0xef, 0xed, brightness); // G1
        col[5] = IntColour(0xff, 0xde, 0xc0, brightness); // K0
        col[6] = IntColour(0xff, 0xa2, 0x5a, brightness); // M0
        col[7] = IntColour(0xff, 0x7d, 0x24, brightness); // M9.5
        col[8] = Color.white;

        int col_idx = -1;
        if (spectral_color == 'O')
        {
            col_idx = 0;
        }
        else if (spectral_color == 'B')
        {
            col_idx = 1;
        }
        else if (spectral_color == 'A')
        {
            col_idx = 2;
        }
        else if (spectral_color == 'F')
        {
            col_idx = 3;
        }
        else if (spectral_color == 'G')
        {
            col_idx = 4;
        }
        else if (spectral_color == 'K')
        {
            col_idx = 5;
        }
        else if (spectral_color == 'M')
        {
            col_idx = 6;
        }

        // If unknown, make white.
        if (col_idx == -1)
        {
            col_idx = 8;
        }

        Debug.Log(col[col_idx]);
        return col[col_idx];
    }

    Color IntColour(int r, int g, int b, float brightness)
    {
        return new Color(r / 255f, g / 255f, b / 255f, brightness);
    }
    void PlotingStars()
    {
        for (int i = 0; i < starPositions.Count; i++)
        {
            GameObject star = Instantiate(quadPrefab, starPositions[i], Quaternion.identity);
            Renderer renderer = star.GetComponent<Renderer>();

            MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propBlock);

            // Set the tint color on the property block
            if (starsData[i][8] != "" && starsData[i][7] != "")
            {
                propBlock.SetColor("_TintColor", getColor(starsData[i][8].Split(' '), float.Parse(starsData[i][7])));
            }
            // Apply the property block back to the renderer
            renderer.SetPropertyBlock(propBlock);


        }


    }

    public void ClearAllLines()
    {
        LineRenderer[] allLineRenderers = FindObjectsOfType<LineRenderer>();
        Debug.Log("allLineRenderers: " + allLineRenderers.Length);
        foreach (LineRenderer lr in allLineRenderers)
        {
            //lr.positionCount = 0; // Clears the LineRenderer
            Destroy(lr);
        }
    }

    void getconst()
    {
        string[] data = ConsttextAssetData.text.Split(new string[] { "\n" }, StringSplitOptions.None);
        for (int i = 0; i < data.Length; i++)
        {
            Debug.Log("data " + data[i]);
        }

    }

    String[] GetConstellations(string constellationName)
    {
        string[] data = ConsttextAssetData.text.Split(new string[] { "\n" }, StringSplitOptions.None);
        //Debug.Log(result[0]);
        return data;

    }

    void GetVectorOfConstellations()
    {
        for (int i = 0; i < constellationsData.Count; i++)
        {
            List<string> constellation = new List<string>(constellationsData[i].Split(' '));
            List<Vector3> constellationVectorData = new List<Vector3>();
            for (int j = 3; j < constellation.Count; j++)
            {
                Debug.Log(constellation[j] + " " + starsData[0][0] + " " + ((int)Math.Floor(float.Parse(starsData[0][0]))).ToString());
                var starVectorIndex = starsData.FindIndex(x => ((int)Math.Floor(float.Parse(x[0]))).ToString() == constellation[j].ToString());
                Debug.Log(starVectorIndex);
                if (starVectorIndex > 1)
                {
                    Debug.Log(starsData[starVectorIndex][0]);
                    constellationVectorData.Add(new Vector3((float.Parse(starsData[starVectorIndex][1]) * 1.01236f), (float.Parse(starsData[starVectorIndex][2]) * 1.01236f), (float.Parse(starsData[starVectorIndex][3]) * 1.01236f)));
                }
            }
            Debug.Log(constellationVectorData.Count);
            if (constellationVectorData.Count != 0)
                constellationsVectorData.Add(constellationVectorData);

            //var result = starsData.Find(x => x[0] == constellationsList[0]);
        }
    }


    void DrawConstellation(List<Vector3> stars)
    {
        //float distanceToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);
        //float thickness = 0.01f + (distanceToCamera * 0.01f);
        Debug.Log("stars.Count " + stars.Count);

        if (stars.Count < 2) return; // A constellation needs at least two stars to form a line

        var lineRenderer = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<LineRenderer>();

        lineRenderer.positionCount = stars.Count;
        lineRenderer.startWidth = lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;


        for (int i = 0; i < stars.Count; i = i + 1)
        {

            Debug.Log("stars[i] " + stars[i]);
            lineRenderer.SetPosition(i, stars[i]);
        }
    }


    public void DrawConstellations(string constellationName)


    {
        ClearAllLines();
        Debug.Log("constellationName: " + constellationName);
        constellationsData.AddRange(GetConstellations(constellationName));
        GetVectorOfConstellations();
        Debug.Log("constellationsVectorData: " + constellationsVectorData.Count);
        foreach (var constellation in constellationsVectorData)
        {
            DrawConstellation(constellation);
        }
    }
}
