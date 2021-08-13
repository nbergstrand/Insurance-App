﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LocationPanel : MonoBehaviour, IPanel
{
    
    public InputField notesField;
    public GameObject _nextPanel;

    [SerializeField]
    Text caseNumberText;

    [SerializeField]
    string apiKey;

    //Map image info
    [SerializeField]
    RawImage locationImage;
    double xCoord, yCoord;
    [SerializeField]
    int imgSize;
    [SerializeField]
    int zoom;

    [SerializeField]
    string googleURL = "https://maps.googleapis.com/maps/api/staticmap?";

        
    public IEnumerator Start()
    {
        caseNumberText.text = "CASE NUMBER: " + UIManager.Instance.activeCase.caseNumber;

        Input.location.Start();

        if (Input.location.isEnabledByUser == true)
        {
           
            
            int maxWait = 20;

            while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1.0f);
                maxWait--;
            }

            if(maxWait < 1)
            {
                Debug.Log("Timed out");
                yield break;
            }


            if(Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Failed to retrieve location");
            }
            else
            {
                
                xCoord = Input.location.lastData.latitude;
                yCoord = Input.location.lastData.longitude;
            }

            Input.location.Stop();
        }

        StartCoroutine(LoadMap());
    }

    public void ProcessInfo()
    {

        if (string.IsNullOrEmpty(notesField.text))
        {
            _nextPanel.SetActive(true);
            this.gameObject.SetActive(false);

        }
        else
        {
            UIManager.Instance.activeCase.locationNotes = notesField.text;
            _nextPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator LoadMap()
    {
        googleURL = googleURL + "center=" + xCoord + "," + yCoord + "&zoom=" + zoom + "&size=" + imgSize + "x" + imgSize + "&key=" + apiKey;

        using (WWW map = new WWW(googleURL))
        {
            yield return map;

            if (map.error != null)
            {
                Debug.LogError("Map Error:" + map.error);
            }

            locationImage.texture = map.texture;
        }
    }

   


}
