using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButton : MonoBehaviour {
    [SerializeField] private GameObject _enableButton;
    [SerializeField] private GameObject _disableButton;

    public void ChangeButtons(bool enable)
    {
        _enableButton.SetActive(!enable);
        _disableButton.SetActive(enable);
    }
}
