using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObject : MonoBehaviour {

    //Drag the object which you want to be automatically selected by the keyboard or gamepad when this panel becomes active
    public GameObject FirstSelectedObject;
    public GameObject[] Buttons;
    public RectTransform Selector;
    private int _index;
    
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _index++;
            
            if (_index >= Buttons.Length)
            {
                _index = 0;
            }

            SetSelected(Buttons[_index]);
            SetPositionSelector(Buttons[_index]);
        }

        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            _index--;
            
            if (_index < 0)
            {
                _index = Buttons.Length - 1;
            }

            SetSelected(Buttons[_index]);
            SetPositionSelector(Buttons[_index]);
        }

    }

}
