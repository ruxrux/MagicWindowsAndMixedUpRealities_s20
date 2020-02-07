using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMe : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target);
    }
}
