using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCasePanel : MonoBehaviour, IPanel
{
    
    public InputField firstName, lastName;

    [SerializeField]
    Text caseNumberText;

    public void ProcessInfo()
    {
        
    }

    void OnEnable()
    {
        Debug.Log("Enabled");
        caseNumberText.text = "CASE NUMBER: " + UIManager.Instance.activeCase.caseNumber.ToString();
    }
}
