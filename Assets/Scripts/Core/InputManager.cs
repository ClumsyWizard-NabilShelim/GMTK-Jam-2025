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
    public Action OnCrouchToggle;
    public Action OnAttack;
    public Action OnJump;
    public Action OnDragDrop;

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

        if(Input.GetKeyDown(KeyCode.LeftControl))
            OnCrouchToggle?.Invoke();

        if (Input.GetKeyDown(KeyCode.Space))
            OnJump?.Invoke();

        if (Input.GetKeyDown(KeyCode.E))
            OnDragDrop?.Invoke();

        if (Input.GetMouseButtonDown(0))
            OnAttack?.Invoke();
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