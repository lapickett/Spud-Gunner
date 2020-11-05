using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject Fry;
    GameObject gc;
    public float Life;
    public float timeToDate = 0f;
    public float multiplier = 1f;
    //public SphereCollider myCollider = transform.GetComponent<SphereCollider>();
    // Start is called before the first frame update
    void Awake()
    {
        gc = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        //expands with time
        transform.localScale = new Vector3((timeToDate * multiplier)+1, (timeToDate * multiplier)+1, (timeToDate * multiplier)+1);
        timeToDate += Time.deltaTime;
        if (timeToDate > Life)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider Col)
    {
        //when colliding with an enemy, it will kill that enemy and add points
        if (Col.tag == "Enemy")
        {
            Vector3[] Offset = new Vector3[] { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 0, 1) };
            for (int i=0; i<3; i++)
            {
                GameObject fry = GameObject.Instantiate(Fry, Col.transform.position + Offset[i], Col.transform.rotation);
                fry.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
            }
            gc.GetComponent<GameController>().Score += 5;
            Destroy(Col.gameObject);

        }
        //when colliding with player, boosts player upward
        if (Col.tag == "Player")
        {
            Knockback(Col);
        }
    }

    void Knockback(Collider col)
    {
        Vector3 knock = new Vector3(0,10,0);
        col.gameObject.GetComponent<FPScontroller>().moveDirection.y += 6f *(1.5f - timeToDate/Life);
    }
}
