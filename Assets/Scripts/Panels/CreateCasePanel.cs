using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCasePanel : MonoBehaviour, IPanel
{
    
    public InputField firstName, lastName, date;

    [SerializeField]
    GameObject _nextPanel;

    [SerializeField]
    Text _caseNumberText, _errorText;

    public void ProcessInfo()
    {
        if(string.IsNullOrEmpty(firstName.text) || string.IsNullOrEmpty(lastName.text) || string.IsNullOrEmpty(date.text))
        {
            _errorText.gameObject.SetActive(true);
            
        }
        else
        {
            UIManager.Instance.activeCase.name = firstName.text + " " + lastName.text;
            _errorText.gameObject.SetActive(false);
            _nextPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {        
        _caseNumberText.text = "CASE NUMBER: " + UIManager.Instance.activeCase.caseNumber;
        date.text = "" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year;
        UIManager.Instance.activeCase.date = date.text;
    }
}
