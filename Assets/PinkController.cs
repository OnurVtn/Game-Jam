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

            //if (areTheyUnited == false)
            //{
            //    this.transform.DOPunchScale(Vector3.one * 0.5f, 0.25f);
            //    this.gameObject.transform.localPosition = Vector3.up;

            //    areTheyUnited = true;
            //}
        }

        if (other.CompareTag("SwitchLimit"))
        {

            //Vector3[] path = new Vector3[] { new Vector3(1, 0, 1), new Vector3(0, 0, 0) };
            //this.transform.DOLocalPath(path, 1f).OnStart(() => PlayerController.Instance.SwitchPositionsStart()).OnComplete(() => PlayerController.Instance.SwitchPositionsStop());
            //this.transform.DOLocalMove(Vector3.zero, 0.25f);

            PlayerController.Instance.SwitchPositionsStart();
            //transform.DORotate(Vector3.up * 0, 0.25f);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SwitchLimit"))
        {
            
        }
    }


}
