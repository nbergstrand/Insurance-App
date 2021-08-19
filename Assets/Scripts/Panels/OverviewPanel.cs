using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverviewPanel : MonoBehaviour, IPanel
{
    public Text caseNumberText;
    public Text nameText;
    public Text dateText;
    public RawImage locationImage;
    public Text locationNotesText;
    public RawImage photoImage;
    public Text photoNotes;
    

    public void ProcessInfo()
    {
        
    }

    void OnEnable()
    {
        caseNumberText.text = "CASE NUMBER: " + UIManager.Instance.activeCase.caseNumber;
        nameText.text = "NAME: " + UIManager.Instance.activeCase.name.ToUpper();
        dateText.text = "DATE: " + UIManager.Instance.activeCase.date;

        Texture2D tempLocationTex = new Texture2D(1,1);
        tempLocationTex.LoadImage(UIManager.Instance.activeCase.locationImage);
               

        locationImage.texture = (Texture)tempLocationTex;
        locationNotesText.text = "LOCATION NOTES: \n" + UIManager.Instance.activeCase.locationNotes;

        Texture2D tempPhotoTex = new Texture2D(1, 1);
        tempPhotoTex.LoadImage(UIManager.Instance.activeCase.photo);

        photoImage.texture = tempPhotoTex;
        photoNotes.text = "PHOTO NOTES: \n" + UIManager.Instance.activeCase.photoNotes;
    }
}
