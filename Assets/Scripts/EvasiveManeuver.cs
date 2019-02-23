using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour
{

    [SerializeField]
    private float dodge;
    [SerializeField]
    private float tilt;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 startWait;
    [SerializeField]
    private Vector2 maneuverTime;
    [SerializeField]
    private Vector2 maneuverWait;
    [SerializeField]
    private Boundary boundary;

    private float currentZSpeed;
    private float targetManeuver;
    private Rigidbody rb;
    private Transform playerTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentZSpeed = rb.velocity.z;
        StartCoroutine(Evade());
        playerTransform = GameObject.FindWithTag("Player")?.transform;
    }

    private IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while(true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0.0f;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }

    private void FixedUpdate()
    {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * speed);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentZSpeed);
        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);

        if(targetManeuver==0.0f && playerTransform!=null)
        {
            transform.LookAt(playerTransform);
            transform.Rotate(0.0f, 180f, 0.0f);
        }

    }

}
