using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 20;

    [SerializeField] private ParticleSystem explosion;
    private Transform parent;
    [SerializeField] private ParticleSystem hitFX;
    private ScoreBoard scoreboard;

    

    private void Start()
    {
        scoreboard = FindObjectOfType<ScoreBoard>();
        parent = GameObject.FindGameObjectWithTag("VFXparent").GetComponent<Transform>();
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit(other);
        if (health <= 0)
            DestructionSequence();

    }

    private void ProcessHit(GameObject other)
    {
        scoreboard.IncreaseScore();
        health--;

        List<ParticleCollisionEvent> events = new List<ParticleCollisionEvent>();
        other?.GetComponent<ParticleSystem>().GetCollisionEvents(gameObject, events);
        
        var fx = Instantiate(hitFX, events[0].intersection, Quaternion.identity);
        fx.transform.parent = parent;
    }

    private void DestructionSequence()
    {
        ParticleSystem vfx = Instantiate(explosion, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;

        Destroy(gameObject);
    }
}
