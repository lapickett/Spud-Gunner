using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyDamage : MonoBehaviour
{
    public float DamageRate = 10f;
    GameObject gc;

    //potatoes method of damage is via contact over time
    void OnTriggerStay(Collider Col)
    {
        Health H = Col.gameObject.GetComponent<Health>();
        if(H == null)
        {
            return;
        }
        H.HealthPoints -= DamageRate * Time.deltaTime;
    }
    void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
    }
    void Update()
    {
        if (!gc.GetComponent<GameController>().HasStarted)
        {
            Destroy(gameObject);
        }
    }
}
