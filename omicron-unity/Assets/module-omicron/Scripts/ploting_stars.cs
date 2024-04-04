using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using UnityEditor.AnimatedValues;
using UnityEngine.Events;

public class ploting_stars : MonoBehaviour
{
    public ParticleSystem starField;
    public TextAsset textAssetData;
    public Camera mainCamera;
    public GameObject lineRendererPrefab;

    List<string[]> starsData = new List<string[]>();
    List<Vector3> starPositions = new List<Vector3>();
    List<string> constellationsData = new List<string>();
    List<List<Vector3>> constellationsVectorData = new List<List<Vector3>>();

    public float TimeScale = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateStars();
        //constellationsData.AddRange(GetConstellations("lol"));
        Debug.Log("starsData: "+starsData.Count);
        //GetVectorOfConstellations();
        //Debug.Log(constellationsVectorData.Count);
 
    



    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 60;
    
        //Debug.Log("constellationsList " +constellationsList.Count);
        //PlotStars();
        //DrawConstellations();
        PlotStars();
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

    float getsize(string spectral_type)
    {
        string size = spectral_type;
        float baseScale = 0.1f; // Base scale for all stars, adjust as needed
        //star.transform.localScale =
        if (spectral_type.StartsWith("O"))  return baseScale * 1.5f;
        else if (spectral_type.StartsWith("B")) return baseScale * 1.4f;

        return 0.0f;
    }

