using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform leftSMR;
    [SerializeField] private Transform rightSMR;
    [SerializeField] private Transform RotateManager;
    [SerializeField] private Transform leftLimit, RightLimit;
    [SerializeField] private float forwardMovementSpeed = 1f;
    [SerializeField] private float sideMovementSensivity = 0.1f;
    [SerializeField] private bool activateOppositeMovement = true;
    [SerializeField] private float Gab = 0.1f; // only use if activate opposite movement = false

    [SerializeField] private List<Transform> pathObjects;
    private Vector3[] pathPoints;

    [SerializeField] private List<Transform> playerLevelObjects;

    private static PlayerController instance;
    public static PlayerController Instance => instance ?? (instance = FindObjectOfType<PlayerController>());

    private Vector2 inputDrag;
    private Vector2 inputpreviousMousePosition;

    private int playerLevelCount = 0;
    private int playerLevelCountPrev = 2;
    private int playerLevelCountMax = 3;

    private bool updatePose = true;
    private bool switchControl = false;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

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


        pathPoints = new Vector3[pathObjects.Count];
         
        for (int i = 0; i<pathObjects.Count; i++)
        {
            pathPoints[i] = pathObjects[i].position;
        }

        transform.DOPath(pathPoints, forwardMovementSpeed).SetSpeedBased().SetEase(Ease.Linear);
        
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
        //transform.Translate(Vector3.forward * forwardMovementSpeed * Time.deltaTime);
    }

    private void HandleSideMovement()
    {
        if (activateOppositeMovement)
        {
            
            if (updatePose)
            {
                if (switchControl)
                {
                    Vector3 localPos = rightSMR.localPosition;
                    localPos += Vector3.right * inputDrag.x * sideMovementSensivity;
                    localPos.x = Mathf.Clamp(localPos.x, leftLimit.localPosition.x, RightLimit.localPosition.x);

                    leftSMR.localPosition = -localPos;
                    rightSMR.localPosition = localPos;
                    Debug.Log("true");
                }
                else
                {
                    Vector3 localPos = leftSMR.localPosition;
                    localPos += Vector3.right * inputDrag.x * sideMovementSensivity;
                    localPos.x = Mathf.Clamp(localPos.x, leftLimit.localPosition.x, RightLimit.localPosition.x);

                    leftSMR.localPosition = localPos;
                    rightSMR.localPosition = -localPos;
                    Debug.Log("false");
                }
            }
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

    public void SwitchPositionsStart()
    {
        updatePose = false;

        if (switchControl)
        {
            RotateManager.DORotate(Vector3.up, 0.25f).OnComplete(() => SwitchPositionsStop());
            switchControl = false;
        }
        else
        {
            RotateManager.DORotate(Vector3.up * 180, 0.25f).OnComplete(() => SwitchPositionsStop());
            switchControl = true;
        }

        leftSMR.transform.DORotate(Vector3.up * 0, 0.25f);
        rightSMR.transform.DORotate(Vector3.up * 0, 0.25f);

    }

    public void SwitchPositionsStop()
    {
        float leftLocalPosX = leftSMR.localPosition.x;
        float rightLocalPosX = rightSMR.localPosition.x;

        leftSMR.transform.DOLocalMoveX(leftLocalPosX - 0.25f, 0.05f);
        rightSMR.transform.DOLocalMoveX(rightLocalPosX + 0.25f, 0.05f);

        updatePose = true;
    }

}
