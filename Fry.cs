using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fry : MonoBehaviour
{
    public Transform ObjToFollow = null;
    public Rigidbody rb;
    public float speed = 10;
    public float timer = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
        if (PlayerObj != null)
        {
            ObjToFollow = PlayerObj.transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //falls to the ground, but after some time, turns to face player and shoots like missile
        timer += Time.deltaTime;
        if (timer > 1.5f && timer < 2.0f)
        {
            if (ObjToFollow == null)
            {
                return;
            }
            Vector3 DirToObject = ObjToFollow.position - transform.position;
            if (DirToObject != Vector3.zero)
            {
                //transform.position.y += 2f;
                transform.localRotation = Quaternion.LookRotation(DirToObject.normalized, Vector3.up);
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
            }
        }
        else if (timer > 2.0f)
        {
            Vector3 v3Force = speed * transform.forward;
            rb.AddForce(v3Force * Time.deltaTime);
        }


        if (timer > 5.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay(Collider Col)
    {
        //does small chip damage to player if collided with
        if (Col.tag == "Player" && timer > 2.0f)
        {
            Health H = Col.gameObject.GetComponent<Health>();
            if (H == null)
            {
                return;
            }
            H.HealthPoints -= 2f;
        }
        if (Col.tag != "Fry" && timer > 2.0f && Col.tag != "UnFryable")
        {
            Destroy(gameObject);
        }
    }
}