    Color getColor(string[] spectral_type, float brightness)
    {
        char spectral_color = spectral_type[0][0];
        Debug.Log(brightness+" spectral_type: " +spectral_type);
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

    Vector3 UpdateStarPositions(Vector3 starPositions, Vector3 Velocity, float deltaTimeYears)
    {
        starPositions += Velocity * deltaTimeYears;
        return starPositions;
    }

    void PlotStars()
    {
        var mainModule = starField.main;
        mainModule.maxParticles = starPositions.Count;
        float deltaTimeYears = Time.deltaTime * TimeScale;

        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[starPositions.Count];
        for (int i = 0; i < starPositions.Count; i++)
        {
            //Debug.Log(deltaTimeYears+" lol: " + UpdateStarPositions(starPositions[i], new Vector3(float.Parse(starsData[i][4]) * 1.02269e-6f, float.Parse(starsData[i][5]) * 1.02269e-6f, float.Parse(starsData[i][6])) * 1.02269e-6f, deltaTimeYears));
            //particles[i].position = UpdateStarPositions(starPositions[i], new Vector3(float.Parse(starsData[i][4]) * 1.02269e-6f, float.Parse(starsData[i][5]) * 1.02269e-6f, float.Parse(starsData[i][6])) * 1.02269e-6f, deltaTimeYears);
            particles[i].position = starPositions[i];

            particles[i].startSize = mainModule.startSize.constant; // Use your particle size
                                                                    //Debug.Log(starsData[i][7]+" lol:" + starsData[i][8]);
            particles[i].startColor = mainModule.startColor.color; // Use your particle color

            //if (starsData[i][8] !=  "" && starsData[i][7]!="")
            //{
            //    //Debug.Log(starsData[i][7]+" lol:" + starsData[i][8]);
            //    particles[i].startColor = getColor(starsData[i][8].Split(' '), float.Parse(starsData[i][7])); // Use your particle color

            //}
            ////if (starsData[i].Length >= 8)
            ////{
            ////    particles[i].startColor = getColor(starsData[i][8][0], float.Parse(starsData[i][7])); // Use your particle color

            ////}
            //else
            //{

            //    //particles[i].startColor = new Color(starColor.r, starColor.g, starColor.b, brightness);
            //    particles[i].startColor = mainModule.startColor.color; // Use your particle color
            //}

            particles[i].remainingLifetime = Mathf.Infinity; // Ensure they don't die
        }
        starField.SetParticles(particles, particles.Length);
    }

    String[] GetConstellations(string constellationName)
    {
        using (BinaryReader reader = new BinaryReader(File.Open("Assets/module-omicron/Data/"+constellationName+".fab", FileMode.Open, FileAccess.Read)))
        {

            byte[] headerBytes = reader.ReadBytes((int)reader.BaseStream.Length); // Replace 8 with the actual header size

            string formatIdentifier = Encoding.ASCII.GetString(headerBytes, 0, (int)reader.BaseStream.Length); // Example: extract format identifier
            //Debug.Log(formatIdentifier + "  " + reader);
            string[] result = formatIdentifier.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            //Debug.Log(result[0]);
            return result;
        }
    }

    void GetVectorOfConstellations()
    {
        for (int i = 0; i < constellationsData.Count; i++)
        {
            List<string> constellation= new List<string>(constellationsData[i].Split(' '));
            List<Vector3> constellationVectorData = new List<Vector3>();
            for (int j=3;j< constellation.Count; j++)
            {
                Debug.Log(constellation[j]+" "+ starsData[0][0]+" "+ ((int)Math.Floor(float.Parse(starsData[0][0]))).ToString());
                var starVectorIndex = starsData.FindIndex(x => ((int)Math.Floor(float.Parse(x[0]))).ToString() == constellation[j].ToString());
                Debug.Log(starVectorIndex);
                if (starVectorIndex > 1)
                {
                    Debug.Log(starsData[starVectorIndex][0]);
                    constellationVectorData.Add(new Vector3((float.Parse(starsData[starVectorIndex][1]) * 1.01236f), (float.Parse(starsData[starVectorIndex][2]) * 1.01236f), (float.Parse(starsData[starVectorIndex][3]) * 1.01236f)));
                }
                }
            Debug.Log(constellationVectorData.Count);
            if(constellationVectorData.Count!=0)
            constellationsVectorData.Add(constellationVectorData);

            //var result = starsData.Find(x => x[0] == constellationsList[0]);
        }
    }

    void DrawLines()
    {

        // Now you can set positions or any other properties
       
        int count_lines = 1;
        //lr.positionCount = 10000;
        int totalCount=0;
        foreach (List<Vector3> sublist in constellationsVectorData)
        {
            totalCount += sublist.Count;
        }
        

        for (int c = 0; c < constellationsVectorData.Count; c++)
        {
            GameObject myLine = Instantiate(lineRendererPrefab);
            LineRenderer lr = myLine.GetComponent<LineRenderer>();
       
            lr.startWidth = lr.endWidth = 0.10f;
            lr.startColor = Color.red;
            lr.endColor = Color.blue;

            //LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lr.positionCount = totalCount*2; // Set the number of positions

            for (int i = 0; i < constellationsVectorData[c].Count - 1; i++)
            {
              
                    lr.SetPosition(count_lines, constellationsVectorData[c][i]);
                    lr.SetPosition(count_lines+1, constellationsVectorData[c][i + 1]);
                count_lines = count_lines + 2;


            }
        }
        //for (int c = 0; c < constellationsVectorData.Count; c++)
        //{
        //    //Debug.Log("conts_data2.Count " + conts_data2[c]);
        //    LineRenderer lineRenderer = GetComponent<LineRenderer>();
        //    lineRenderer.positionCount = constellationsVectorData[c].Count;
        //    lineRenderer.startWidth = lineRenderer.endWidth = 0.10f;
        //    lineRenderer.startColor = Color.red;
        //    lineRenderer.endColor = Color.blue;

        //    //LineRenderer lineRenderer = GetComponent<LineRenderer>();
        //    lineRenderer.positionCount = constellationsVectorData[c].Count; // Set the number of positions

        //    for (int i = 0; i < constellationsVectorData[c].Count - 1; i++)
        //    {
        //        lineRenderer.SetPosition(i, constellationsVectorData[c][i]); // Set start point
        //        lineRenderer.SetPosition(i + 1, constellationsVectorData[c][i + 1]); // Set end point
        //                                                                //DrawLine(myList[i], myList[i+1], Color.red, 10f);

        //    }
        //}
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
    public void DrawConstellations(string constellationName)

    {
        ClearAllLines();
        constellationsData.AddRange(GetConstellations(constellationName));
        GetVectorOfConstellations();
        Debug.Log(constellationsVectorData.Count);
        foreach (var constellation in constellationsVectorData)
        {
            DrawConstellation(constellation);
        }
    }

    void DrawConstellation(List<Vector3> stars)
    {
        float distanceToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);
        float thickness = 0.01f + (distanceToCamera * 0.01f);

        if (stars.Count < 2) return; // A constellation needs at least two stars to form a line

        var lineRenderer = Instantiate(lineRendererPrefab, Vector3.zero, Quaternion.identity, transform).GetComponent<LineRenderer>();

        lineRenderer.positionCount = stars.Count;
        lineRenderer.startWidth = lineRenderer.endWidth = thickness;
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;

        for (int i = 0; i <stars.Count; i=i+1)
        {
            
            lineRenderer.SetPosition(i, stars[i]);
        }
    }

    public  void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        
    }

}
