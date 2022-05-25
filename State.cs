using System;
using System.Collections;
using UnityEngine;

public abstract class State
{
    protected readonly SpriteSystem _system;

    public State(SpriteSystem system)
    {
        _system = system;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }
    public virtual IEnumerator AscendCoroutine(Rigidbody rigidbody, Transform spriteTransform)
    {
        yield break;
    }

    public virtual IEnumerator FlightCoroutine(Rigidbody rigidbody, Vector3 destinationPosition)
    {
        yield break;
    }

    public virtual IEnumerator DescendCoroutine(Rigidbody rigidbody, Transform spriteTransform)
    {
        yield break;
    }

    public virtual IEnumerator TargetCoroutine()
    {
        yield break;
    }

    public virtual IEnumerator HandCoroutine(Rigidbody rigidbody, Transform spriteTransform)
    {
        yield break;
    }

    public virtual IEnumerator TurnCoroutine(Transform aim, float turnSpeed)
    {
        yield break;
    }
}