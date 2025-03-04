using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    YELLOW,
    BLUE,
    RED,
    GREEN
}

public class PlayerCamController : MonoBehaviour
{
    private static Vector3 YELLOW_CAM_POSITION = new Vector3(-1.5f, 8.5f, -11.5f);
    private static Vector3 YELLOW_CAM_ROTATION = new Vector3(45f, -4.5f, 0);

    private static Vector3 BLUE_CAM_POSITION = new Vector3(12f, 8.5f, -1f);
    private static Vector3 BLUE_CAM_ROTATION = new Vector3(45f, -95f, 0);

    private static Vector3 GREEN_CAM_POSITION = new Vector3(-11.7f, 8.5f, 1.8f);
    private static Vector3 GREEN_CAM_ROTATION = new Vector3(45f, 86.5f, 0);

    private static Vector3 RED_CAM_POSITION = new Vector3(1.5f, 8.5f, 11.5f);
    private static Vector3 RED_CAM_ROTATION = new Vector3(45f, 175f, 0);

    private GameObject MainCamera;
    private GameObject TheBoard;

    private PlayerType playerType;


    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        TheBoard = GameObject.Find("TheBoard");

        playerType = PlayerType.BLUE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            playerType = PlayerType.RED;
        }
        else if (Input.GetKey(KeyCode.G))
        {
            playerType = PlayerType.GREEN;
        }
        else if (Input.GetKey(KeyCode.B))
        {
            playerType = PlayerType.BLUE;
        }
        else if (Input.GetKey(KeyCode.Y))
        {
            playerType = PlayerType.YELLOW;
        }

        switch (playerType)
        {
            case PlayerType.YELLOW:
                MainCamera.transform.position = YELLOW_CAM_POSITION;
                MainCamera.transform.eulerAngles = YELLOW_CAM_ROTATION;
                break;
            case PlayerType.BLUE:
                MainCamera.transform.position = BLUE_CAM_POSITION;
                MainCamera.transform.eulerAngles = BLUE_CAM_ROTATION;
                break;
            case PlayerType.RED:
                MainCamera.transform.position = RED_CAM_POSITION;
                MainCamera.transform.eulerAngles = RED_CAM_ROTATION;
                break;
            case PlayerType.GREEN:
                MainCamera.transform.position = GREEN_CAM_POSITION;
                MainCamera.transform.eulerAngles = GREEN_CAM_ROTATION;
                break;
        }
    }
}
