using ClumsyWizard.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum ControlRestriction
{
    None,
    All,
    Walk,
    Run,
    Jump,
    Crouch,
    Attack,
    FlashLight
}

public class PlayerRestrictionManager : CW_Singleton<PlayerRestrictionManager>
{
    [SerializeField] private List<ControlRestriction> restrictions = new List<ControlRestriction>();

    public bool IsRestricted(ControlRestriction restriction)
    {
        if (restrictions.Contains(ControlRestriction.All))
            return true;

        return restrictions.Contains(restriction);
    }

    public void AddRestriction(ControlRestriction restriction)
    {
        if(restriction == ControlRestriction.None) 
            return;

        restrictions.Add(restriction);
    }

    public void RemoveRestriction(ControlRestriction restriction)
    {
        if (restriction == ControlRestriction.None)
            return;

        if (!restrictions.Contains(restriction))
            return;

        restrictions.Remove(restriction);
    }
}