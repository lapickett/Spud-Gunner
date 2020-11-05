using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingObjFace : MonoBehaviour
{
    public Transform ObjToFollow = null;
    public bool FollowPlayer = false;
    public Rigidbody rb;
    public float speed = 10;
    public float jumpSpeed = 80;
    public bool jumping = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (!FollowPlayer) { return; }
        GameObject PlayerObj = GameObject.FindGameObjectWithTag("Player");
        if (PlayerObj != null)
        {
            ObjToFollow = PlayerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //makes sure to always face and move towards the player object, but jumps as well, making it harder to hit
        if (ObjToFollow == null)
        {
            return;
        }
        Vector3 DirToObject = ObjToFollow.position - transform.position;
        if (DirToObject != Vector3.zero)
        {
            transform.localRotation = Quaternion.LookRotation(DirToObject.normalized, Vector3.up);
        }
        Vector3 v3Force = speed * transform.forward;
        rb.AddForce(v3Force * Time.deltaTime);
        if(rb.velocity.y < 0.05 && rb.velocity.y > -0.05 && !jumping)
        {
            Vector3 jump = new Vector3(0, jumpSpeed, 0);
            rb.AddForce(jump);
            jumping = true;
        }
        if (jumping && rb.velocity.y < -0.1)
        {
            jumping = false;
        }
    }
}
