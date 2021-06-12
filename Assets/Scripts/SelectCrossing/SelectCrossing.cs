using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class SelectCrossing : MonoBehaviour {
    
    [SerializeField] private ChangeButton _horizontalButton;
    [SerializeField] private ChangeButton _verticalButton;
    [SerializeField] private ChangeButton _allButton;
    [SerializeField] private MapGenerator _map;
    
    private Crossing _selected;

    private void OnFire(InputValue value)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()), out hitInfo))
        {
            _selected = hitInfo.collider.GetComponent<Crossing>();
            if (_selected)
            {
                EnableAllButtons(false);
                
                if (_selected.canCross.HasHorizontal)
                {
                    _horizontalButton.gameObject.SetActive(true);
                    if(_selected.canCross.CanBeCrossedHorizontal)
                        _horizontalButton.Enable();
                    else
                        _horizontalButton.Disable();
                }

                if (_selected.canCross.HasVertical)
                {
                    _verticalButton.gameObject.SetActive(true);
                    if(_selected.canCross.CanBeCrossedVertical)
                        _verticalButton.Enable();
                    else
                        _verticalButton.Disable();
                }

                if (_selected.canCross.HasHorizontal && _selected.canCross.HasVertical)
                {
                    _allButton.gameObject.SetActive(true);
                    bool enable = (_selected.canCross.CanBeCrossedVertical == _selected.canCross.CanBeCrossedHorizontal) &&
                                  _selected.canCross.CanBeCrossedVertical == true;
                    if(enable)
                        _allButton.Enable();
                    else
                        _allButton.Disable();
                }
            }
        }
        else
        {
            _selected = null;
            EnableAllButtons(false);
        }
    }

    private void EnableAllButtons(bool enable)
    {
        _horizontalButton.gameObject.SetActive(enable);
        _verticalButton.gameObject.SetActive(enable);
        _allButton.gameObject.SetActive(enable);
    }

    public void ChangeHorizontal(bool enable)
    {
        if (_selected)
            _selected.canCross.CanBeCrossedHorizontal = enable;
    }
    
    public void ChangeVertical(bool enable)
    {
        if (_selected)
            _selected.canCross.CanBeCrossedVertical = enable;
    }

    public void ChangeAll(bool enable)
    {
        ChangeHorizontal(enable);
        ChangeVertical(enable);
    }
}
