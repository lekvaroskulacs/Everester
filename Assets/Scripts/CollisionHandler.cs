using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private List<GameObject> disableOnCrash = new List<GameObject>();
    private PlayerInput movement;

    private void Start()
    {
        movement = GetComponent<PlayerInput>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"'{collision.gameObject.name}' collided with '{gameObject.name}'");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"'{other.gameObject.name}' triggered '{gameObject.name}'");
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        movement.enabled = false;
        explosion.Play();
        foreach (GameObject go in disableOnCrash)
        {
            go.SetActive(false);
        }
        Invoke("ProcessCrash", 1);

    }

    private void ProcessCrash()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
