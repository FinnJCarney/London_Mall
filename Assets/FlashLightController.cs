using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{
    // Start is called before the first frame update
    Light light;
    bool on;

    void Start()
    {
        on = true;
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ChangeLight(on);
        }
    }

    void ChangeLight(bool i)
    {
        on = !i;
        light.enabled = !i;
    }
}