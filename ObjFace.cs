using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFace : MonoBehaviour
{
    public Transform ObjToFollow = null;
    public bool FollowPlayer = false;
    public Rigidbody rb;
    public float speed = 10;
    // Start is called before the first frame update
    void Awake()
    {
        if (!FollowPlayer) { return; }
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
        if(PlayerObj != null)
        {
            ObjToFollow = PlayerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //makes sure to always face and move towards the player object
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
        if (PlayerObj != null)
        {
            ObjToFollow = PlayerObj.transform;
        }
        if (ObjToFollow == null)
        {
            return;
        }
        Vector3 DirToObject = ObjToFollow.position - transform.position;
        if(DirToObject != Vector3.zero)
        {
            transform.localRotation = Quaternion.LookRotation(DirToObject.normalized, Vector3.up);
        }
        Vector3 v3Force = speed * transform.forward;
        rb.AddForce(v3Force * Time.deltaTime);
    }
}
