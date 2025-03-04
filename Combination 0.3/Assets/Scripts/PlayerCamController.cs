using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor
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

    private static Vector3 BLUE_CAM_POSITION = new Vector3(11.5f, 8.5f, -1.5f);
    private static Vector3 BLUE_CAM_ROTATION = new Vector3(45f, -95f, 0);

    private static Vector3 GREEN_CAM_POSITION = new Vector3(-11.5f, 8.5f, 1.7f);
    private static Vector3 GREEN_CAM_ROTATION = new Vector3(45f, 86.5f, 0);

    private static Vector3 RED_CAM_POSITION = new Vector3(1.5f, 8.5f, 11.5f);
    private static Vector3 RED_CAM_ROTATION = new Vector3(45f, 175f, 0);

    private GameObject MainCamera;
    private GameObject TheBoard;

    private PlayerColor playerColor;
    private PlayerColor newPlayerColor;

    private void setCameraForColor(PlayerColor color)
    {
        switch (color)
        {
            case PlayerColor.YELLOW:
                MainCamera.transform.position = YELLOW_CAM_POSITION;
                MainCamera.transform.eulerAngles = YELLOW_CAM_ROTATION;
                break;
            case PlayerColor.BLUE:
                MainCamera.transform.position = BLUE_CAM_POSITION;
                MainCamera.transform.eulerAngles = BLUE_CAM_ROTATION;
                break;
            case PlayerColor.RED:
                MainCamera.transform.position = RED_CAM_POSITION;
                MainCamera.transform.eulerAngles = RED_CAM_ROTATION;
                break;
            case PlayerColor.GREEN:
                MainCamera.transform.position = GREEN_CAM_POSITION;
                MainCamera.transform.eulerAngles = GREEN_CAM_ROTATION;
                break;
        }
    }

    private void testCamPos()
    {
        if (Input.GetKey(KeyCode.R))
        {
            newPlayerColor = PlayerColor.RED;
        }
        else if (Input.GetKey(KeyCode.G))
        {
            newPlayerColor = PlayerColor.GREEN;
        }
        else if (Input.GetKey(KeyCode.B))
        {
            newPlayerColor = PlayerColor.BLUE;
        }
        else if (Input.GetKey(KeyCode.Y))
        {
            newPlayerColor = PlayerColor.YELLOW;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        TheBoard = GameObject.Find("TheBoard");

        playerColor = PlayerColor.BLUE;
        newPlayerColor = PlayerColor.BLUE;
        setCameraForColor(playerColor);
    }

    // Update is called once per frame
    void Update()
    {
        testCamPos();
        if (newPlayerColor == playerColor) return;
        playerColor = newPlayerColor;
        setCameraForColor(playerColor);
    }
}
