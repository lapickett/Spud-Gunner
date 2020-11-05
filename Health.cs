using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject DeathParticlesPrefab = null;
    public bool ShouldDestroyOnDeath = true;
    [SerializeField] private float _HealthPoints = 100f;
    public GameObject gc;

    //keeps track of the health of the player
    public float HealthPoints
    {
        get { return _HealthPoints; }
        set
        {
            _HealthPoints = value;
            if (HealthPoints <= 0)
            {
                gc.GetComponent<GameController>().HasStarted = false;
            }
        }
    }
}