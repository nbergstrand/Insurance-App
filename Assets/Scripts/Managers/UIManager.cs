using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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


        /*
          public string date;
         public string location;
            public RawImage locationImage;
         public string locationNotes;
            public RawImage photo;
    public string photoNotes;
         */


        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "case#" + submittedCase.caseNumber + ".dat");
        formatter.Serialize(file, submittedCase);
        file.Close();
    }
}
