using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EnemyEvents
{
    public static UnityEvent scoreEvent = new UnityEvent();
    public static ScoreAdvancedEvent scoreAdvancedEvent = new();
    public static HitEvent hitEvent = new();

    public static FireEvent fireEvent = new();
}

public class ScoreAdvancedEvent : UnityEvent<int> { }

public class HitEvent : UnityEvent<HitEventData> { }

public class FireEvent: UnityEvent<Vector2> { }

public class HitEventData
{
    public GameObject shooter;
    public GameObject victim;
    public GameObject bullet;

    public HitEventData(GameObject shooter, GameObject victim, GameObject bullet)
    {
        this.shooter = shooter;
        this.victim = victim;
        this.bullet = bullet;
    }
}


