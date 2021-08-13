using System.Collections;
using System.Collections.Generic;
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
}
