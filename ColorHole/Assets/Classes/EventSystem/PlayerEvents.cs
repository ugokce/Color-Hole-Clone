using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventWithVector3 : UnityEvent<Vector3>
{}

public class EventWithFloat : UnityEvent<float>
{}

public class PlayerEvents
{
    public EventWithVector3 onPlayerInput;
    public EventWithVector3 onDestinationTargetSelected;

    public PlayerEvents()
    {
        onPlayerInput = new EventWithVector3();
        onDestinationTargetSelected = new EventWithVector3();
    }
}
