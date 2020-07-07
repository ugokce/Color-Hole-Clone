using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventWithVector3 : UnityEvent<Vector3>
{}

public class EventWithFloat : UnityEvent<float>
{}

public class EventWithInt : UnityEvent<int>
{ }

public class PlayerEvents
{
    public EventWithVector3 onPlayerInput;
    public EventWithVector3 onDestinationTargetSelected;
    public UnityEvent onGateOpened;
    public UnityEvent onSubLevelCleared;
    public EventWithVector3 onCameraShouldFollowPlayer;
    public UnityEvent onPlayerFailed;

    //completed level id
    public EventWithInt onLevelCompleted;

    public PlayerEvents()
    {
        onPlayerInput = new EventWithVector3();
        onDestinationTargetSelected = new EventWithVector3();
        onGateOpened = new UnityEvent();
        onSubLevelCleared = new UnityEvent();
        onLevelCompleted = new EventWithInt();
        onCameraShouldFollowPlayer = new EventWithVector3();
    }
}
