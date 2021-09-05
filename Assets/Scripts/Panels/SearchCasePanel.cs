using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchCasePanel : MonoBehaviour, IPanel
{
    public InputField caseNumberInput;

    public void ProcessInfo()
    {
        // UIManager.Instance.LoadDataLocally(CaseNumberInput.text);
        AWSManager.Instance.LoadFromS3(caseNumberInput.text, () =>
        {
            UIManager.Instance.OpenOverview();

        });
        
    }
}

