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
        locationImage.texture = UIManager.Instance.activeCase.locationImage.texture;
        locationNotesText.text = "LOCATION NOTES: \n" + UIManager.Instance.activeCase.locationNotes;
        photoImage.texture = UIManager.Instance.activeCase.photo.texture;
        photoNotes.text = "PHOTO NOTES: \n" + UIManager.Instance.activeCase.photoNotes;
    }
}
