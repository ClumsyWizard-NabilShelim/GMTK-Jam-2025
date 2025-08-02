using ClumsyWizard.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerItem
{
    ToyKey,
    FlashLight,
    Bat
}

public class PlayerInventory : CW_Persistant<PlayerInventory>, ISceneLoadEvent
{
    private Dictionary<PlayerItem, int> items = new Dictionary<PlayerItem, int>();

    public bool HasItem(PlayerItem item)
    {
        return items.ContainsKey(item);
    }

    public void AddItem(PlayerItem item)
    {
        if (items.ContainsKey(item))
            items[item] += 1;
        else
            items.Add(item, 1);
    }

    public void RemoveItem(PlayerItem item)
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
            items = new Dictionary<PlayerItem, int>();

        onComplete?.Invoke();
    }

    public void OnSceneLoadTriggered(Action onComplete)
    {
        onComplete?.Invoke();
    }
}