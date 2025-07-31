using ClumsyWizard.Core;
using System;
using System.Collections;
using UnityEngine;

public class InputManager : CW_Persistant<InputManager>, ISceneLoadEvent
{
    public Action OnPause;
    public Vector2 InputAxis { get; private set; }
    public Action OnRunStart;
    public Action OnRunEnd;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CW_HUDMenuManager.Instance.IsAnyMenuOpen)
                CW_HUDMenuManager.Instance.CloseLastMenu();
            else
                OnPause?.Invoke();
        }

        InputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.LeftShift))
            OnRunStart?.Invoke();
        if(Input.GetKeyUp(KeyCode.LeftShift))
            OnRunEnd?.Invoke();
    }

    //Clean up
    public void OnSceneLoadOver(Action onComplete)
    {
        onComplete?.Invoke();
    }

    public void OnSceneLoadTriggered(Action onComplete)
    {
        onComplete?.Invoke();
    }
}