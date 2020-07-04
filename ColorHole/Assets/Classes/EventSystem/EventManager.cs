using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private static EventManager _instance;

    public PlayerEvents playerEvents;

    private EventManager()
    {
        playerEvents = new PlayerEvents();
    }

    public static EventManager getInstance()
    {
        if(_instance == null)
        {
            _instance = new EventManager();
            return _instance;
        }
        else
        {
            return _instance;
        }
    }
}
