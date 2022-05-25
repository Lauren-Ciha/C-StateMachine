using System;
using System.Collections;
using UnityEngine;

public class Ascend : State
{
    public Ascend(SpriteSystem spriteSystem) : base(spriteSystem)
    {
    }

    public float ascentVelocityX = -0.1f;
    public float ascentVelocityY = 0.2f;
    public float ascentVelocityZ = 0.0f;
    public float ascentTime = 1f;
    public float waitTime = 1f;

    public override IEnumerator AscendCoroutine(Rigidbody rigidbody, Transform spriteTransform)
    {
        _system.eyesFollow = false; 

        yield return _system.StartCoroutine(Up());

        yield return new WaitForSeconds(ascentTime);

        rigidbody.velocity = new Vector3(0f, 0f, 0f);
        yield return _system.StartCoroutine(Turn(_system.DescentStart.transform, .99f));

        yield return new WaitForSeconds(waitTime);

        _system.SetState(new Flight(_system));
        _system.StartCoroutine(_system.state.FlightCoroutine(rigidbody, _system.DescentStart.transform.position));

        IEnumerator Up()
        {
            rigidbody.drag = 0.0f;
            rigidbody.velocity = new Vector3(ascentVelocityX, ascentVelocityY, ascentVelocityZ);

            yield return new WaitForFixedUpdate();
        }

        IEnumerator Turn(Transform destination, float turnSpeed)
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
}