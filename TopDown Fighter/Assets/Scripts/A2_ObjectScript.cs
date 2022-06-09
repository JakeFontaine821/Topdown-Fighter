using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A2_ObjectScript : MonoBehaviour
{
    [SerializeField] float lifeSpan;
    public float lifeCounter;
    bool falling;

    // Start is called before the first frame update
    void Start()
    {
        falling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!falling)
        {
            lifeCounter += Time.deltaTime;

            if(lifeCounter >= lifeSpan)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Ground"))
        {
            falling = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
