using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject anchor;
    bool uiActive = false;

    private void Start()
    {
        inventory.SetActive(false);
        uiActive = false;
    }

    private void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Four))
        {
            uiActive = !uiActive;
            inventory.SetActive(uiActive);
        }

        if(uiActive)
        {
            inventory.transform.position = anchor.transform.position;
            inventory.transform.eulerAngles = new Vector3(anchor.transform.eulerAngles.x + 15, anchor.transform.eulerAngles.y, 0);
        }
    }
}
