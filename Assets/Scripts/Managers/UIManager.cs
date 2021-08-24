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
        
        
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/case #" + activeCase.caseNumber + ".dat");
        formatter.Serialize(file, activeCase);
        file.Close();
    }

    public void LoadData(string caseNumber)
    {
        if(File.Exists(Application.persistentDataPath + "/case #" + caseNumber + ".dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/case #" + caseNumber + ".dat", FileMode.Open);
            activeCase = (Case)formatter.Deserialize(file);
            file.Close();
            
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
