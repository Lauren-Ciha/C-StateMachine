using System;
using System.Collections;
using UnityEngine;

public class Hand : State
{
    public Hand(SpriteSystem spriteSystem) : base(spriteSystem)
    {
    }

    public override IEnumerator HandCoroutine(Rigidbody rigidbody, Transform spriteTrans)
    {
        float followSpeed;

        bool on = true;

        _system.eyeContactL.lookLocation = _system.eyesLocation;
        _system.eyeContactR.lookLocation = _system.eyesLocation;
        _system.eyeContactL.eyesFollow = true;
        _system.eyeContactR.eyesFollow = true;

        while (on)
        {
            if (Vector3.Distance(rigidbody.transform.position, _system.nonDominantHand.transform.position) > _system.allowedDist)
            {
                followSpeed = Vector3.Distance(rigidbody.transform.position, _system.nonDominantHand.transform.position) > 5f ? 20.0f : 0.8f;

                Vector3 direction = _system.nonDominantHand.transform.position - rigidbody.transform.position;

                rigidbody.velocity = Vector3.Normalize(direction) * followSpeed; //set rb to interpolate for smooth movement
                rigidbody.MoveRotation(Quaternion.LookRotation(_system.eyesLocation.transform.position - rigidbody.transform.position));

                yield return new WaitForFixedUpdate();
            }

            else
            {
                _system.buttonsEnabled = true;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}