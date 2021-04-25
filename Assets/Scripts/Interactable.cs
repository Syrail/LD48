using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractionType
    {
        Thrusters,
        Boost,
        Repair,
        Upgrade
    }
    
    public InteractionType interaction;
    public int slot;
}
