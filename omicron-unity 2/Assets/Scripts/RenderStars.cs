using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class RenderStars : MonoBehaviour
{
    public TextAsset exoCsvFile;
    public TextAsset starCsvFile;


    public Transform userOrigin; 
    public Transform playerController;

    public float maxDistance = 100f; 
    private float positionScale = 1.0f;

    public GameObject starPrefab;
    public Slider positionScaleSlider;

    // List to store star data
    public List<StarData> starDataset = new List<StarData>();
    //private List<StarData> starDataset = new List<StarData>(); // List to store star data
    public Dictionary<int, GameObject> loadedStars = new Dictionary<int, GameObject>(); // Dictionary to store loaded stars with hip as key
    private List<StarData> renderedStars = new List<StarData> { }; // Dictionary to store loaded stars with hip as key

    private bool isStarted = false; // Flag to indicate whether Start function has been called

    private float lastUpdateTime; // Time of the last update
    public float updateInterval = 5f; // Update interval in seconds
    public float updateVelocityInterval = 2f; // Update interval in seconds
    private float lastVelocityUpdateTime; // Time of the last velocity update
    public bool startVelocity = false;

    //public ConstellationRenderer ConstellationRenderer;

    public static event Action StarsLoaded; // Event to signal that stars are loaded

    private Vector3 initialPosition; // Initial position of the origin GameObject
    private Vector3 originalPosition;
    private Quaternion originalOrientation;
    public float resetDuration = 5.0f; // Duration of the movement
    //public float distanceThreshold = 1.0f; // Distance threshold to trigger the function

    //public Toggle stellarCheckbox;
    //public Toggle knownPlanetsCheckbox;

    public bool isVelocityNegative = false;
    private int velocityDirectionMultiplier = 1;

    private int numVelocityCycles = 0;
    public bool activateMoveToAndromeda = false;




    public struct StarData
    {
        public int hip;
        public float dist, x0, y0, z0, absmag, mag, vx, vy, vz;
        public string spect;
        public int numExo;
    }

    // Start is called before the first frame update
    void Start()
    {
        ReadCsvFile();
        
        ReadExoplanetCSV();


        isStarted = true;

        positionScaleSlider.onValueChanged.AddListener(delegate { SetPositionScale(positionScaleSlider.value); });

        IntitialStars();

        // Store the initial position
        initialPosition = userOrigin.transform.position;
        originalPosition = playerController.transform.position;
        originalOrientation = playerController.transform.rotation;

        // Raise the event to signal that stars are loaded
        if (StarsLoaded != null)
        {
            StarsLoaded.Invoke();
        }

        
    }
    public void SetPositionScale(float scale)
    {
        positionScale = scale;
        foreach (var star in loadedStars.Keys)
        {
            foreach (var starData in renderedStars)
            {
                if (starData.hip == star)
                {
                    Debug.Log("isStarted");
                    //loadedStars[star].Move();
                    if (loadedStars[star].activeSelf)
                    {


                        loadedStars[star].transform.position += (new Vector3(starData.vx, starData.vy, starData.vz) * positionScale);
                    }

                }
            }
        }
    }


    public void Starvelocity(string flag)
    {
        //distanceInLightYears = distanceInLightYears + Time.deltaTime / 600;
        //text.text = distanceInLightYears.ToString("F2") + " Light Years";
        //Debug.Log("isStartedflag: " + flag);

        //if (flag == "reset")
        //{
        //    //text.text="0";
        //    distanceInLightYears = 0;
        //}
        foreach (var star in loadedStars.Keys)
        {
            foreach (var starData in renderedStars)
            {
                if (starData.hip == star)
                {
                    Debug.Log("isStarted");
                    //loadedStars[star].Move();
                    if (loadedStars[star].activeSelf)
                    {
                        MoveSta starscript = loadedStars[star].GetComponent<MoveSta>();
                        starscript.flag = flag;

                        //loadedStars[star].transform.position += (new Vector3(starData.vx, starData.vy, starData.vz) * Time.deltaTime);
                    }

                }
            }
        }
    }



    public void exodata()
    {

        foreach (var star in loadedStars.Keys)
        {
            foreach (var starData in renderedStars)
            {
                if (starData.hip == star)
                {
                    //loadedStars[starHip].transform.position
                    Renderer renderer = loadedStars[star].GetComponent<Renderer>();
                    MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                    renderer.GetPropertyBlock(propBlock);
                    if (renderer != null)
                    {
                        propBlock.SetColor("_TintColor", exodata(starData.numExo));
                    }
                    renderer.SetPropertyBlock(propBlock);

                }
            }

        }
    }



        void Update()
    {
        
        if (isStarted)
        {
            // Check if it's time to update stars with origin movement
            if (Time.time - lastUpdateTime >= updateInterval)
            {
                if (HasMovedBeyondThreshold())
                {
                    IntitialStars();
                    lastUpdateTime = Time.time; // Update the last update time
                }
            }

        }
    }

 
    private bool HasMovedBeyondThreshold()
    {
        // Calculate the distance between the current position and the initial position
        float distance = Vector3.Distance(userOrigin.transform.position, initialPosition);

        // Return true if the distance is greater than or equal to the threshold
        bool shouldUpdate = distance >= maxDistance;
        //Debug.Log("Called moved" + shouldUpdate);
        if (shouldUpdate)
        {
            initialPosition = userOrigin.transform.position;
        }
        return shouldUpdate;
    }

    public void IntitialStars()
    {
       
        RemoveStars();

     
        foreach (var starData in starDataset)
        {
            Vector3 starPosition = new Vector3(starData.x0 * positionScale, starData.y0 * positionScale, starData.z0 * positionScale);
            float distanceToOrigin = Vector3.Distance(userOrigin.position, starPosition);


            if (distanceToOrigin <= maxDistance * positionScale)
            {
                GameObject star = Instantiate(starPrefab, starPosition, Quaternion.identity);
                MoveSta mover = star.AddComponent<MoveSta>();
                mover.velocity = new Vector3(starData.vx * 1.01236f, starData.vy * 1.01236f, starData.vz * 1.01236f);

                //Color starColor = getColor(starData.spect, starData.absmag);
                Renderer renderer = star.GetComponent<Renderer>();
                MaterialPropertyBlock propBlock = new MaterialPropertyBlock();
                renderer.GetPropertyBlock(propBlock);
                if (renderer != null)
                {
                    propBlock.SetColor("_TintColor", getColor(starData.spect, starData.absmag));
                }
                renderer.SetPropertyBlock(propBlock);

                renderedStars.Add(starData);
                loadedStars.Add(starData.hip, star);
            }
        }

    }

    Color getColor(string spectral_type, float brightness)
    {
        char spectral_color;
        if (spectral_type.Length != 0)
        {
            spectral_color = spectral_type[0];
        }
        else
        {
            spectral_color = '_';
        }
        //Debug.Log(brightness + " spectral_type: " + spectral_color);
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
        else
        {
            col_idx = 8;
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

    Color exodata(int no_planet)
    {

        Debug.Log("no_planet: " + no_planet);
        switch (no_planet)
        {
            case 1:
                // Code block to execute when the expression matches value1
                return Color.blue;

            case 2:
                // Code block to execute when the expression matches value2
                return Color.green;

            case 3:
                // Code block to execute when the expression matches value2
                return Color.magenta;
            case 4:
                // Code block to execute when the expression matches value2
                return Color.cyan;
            case 5:
                // Code block to execute when the expression matches value2
                return Color.yellow;
            // You can have any number of case statements
            case 6:
                // Code block to execute when the expression matches value2
                return Color.red;

            default:
                return Color.white;
        }
    }


    //void UpdateStarsWithVelocity(float deltaTime)
    //{
    //    // Update positions of loaded stars based on their velocities
    //    foreach (var starHip in loadedStars.Keys)
    //    {
    //        foreach (var starData in renderedStars)
    //        {
    //            if (starData.hip == starHip)
    //            {
    //                Vector3 velocity = new Vector3(starData.vx, starData.vy, starData.vz);
    //                Vector3 newPosition = new Vector3(
    //                    starData.x0 + velocity.x * deltaTime * velocityDirectionMultiplier,
    //                    starData.y0 + velocity.y * deltaTime * velocityDirectionMultiplier,
    //                    starData.z0 + velocity.z * deltaTime * velocityDirectionMultiplier
    //                );

    //                // Scale the positions of stars
    //                newPosition *= positionScale;

    //                // Check if the star is within the visible range
    //                float distanceToOrigin = Vector3.Distance(userOrigin.position, newPosition);
    //                if (distanceToOrigin <= maxDistance * velocityDistanceMultiplier)
    //                {
    //                    // Update the position of the loaded star
    //                    loadedStars[starHip].transform.position = newPosition;
    //                }
    //                break;
    //            }
    //        }
    //    }
    //}


    void RemoveStars()
    {
        renderedStars.Clear();
        foreach (var star in loadedStars.Values)
        {
            Destroy(star);
        }
        loadedStars.Clear();
    }

    void ReadCsvFile()
    {

        starDataset.Clear();
        string[] starFileLines = starCsvFile.text.Split('\n');
        for (int j =1;j< starFileLines.Length; j++)
        {
            Debug.Log("starFileLines[j] "+ starFileLines[j]);
            string[] values = starFileLines[j].Split(',');
            if (values.Length == 11) 
            {
                StarData starData = new StarData();
                starData.hip = int.Parse(values[0]);
                starData.dist = float.Parse(values[1]);
                starData.x0 = float.Parse(values[2]);
                starData.y0 = float.Parse(values[3]);
                starData.z0 = float.Parse(values[4]);
                starData.absmag = float.Parse(values[5]);
                starData.mag = float.Parse(values[6]);
                starData.vx = float.Parse(values[7]);
                starData.vy = float.Parse(values[8]);
                starData.vz = float.Parse(values[9]);
                starData.spect = values[10];
                starDataset.Add(starData);
            }

        }
        
        Debug.Log("total stas: " + starDataset.Count);
     
    }

    
    void ReadExoplanetCSV()
    {
        string[] fileLines = exoCsvFile.text.Split('\n');
        for (int j = 1; j < fileLines.Length; j++)
        {
            string line = fileLines[j];
            string[] values = line.Split(',');
            if (values.Length == 2) 
            {int hipId;
                if (int.TryParse(values[0].Trim().Split(' ')[1], out hipId))
                { for (int i = 0; i < starDataset.Count; i++)
                    { if (starDataset[i].hip == hipId)
                        { StarData star = starDataset[i];
                            star.numExo = int.Parse(values[1]);
                            starDataset[i] = star;
                            Debug.Log("numExo" + starDataset[i].numExo);
                            break; 
                        }
                    }
                }
                else
                {
                    Debug.LogError("Invalid HIP ID format in exoplanet CSV.");
                }
            }
            else
            {
                Debug.LogError("Invalid data format in exoplanet CSV");
            }
        }


            
        }

    

 
    public void resetLocation()
    {
        Debug.Log("Called reset");
        Vector3 currentPosition = playerController.transform.position;
        MoveToTarget(playerController, currentPosition, originalPosition);
    }

    IEnumerator MoveToTarget(Transform targetObj, Vector3 startPosition, Vector3 targetPosition)
    {float elapsedTime = 0f;
        while (elapsedTime < resetDuration)
        {
            float t = elapsedTime / resetDuration;
            targetObj.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        targetObj.transform.position = targetPosition;
    }


    public void resetOrientation()
    {
        playerController.transform.rotation = originalOrientation;
    }

    public void resetTime()
    {
        numVelocityCycles = 1;
        IntitialStars();
    }

    public void resetAll()
    {
        resetTime();
        resetLocation();
        resetOrientation();
    }
}
