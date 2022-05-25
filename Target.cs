using System;
using System.Collections;
using UnityEngine;

public class Target : State
{
    public Target(SpriteSystem spriteSystem) : base(spriteSystem)
    {
    }

    public override IEnumerator TargetCoroutine()
    {
        _system.eyeContactL.lookLocation = _system.eyesLocation;
        _system.eyeContactR.lookLocation = _system.eyesLocation;
        _system.eyesFollow = true;
        _system.buttonsEnabled = true;
        yield return null; 
    }
}
