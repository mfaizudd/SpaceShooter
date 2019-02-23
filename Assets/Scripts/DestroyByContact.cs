using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private GameObject playerExplosion;
    [SerializeField]
    private int score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy")) return;
        if (explosion != null) Instantiate(explosion, transform.position, transform.rotation);
        if (other.CompareTag("Player"))
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            GameController.Instance.GameOver();
        }
        GameController.Instance.AddScore(score);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
