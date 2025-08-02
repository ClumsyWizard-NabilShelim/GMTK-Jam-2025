using ClumsyWizard.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : CW_Persistant<PlayerInventory>, ISceneLoadEvent
{
    private Dictionary<string, int> items = new Dictionary<string, int>();

    public bool HasItem(string item)
    {
        return items.ContainsKey(item);
    }

    public void AddItem(string item)
    {
        if (items.ContainsKey(item))
            items[item] += 1;
        else
            items.Add(item, 1);
    }

    public void RemoveItem(string item)
    {
        if (!items.ContainsKey(item))
            return;
        
        items[item] -= 1;
        if (items[item] == 0)
            items.Remove(item);
    }

    //Clean up
    public void OnSceneLoadOver(Action onComplete)
    {
        if(CW_SceneManagement.Instance.IsMenuScene)
            items = new Dictionary<string, int>();

        onComplete?.Invoke();
    }

    public void OnSceneLoadTriggered(Action onComplete)
    {
        onComplete?.Invoke();
    }
}