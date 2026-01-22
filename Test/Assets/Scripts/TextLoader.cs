using CSVToolKit;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.UI.Image;

public class TextLoader : MonoBehaviour
{
    public string fileName = "TextData.csv"; // file in StreamingAssets
    private Dictionary<string, string> textData = new Dictionary<string, string>();

    [HideInInspector]
    public bool isLoaded = false;

    void Start()
    {
        LoadTextData();
    }

    void LoadTextData()
    {
        List<List<string>> allData;
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        if (File.Exists(filePath))
        {
            allData = CSVParser.Instance.ReadData(Application.streamingAssetsPath, fileName);
            foreach (List<string> row in allData)
            {
                if (row.Count < 2)
                    continue;

                string val = row[1];

                for (int i = 2; i < row.Count; i++)
                {
                    val = val+","+row[i];
                }
                val = val.Trim();
                char lastChar = val[val.Length - 1];

                string acVal = "";
                if (lastChar == '"') 
                {
                    acVal = val.Substring(1, val.Length - 2);
                }
                else
                {

                    acVal = val.Substring(1, val.Length - 1);
                }
                textData[row[0].Trim()] = acVal;
            }
            Debug.Log("Text data loaded successfully!");
            isLoaded = true;
        }
        else
        {
            Debug.LogError("Text file not found at: " + filePath);
        }
    }

    // Call this to get text for a specific panel
    public string GetText(string panelName)
    {
        if (textData.ContainsKey(panelName))
            return textData[panelName];
        else
            return "";
    }
}
