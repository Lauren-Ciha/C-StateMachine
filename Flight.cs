using System;
using System.Collections;
using UnityEngine;

public class Flight : State
{
    public Flight(SpriteSystem spriteSystem) : base(spriteSystem)
    {
    }

    public override IEnumerator FlightCoroutine(Rigidbody rigidbody, Vector3 destinationPosition)
    {
        float flightForce = .15f;
        float dragMult = 27.5f;
        float flightTurnSpeed = 2f;
        Vector3 spritePosition = _system.transform.position;
        Quaternion spriteRotation = _system.transform.rotation;

        if (_system.doNotContinue)
        {
            yield return _system.StartCoroutine(Turn(destinationPosition, flightTurnSpeed));
        }

        while (Vector3.Distance(destinationPosition, _system.transform.position) > 0.01f)
        {
            Vector3 targetDirectionection = destinationPosition - _system.transform.position;
            rigidbody.AddForce(targetDirectionection.x * flightForce, targetDirectionection.y *
                flightForce, targetDirectionection.z * flightForce, ForceMode.VelocityChange);

            rigidbody.drag = dragMult * flightForce;

            yield return new WaitForFixedUpdate();

        }

        rigidbody.velocity = new Vector3(0f, 0f, 0f);

        if (_system.toHand)
        {
            _system.SetState(new Hand(_system));

            _system.groundRen.material.color = Color.yellow;

            _system.StartCoroutine(_system.state.HandCoroutine(rigidbody, _system.transform));
        }

        else if (!_system.doNotContinue)
        {
            _system.SetState(new Descend(_system));

            _system.StartCoroutine(_system.state.DescendCoroutine(rigidbody, _system.transform));
        }

        else
        {
            _system.SetState(new Target(_system));

            _system.groundRen.material.color = Color.blue;

            _system.StartCoroutine(_system.state.TargetCoroutine());
        }

        IEnumerator Turn(Vector3 destination, float turnSpeed)
        {
            Vector3 targetDirection = destination - spritePosition;
            float angle = Vector3.SignedAngle(rigidbody.transform.forward, targetDirection, Vector3.up);

            while (angle > 5f || angle < -5f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                float strength = Mathf.Min(turnSpeed * Time.fixedDeltaTime, 1);
                spriteRotation = Quaternion.Slerp(spriteRotation, targetRotation, strength);

                angle = Vector3.SignedAngle(rigidbody.transform.forward, targetDirection, Vector3.up);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}