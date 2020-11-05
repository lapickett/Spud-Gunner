using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public float Life;
    public float timeToDate = 0f;
    public Rigidbody rb;
    public GameObject ExplosionSphere;
    public GameObject Fry;
    GameObject gc;
    

    // Start is called before the first frame update
    void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        //moves shot forward, ends objects life if too long
        rb.velocity = transform.forward * Speed;
        timeToDate += Time.deltaTime;
        if (timeToDate > Life)
        {
            Explode();
        }
    }

    void OnTriggerStay(Collider Col)
    {
        //when colliding with an enemy, it will kill that enemy and add points
        if (Col.tag == "Enemy")
        {
            Vector3[] Offset = new Vector3[] { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 0, 1) };
            for (int i = 0; i < 2; i++)
            {
                GameObject fry = GameObject.Instantiate(Fry, Col.transform.position + Offset[i], Col.transform.rotation);
                fry.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
            }
            
            gc.GetComponent<GameController>().Score += 5;
            print(gc.GetComponent<GameController>().Score);
            Destroy(Col.gameObject);
        }
        Explode();
    }

    void Explode()
    {
        GameObject.Instantiate(ExplosionSphere, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
