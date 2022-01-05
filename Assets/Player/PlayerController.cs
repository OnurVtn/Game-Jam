using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform leftSMR;
    [SerializeField] private Transform rightSMR;
    [SerializeField] private Transform leftLimit, RightLimit;
    [SerializeField] private float forwardMovementSpeed = 1f;
    [SerializeField] private float sideMovementSensivity = 0.1f;
    [SerializeField] private bool activateOppositeMovement = true;
    [SerializeField] private float Gab = 0.1f; // only use if activate opposite movement = false

    [SerializeField] private List<Transform> playerLevelObjects;



    private Vector2 inputDrag;
    private Vector2 inputpreviousMousePosition;

    private int playerLevelCount = 0;
    private int playerLevelCountPrev = 2;
    private int playerLevelCountMax = 3;

    // Start is called before the first frame update
    void Start()
    {
        if (activateOppositeMovement)
        {
            leftSMR.localPosition = leftLimit.localPosition;
            rightSMR.localPosition = RightLimit.localPosition;
        }
        else
        {
            leftSMR.localPosition = Vector3.left * (0.5f + Gab);
            rightSMR.localPosition = Vector3.right * (0.5f + Gab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleForwardMovement();
        HandleInput();
        HandleSideMovement();
    }

    private void HandleForwardMovement()
    {
        transform.Translate(Vector3.forward * forwardMovementSpeed * Time.deltaTime);
    }

    private void HandleSideMovement()
    {
        Vector3 localPos = leftSMR.localPosition;
        localPos += Vector3.right * inputDrag.x * sideMovementSensivity;

        if (activateOppositeMovement)
        {
            localPos.x = Mathf.Clamp(localPos.x, leftLimit.localPosition.x, RightLimit.localPosition.x);
            leftSMR.localPosition = localPos;
            rightSMR.localPosition = -localPos;
        }
        else
        {
            localPos.x = Mathf.Clamp(localPos.x, leftLimit.localPosition.x, RightLimit.localPosition.x - (2 * Gab + 2 * 0.5f));
            leftSMR.localPosition = localPos;
            rightSMR.localPosition = localPos + Vector3.right * (2 * Gab + 2 * 0.5f);
        }
   
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inputpreviousMousePosition = Input.mousePosition;

        }
        if (Input.GetMouseButton(0))
        {
            Vector2 deltaMouseY = (Vector2)Input.mousePosition - inputpreviousMousePosition;
            inputDrag = deltaMouseY;
            inputpreviousMousePosition = Input.mousePosition;
        }
        else
        {
            inputDrag = Vector2.zero;
        }
    }

    private void addCube()
    {
        playerLevelCountPrev = playerLevelCount;
        playerLevelCount++;
        playerLevelCount = playerLevelCount % playerLevelCountMax;

        playerLevelObjects[playerLevelCount].gameObject.SetActive(true);
        playerLevelObjects[playerLevelCountPrev].gameObject.SetActive(false);


    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(" OnTriggerEnter" + other.gameObject.tag);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(" OnCollisionEnter" + collision.gameObject.tag);

    }
}
