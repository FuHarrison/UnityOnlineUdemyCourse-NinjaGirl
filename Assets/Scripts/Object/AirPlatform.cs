using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlatform : MonoBehaviour
{
    public Vector3 turnPoint;
    Vector3 targetPosition,originPosition;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position == originPosition){
            targetPosition = turnPoint;
        }else if(transform.position == turnPoint){
            targetPosition = originPosition;
        }
        transform.position = Vector3.MoveTowards(transform.position,targetPosition,moveSpeed * Time.deltaTime);
    }
}
