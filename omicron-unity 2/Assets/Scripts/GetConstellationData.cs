



using System;
using System.Collections.Generic;

using UnityEngine;

public class GetConstellationData : MonoBehaviour
{
    public enum ConstellationDataset
    {
        Modern,
        Arabic,
        Norse,
        Egyptian,
        Chinese,
        Maya
    }

    public TextAsset modernConstellationFile;
    //public TextAsset modernConstellationNamesFile;


    public TextAsset egyptianConstellationFile;
    //public TextAsset egyptianConstellationNamesFile;

    public TextAsset chineseConstellationFile;
    //public TextAsset chineseConstellationNamesFile;


    public TextAsset arabicConstellationFile;
    //public TextAsset arabicConstellationNamesFile;

    public TextAsset indianConstellationFile;
    //public TextAsset indianConstellationNamesFile;



    private Dictionary<string, string[]> datasetMap = new Dictionary<string, string[]>();

    public string SelectedConstellationDataset { get; private set; }

    public DrawContellation drawContellation;


    void Start()
    {
        // Populate the dataset map
        datasetMap.Add("modern", (GetLines(modernConstellationFile)));
        datasetMap.Add("arabic", (GetLines(arabicConstellationFile)));
        datasetMap.Add("indian", (GetLines(indianConstellationFile)));
        datasetMap.Add("egpytian", (GetLines(egyptianConstellationFile)));
        datasetMap.Add("chinese", (GetLines(chineseConstellationFile)));
        SelectedConstellationDataset = "moden";

        buttonConst("modern");

    }

    string[] GetLines(TextAsset textAsset)
    {
        if (textAsset != null)
        {
            string[] lines = textAsset.text.Split('\n');
            Debug.Log(lines[0]);
            return lines;
        }
        else
        {
            Debug.LogError("TextAsset is null.");
            return new string[0];
        }
    }




    public void buttonConst(string constellationName)
    {


        SelectedConstellationDataset = constellationName;
        Debug.Log("datasetMap "+ SelectedConstellationDataset);
        drawContellation.SendMessage("UpdateConstellations");


    }




    public string[] GetConstellationFile()
    {
        string[] dataset;
        if (datasetMap.TryGetValue(SelectedConstellationDataset, out dataset))
        {
            return dataset;
        }
        else
        {
            Debug.LogError("Selected constellation dataset not found in dataset map: " + SelectedConstellationDataset);
            return null;
        }
    }


}
