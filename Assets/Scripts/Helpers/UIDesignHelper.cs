using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIDesignHelper : MonoBehaviour
{
    [SerializeField] private Transform vect1;
    [SerializeField] private Transform vect2;
    private Transform curTrans;

    private void Start()
    {
        curTrans = vect1;
    }
    private void FixedUpdate()
    {
        print(Vector3.Distance(transform.position, curTrans.position));
        if (Vector3.Distance(transform.position, curTrans.position) >= 0.1f)
            transform.position = Vector3.MoveTowards(transform.position, curTrans.position, 1f);
        else
        {
            if (curTrans == vect1)
                curTrans = vect2;
            else
                curTrans = vect1;
        }
    }
}
