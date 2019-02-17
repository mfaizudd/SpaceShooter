using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    // Serialized Field
    [SerializeField]
    private float speed;
    [SerializeField]
    private Boundary boundary;
    [SerializeField]
    private float tilt;
    [SerializeField]
    private GameObject shot;
    [SerializeField]
    private Transform shotSpawn;
    [SerializeField]
    private float fireRate;

    // Private Field
    private Rigidbody rigid;
    private AudioSource audioSource;
    private float nextFire = 0.0f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
    }

    private void FixedUpdate()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigid.velocity = movement * speed;

        rigid.transform.position = new Vector3
            (
                Mathf.Clamp(rigid.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rigid.position.z, boundary.zMin, boundary.zMax)
            );

        rigid.rotation = Quaternion.Euler(0.0f, 0.0f, rigid.velocity.x * -tilt);
    }
}
