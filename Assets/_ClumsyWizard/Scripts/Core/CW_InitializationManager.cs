using ClumsyWizard.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface ICoreInit
{
    public void OnCoreInit();
}

public class CW_InitializationManager : CW_Singleton<CW_InitializationManager>
{
    private List<Action<Action>> initEvents;
    private int currentIndex;

    private void Start()
    {
        initEvents = new List<Action<Action>>()
        {

        };

        //currentIndex = 0;
        //initEvents[currentIndex]?.Invoke(OnEventComplete);
    }

    private void OnEventComplete()
    {
        currentIndex++;

        if (currentIndex == initEvents.Count)
            return;

        initEvents[currentIndex]?.Invoke(OnEventComplete);
    }
}