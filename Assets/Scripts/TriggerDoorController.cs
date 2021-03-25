using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorController : MonoBehaviour
{
    public GameObject myDoor = null;
    public bool open = false;
    //public bool opened = false;
    public float openAngle = 0;
    public Material green;
    private MeshRenderer mr;

    public string trigTag;
    public float endAngle;
    public float diffAngle;

    void Start()
    {
        //anim = myDoor.GetComponent<Animator>();
        mr = GetComponent<MeshRenderer>();

        mr.enabled = false;
    }
    void FixedUpdate()
    {
        if (open && openAngle < endAngle)
        {
            mr.material = green;
            if (openAngle < endAngle)
            {
                myDoor.transform.Rotate(new Vector3(0,diffAngle,0));
                openAngle += diffAngle;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(trigTag))
        {
            open = true; 
        }
    }
}