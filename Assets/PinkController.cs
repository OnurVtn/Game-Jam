using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PinkController : MonoBehaviour
{
    private bool areTheyUnited = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("pink collision = " + other.gameObject.tag);

        if (other.CompareTag("BluePlayer"))
        {

            if (areTheyUnited == false)
            {
                this.transform.DOPunchScale(Vector3.one * 0.5f, 0.25f);
                this.gameObject.transform.localPosition = Vector3.up;

                areTheyUnited = true;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BluePlayer"))
        {
            if (areTheyUnited == true)
            {
                this.transform.DOLocalJump(Vector3.zero, 1.0f, 1, 0.2f);
                areTheyUnited = false;
            }
        }

    }

}
