using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventWithVector3 : UnityEvent<Vector3>
{}

public class EventWithFloat : UnityEvent<float>
{}

public class GenericEventWithList<T> : UnityEvent<List<T>>
{}

public class EventWithInt : UnityEvent<int>
{ }

public class PlayerEvents
{
    public EventWithVector3 onPlayerInput;

    public UnityEvent onGateOpened;
    public UnityEvent onSubLevelCleared;
    public UnityEvent onPlayerCompletedSubLevelAnim;
    public UnityEvent onPlayerFailed;
    public UnityEvent onRestartGame;
    public UnityEvent onObjectCollected;
    public EventWithFloat onProgressionUpdate;
    public EventWithInt onLevelCompleted;

    public PlayerEvents()
    {
        onPlayerInput = new EventWithVector3();
        onGateOpened = new UnityEvent();
        onSubLevelCleared = new UnityEvent();
        onPlayerCompletedSubLevelAnim = new UnityEvent();
        onLevelCompleted = new EventWithInt();
        onRestartGame = new UnityEvent();
        onPlayerFailed = new UnityEvent();
        onObjectCollected = new UnityEvent();
        onProgressionUpdate = new EventWithFloat();
    }
}
