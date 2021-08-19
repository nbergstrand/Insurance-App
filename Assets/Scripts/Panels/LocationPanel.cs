using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class LocationPanel : MonoBehaviour, IPanel
{
    
    public InputField notesField;
    public GameObject _nextPanel;

    [SerializeField]
    Text _caseNumberText;

    [SerializeField]
    string apiKey;

    //Map image info
    [SerializeField]
    RawImage locationImage;
    [SerializeField]
    double xCoord, yCoord;
    [SerializeField]
    int imgSize;
    [SerializeField]
    int zoom;

    [SerializeField]
    string googleURL = "https://maps.googleapis.com/maps/api/staticmap?";

    Texture2D texture;

        
    public IEnumerator Start()
    {
        _caseNumberText.text = "CASE NUMBER: " + UIManager.Instance.activeCase.caseNumber;

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
                
        UIManager.Instance.activeCase.locationNotes = notesField.text;
        
        
        UIManager.Instance.activeCase.locationImage = texture.EncodeToPNG();


        _nextPanel.SetActive(true);
        this.gameObject.SetActive(false);
        
    }



    IEnumerator LoadMap()
    {
        googleURL = googleURL + "center=" + xCoord + "," + yCoord + "&zoom=" + zoom + "&size=" + imgSize + "x" + imgSize + "&key=" + apiKey;

        using (UnityWebRequest map = UnityWebRequestTexture.GetTexture(googleURL))
        {
            yield return map.SendWebRequest(); 

            if (map.isNetworkError || map.isHttpError)
            {
                Debug.LogError("Map Error:" + map.error);
            }

            locationImage.texture = DownloadHandlerTexture.GetContent(map);

            texture = (Texture2D)locationImage.texture;


        }
    }



}
