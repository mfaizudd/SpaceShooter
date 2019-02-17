using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject playerExplosion;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary")) return;
        Instantiate(explosion, transform.position, transform.rotation);
        if(other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            GameController.Instance.GameOver();
        }
        GameController.Instance.AddScore(10);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
