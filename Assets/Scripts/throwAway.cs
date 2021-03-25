using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwAway : MonoBehaviour
{
    public bool picked = false;
    public GameObject thisBall;
    public GameObject cam;
    public Camera realCam;
    public bool inHand = false;
    public GameObject handBall;
    public int throwCounter = 0;
    public bool throwing = false;
    public GameObject sight;
    private GameObject dia;

    private MeshRenderer gunMR;
    private ShootTeleProjectile STP;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        realCam = cam.GetComponent<Camera>();
        GameObject d = Instantiate(sight, new Vector3(0,0,0),Quaternion.Euler(0,0,0));
        d.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, Camera.main.nearClipPlane + 1));
        dia = d;
        dia.transform.parent = cam.transform;
        dia.SetActive(false);

        STP = GetComponent<ShootTeleProjectile>();
        gunMR = GameObject.Find("gunDesignFBX").GetComponent<MeshRenderer>();
    }

    // Update is called once per frame

    
    void Update()
    {
        dia.transform.forward = cam.transform.forward;

        if (!picked && thisBall != null && !inHand)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                picked = true;
                thisBall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width*5 / 6, Screen.height / 5, Camera.main.nearClipPlane+1));
                thisBall.transform.parent = cam.transform;
                thisBall.transform.forward = cam.transform.forward;
                Rigidbody rb = thisBall.GetComponent<Rigidbody>();
                BoxCollider bc = thisBall.GetComponent<BoxCollider>();
                bc.enabled = false;
                rb.isKinematic = true;
                picked ballP = thisBall.GetComponent<picked>();
                ballP.approached = false;
                inHand = true;
                handBall = thisBall;
                throwing = true;
                dia.SetActive(true);
            }
        }
        if (picked && inHand && handBall != null)
        {
            STP.enabled = false;
            gunMR.enabled = false;
            if (Input.GetMouseButtonDown(0))
            {
                picked = false;
                Rigidbody rb = thisBall.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                BoxCollider bc = thisBall.GetComponent<BoxCollider>();
                bc.enabled = true;
                handBall.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane + 1));
                handBall.transform.parent = null;
                picked ballP = handBall.GetComponent<picked>();
                ballP.thrown = true;
                handBall = null;
                inHand = false;
                throwing = true;
                dia.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.E) && !throwing)
            {
                picked = false;
                Rigidbody rb = thisBall.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                BoxCollider bc = thisBall.GetComponent<BoxCollider>();
                bc.enabled = true;
                handBall.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z) + (cam.transform.forward * 1/2);
                handBall.transform.parent = null;
                handBall = null;
                inHand = false;
                throwing = true;
                dia.SetActive(false);
            }
        }
        else
        {
            STP.enabled = true;
            gunMR.enabled = true;
        }
    }
    void FixedUpdate()
    {
        if (throwing)
        {
            throwCounter += 1;
        }
        if (throwCounter > 10)
        {
            throwing = false;
            throwCounter = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "pickup" && !picked && !inHand && !throwing)
        {
            picked p = other.gameObject.GetComponent<picked>();
            p.approached = true;
            thisBall = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "pickup" && !picked && !inHand && !throwing)
        {
            picked p = other.gameObject.GetComponent<picked>();
            p.approached = true;
            thisBall = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "pickup" && !inHand)
        {
            picked p = other.gameObject.GetComponent<picked>();
            p.approached = false;
            thisBall = null;
        }
    }

}
