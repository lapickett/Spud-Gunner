using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject gc;
    public GameObject Enemy;
    public GameObject Jumper;
    public float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.GetComponent<GameController>().wave != 0 && !gc.GetComponent<GameController>().WaveWait)
        {
            if (timer> (1 + (100/(gc.GetComponent<GameController>().wave + 10))))
            {
                if (Random.Range(0f, 1f) < 0.75 - gc.GetComponent<GameController>().wave / 100f)
                {
                    GameObject.Instantiate(Enemy, transform.position, transform.rotation);
                }
                else
                {
                    GameObject.Instantiate(Jumper, transform.position, transform.rotation);
                }
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
            
        }
    }
}
