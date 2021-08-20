using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;

    public static UIManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    #endregion

    public Case activeCase;
    public Text caseNotFoundText;

    public GameObject searchPanel;
    public GameObject overViewPanel;


    public void CreateNewCase()
    {
        activeCase = new Case();
        activeCase.caseNumber = Random.Range(1, 1000).ToString();


    }

    public void Submit()
    {
        Case submittedCase = new Case();

        submittedCase.caseNumber = activeCase.caseNumber;
        submittedCase.name = activeCase.name;
        submittedCase.date = activeCase.date;
        submittedCase.locationNotes = activeCase.locationNotes;
        submittedCase.photoNotes = activeCase.photoNotes;
        submittedCase.locationImage = activeCase.locationImage;
        submittedCase.photo = activeCase.photo;
        
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/case #" + submittedCase.caseNumber + ".dat");
        formatter.Serialize(file, submittedCase);
        file.Close();
    }

    public void LoadData(string caseNumber)
    {
        if(File.Exists(Application.persistentDataPath + "/case #" + caseNumber + ".dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/case #" + caseNumber + ".dat", FileMode.Open);
            Case loadedCase = (Case)formatter.Deserialize(file);
            file.Close();

            activeCase.caseNumber = loadedCase.caseNumber;
            activeCase.name = loadedCase.name;
            activeCase.date = loadedCase.date;
            activeCase.locationNotes = loadedCase.locationNotes;
            activeCase.photoNotes = loadedCase.photoNotes;
            activeCase.locationImage = loadedCase.locationImage;
            activeCase.photo = loadedCase.photo;

            searchPanel.SetActive(false);
            overViewPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Case not found");
            caseNotFoundText.gameObject.SetActive(true);
        }
        
    }
}
