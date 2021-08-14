using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoPanel : MonoBehaviour, IPanel
{
    [SerializeField]
    Text _caseNumberText;

    [SerializeField]
    GameObject _nextPanel;

    [SerializeField]
    RawImage photoImage;

    [SerializeField]
    int photoSize;

    [SerializeField]
    InputField notesField;

    void OnEnable()
    {
        _caseNumberText.text = "CASE NUMBER: " + UIManager.Instance.activeCase.caseNumber;
    }

    public void ProcessInfo()
    {
       

        UIManager.Instance.activeCase.photoNotes = notesField.text;
        UIManager.Instance.activeCase.photo = photoImage;

        _nextPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void ActivateCamera()
    {
        TakePicture(photoSize);

    }

    private void TakePicture(int maxSize)
    {
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                // Create a Texture2D from the captured image
                Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                photoImage.texture = texture;
                photoImage.gameObject.SetActive(true);
            }
        }, maxSize);

        Debug.Log("Permission result: " + permission);
    }

  

}
