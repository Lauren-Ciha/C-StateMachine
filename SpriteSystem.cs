using System;
using System.Collections;
using UnityEngine;

public class SpriteSystem : MonoBehaviour
{
    [Header("General Settings")]
    public Rigidbody rb;
    public EyeContact eyeContactR;
    public EyeContact eyeContactL;
    public Transform eyesLocation;
    public bool eyesFollow = false;
    public GameObject player;
    public bool buttonsEnabled = true;
    public bool toHand = false;

    [Header("Hand Settings")]
    public Transform dominantHand;
    public float dominantHandRange;
    public GameObject nonDominantHand;
    public float allowedDist = 0.05f;

    [Header("Descend Settings")]
    public GameObject DescentStart;

    [Header("Target Settings")]
    public Transform target;

    [Header("Temporary Settings")]
    public Vector3 destination;
    public Renderer groundRen;
    public bool doNotContinue; //for testing the flight state only

    public State state;

    public void SetState(State currState)
    {
        state = currState;
    }

    public void Update()
    {
        /*
        OVRInput.Update();

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) == 1f)
        {
            if (buttonsEnabled)
            {
                StopAllCoroutines();

                SetState(new Flight(this));
                toHand = true;
                StartCoroutine(state.FlightCoroutine(rb, nonDominantHand.transform.position));
                buttonsEnabled = false;
            }
        }
        

        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) == 1f)
        {
            if (buttonsEnabled)
            {
                StopAllCoroutines();

                SetState(new Ascend(this));
                StartCoroutine(state.AscendCoroutine(rb, transform));
                buttonsEnabled = false; 
            }
        }

        if (OVRInput.Get(OVRInput.Button.One))
        {
            if (buttonsEnabled)
            {
                doNotContinue = true;
                StopAllCoroutines();
                SetState(new Flight(this));
                StartCoroutine(state.FlightCoroutine(rb, DescentStart.transform.position));
                buttonsEnabled = false; 
            }
        }
        */
    }
}