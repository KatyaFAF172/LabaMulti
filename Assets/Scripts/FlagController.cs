using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
public class FlagController : NetworkBehaviour
{
    // Start is called before the first frame update
    Vector3 settedPosition;
    bool positionSetted = false;
    [SerializeField]
    private Camera cam;
    public GameObject flagHolder;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == (isServer ? "FlagServer" : "FlagClient"))
        {
            if(settedPosition != null)
            {
                other.transform.position = settedPosition;
            }
            if(flagHolder.transform.childCount == 1)
            {
                other.transform.parent = null;
            }
        }

        if (other.tag == (isServer ? "FlagClient" : "FlagServer"))
        {
            other.transform.parent = flagHolder.transform;
            other.transform.localPosition = Vector3.zero;
        }

       


    }

    private void Start()
    {
        flagHolder.transform.GetChild(0).tag = isServer ? "FlagServer" : "FlagClient";
    }

    private void Update()
    {
        if(flagHolder.transform.childCount == 1)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;

                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5.0f))
                {
                    Transform flag = flagHolder.transform.GetChild(0);
                    flag.position = detectSpawnPosition(hit.point, hit.transform.position, hit.transform.localScale, flag.localScale);
                    flag.parent = null;
                    if (!positionSetted)
                    {
                        settedPosition = flag.position;
                        positionSetted = true;
                    }
                }
            }
        }
    }

    Vector3 detectSpawnPosition(Vector3 hitPosition, Vector3 objPosition, Vector3 hittedObjScale, Vector3 creatingObjScale)
    {

        double[] diferrences = new double[3];

        diferrences[0] = Mathf.Abs(hitPosition.x - objPosition.x);
        diferrences[1] = Mathf.Abs(hitPosition.y - objPosition.y);
        diferrences[2] = Mathf.Abs(hitPosition.z - objPosition.z);

        int i = diferrences.ToList().IndexOf(diferrences.Max());

        switch (i)
        {
            case 0:
                return new Vector3(
                    objPosition.x + (hittedObjScale.x * 0.5f * (hitPosition.x - objPosition.x) > 0 ? 1 : -1),
                    objPosition.y + 1.5f,
                    objPosition.z);
            case 1:
                return new Vector3(
                    objPosition.x,
                    objPosition.y + (hittedObjScale.y * 0.5f * (hitPosition.y - objPosition.y) > 0 ? 1 : -1) + 1.5f,
                    objPosition.z);
            case 2:
                return new Vector3(
                    objPosition.x,
                    objPosition.y + 1.5f,
                    objPosition.z + (hittedObjScale.z * 0.5f * (hitPosition.z - objPosition.z) > 0 ? 1 : -1));

        }
        return Vector3.zero;
    }

}
