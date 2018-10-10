using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightTracker : MonoBehaviour
{
    private Transform targetObject; // the transform to seek towards

    [SerializeField]
    private Transform startingLocation;

    [SerializeField]
    private float lerpFactor = .3f; // amount to lerp towards the target every frame
    [SerializeField]
    private float safeDistance = .1f; // distance to start correcting position at

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(targetObject == null)
        {
            return;
        }

        Vector2 goalPos = targetObject.position; // target's position as a vec2
        Vector2 currentPos = transform.position; // current position as a vec2

        if((goalPos - currentPos).sqrMagnitude > safeDistance)
        {
            transform.position = Vector2.Lerp(currentPos, goalPos, lerpFactor);
        }
	}

    public void SetTarget(Transform target)
    {
        targetObject = target;
    }

    
}
