using System;
using System.Collections;
using UnityEngine;

public class Descend : State
{
    public Descend(SpriteSystem spriteSystem) : base(spriteSystem)
    {
    }

    public override IEnumerator DescendCoroutine(Rigidbody rigidbody, Transform spriteTransform)
    {
        float descentSpeed = .15f;

        rigidbody.drag = 0f;

        yield return _system.StartCoroutine(Turn(rigidbody, _system.eyesLocation, 2f, spriteTransform));

        while (Vector3.Distance(_system.target.transform.position, spriteTransform.position) > .005f)
        {

            Vector3 descentDirection = _system.target.transform.position - spriteTransform.position;
            rigidbody.velocity = descentDirection.normalized * descentSpeed; 

            yield return new WaitForFixedUpdate();
        }

        rigidbody.velocity = new Vector3(0f, 0f, 0f);

        spriteTransform.position = _system.target.transform.position;

        _system.SetState(new Target(_system));

        _system.StartCoroutine(_system.state.TargetCoroutine());
    }

    IEnumerator Turn(Rigidbody rigidbody, Transform destination, float turnSpeed, Transform spriteTransform)
    {
        Vector3 targetDirection = destination.position - rigidbody.transform.position;
        float angle = Vector3.SignedAngle(rigidbody.transform.forward, targetDirection, Vector3.up);

        while (angle > 5f || angle < -5f)
        {
            Quaternion targetRot = Quaternion.LookRotation(targetDirection);
            float strength = Mathf.Min(turnSpeed * Time.fixedDeltaTime, 1);
            spriteTransform.rotation = Quaternion.Slerp(spriteTransform.rotation, targetRot, strength);

            angle = Vector3.SignedAngle(rigidbody.transform.forward, targetDirection, Vector3.up);
            yield return new WaitForFixedUpdate();
        }
    }
}