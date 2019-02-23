using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRoller : MonoBehaviour
{

    [SerializeField]
    private float scrollSpeed;

    private Vector3 startPosition;
    private float tileScaleZ;

    private void Start()
    {
        tileScaleZ = transform.localScale.y;
        startPosition = transform.position;
    }   

    private void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileScaleZ);
        transform.position = startPosition + Vector3.forward * newPosition;
    }

}
