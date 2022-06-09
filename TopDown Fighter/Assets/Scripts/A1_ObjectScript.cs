using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A1_ObjectScript : MonoBehaviour
{
    [SerializeField] float lifeSpan;
    float lifeCounter;

    // Update is called once per frame
    void Update()
    {
        lifeCounter += Time.deltaTime;

        if(lifeCounter >= lifeSpan)
        {
            Destroy(gameObject);
        }
    }
}
