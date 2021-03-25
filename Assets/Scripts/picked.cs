using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class picked : MonoBehaviour
{
    public bool approached = false;
    public Behaviour halo;
    public bool thrown = false;
    public Rigidbody rb;
    public int throwCounter = 0;
    public int throwCounterMax = 5000;
    public float throwForce = 0;
    public float throwForceMax = 2000;
    // Start is called before the first frame update
    void Start()
    {
        throwForce = throwForceMax;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (thrown)
        {
            rb.AddRelativeForce(Vector3.forward * throwForce);
            if (throwForce > 0)
            {
                throwForce -= throwForceMax*2 / throwCounterMax;
            }
            throwCounter += 1;
        }

        if (throwCounter > throwCounterMax)
        {
            thrown = false;
            throwCounter = 0;
            throwForce = throwForceMax;
        }

        if (approached)
        {
            halo.enabled = true;
        }
        else
        {
            halo.enabled = false;
        }
    }
}
