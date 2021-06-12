using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButton : MonoBehaviour {
    [SerializeField] private GameObject _enableButton;
    [SerializeField] private GameObject _disableButton;

    public void Enable()
    {
        _enableButton.SetActive(false);
        _disableButton.SetActive(true);
    }

    public void Disable()
    {
        _enableButton.SetActive(true);
        _disableButton.SetActive(false);
    }
}
