using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuObject : MonoBehaviour {

    //Drag the object which you want to be automatically selected by the keyboard or gamepad when this panel becomes active
    public GameObject FirstSelectedObject;
    public GameObject[] Buttons;
    public RectTransform Selector;
    private int _index;
    public bool BlockSelect;
    public UnityEvent OnSelect; 


    public void SetFirstSelected()
    {
        EventSystemChecker.menuEventSystem.SetSelectedGameObject(FirstSelectedObject);
    }

    public void SetSelected(GameObject objectToSelect)
    {
        EventSystemChecker.menuEventSystem.SetSelectedGameObject(objectToSelect);
    }

    public void SetPositionSelector(GameObject selectedGameObject)
    {
        RectTransform rectTransform = selectedGameObject.GetComponent<RectTransform>();
        Selector.position = new Vector2(Selector.position.x, rectTransform.position.y);
    }

    public void OnEnable()
    {
        //Check if we have an event system present
        if (EventSystemChecker.menuEventSystem != null)
        {
            //If we do, select the specified object
            SetFirstSelected();
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetBlockSelect(bool value)
    {
        BlockSelect = value;
    }

    public void Update()
    {
        if (BlockSelect) return;

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            _index++;
            
            if (_index >= Buttons.Length)
            {
                _index = 0;
            }

            SetSelected(Buttons[_index]);
            SetPositionSelector(Buttons[_index]);
            OnSelect.Invoke();
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            _index--;
            
            if (_index < 0)
            {
                _index = Buttons.Length - 1;
            }

            SetSelected(Buttons[_index]);
            SetPositionSelector(Buttons[_index]);
            OnSelect.Invoke();
        }

    }

}
