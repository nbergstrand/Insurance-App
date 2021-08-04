using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationPanel : MonoBehaviour, IPanel
{
    public RawImage locationImage;
    public InputField notesField;

    [SerializeField]
    Text caseNumberText;

    public void ProcessInfo()
    {
    }

    void OnEnabled()
    {
        caseNumberText.text = UIManager.Instance.activeCase.caseNumber.ToString();
    }
}
