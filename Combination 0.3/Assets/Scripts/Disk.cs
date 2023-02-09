using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    // Which disk this is: 0 is startline, 1 is green, 2 is red, 3 is blue, and 4 is finishline
    private int diskNumber;

    private string diskName;

    public GameObject TheBoard;

    private Board BoardScript;

    public GameObject DieRoll;

    private DieRoller DieRollerScript;

    private float currentAngle;

    public float setAngle;

    private float tileWidth;

    public GameObject higherDisk;

    public Disk upperDiskScript;

    public enum RotateMode
    {
        None,
        Left,
        Right
    }

    public int getDiskNumber()
    {
        return diskNumber;
    }

    public RotateMode rotationDirection;

    // Start is called before the first frame update
    void Start()
    {
        TheBoard = GameObject.Find("TheBoard");
        BoardScript = TheBoard.GetComponent<Board>();
        DieRollerScript = DieRoll.GetComponent<DieRoller>();
        /*rotationDirection = RotateMode.Left;*/
        determineDisk();
        if(diskNumber < 4)
            upperDiskScript = higherDisk.GetComponent<Disk>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = transform.eulerAngles.y;
        /*Debug.Log("CurrentAngle: " + currentAngle + " SetAngle: " + setAngle);*/
        if (currentAngle != setAngle && rotationDirection != RotateMode.None && diskNumber != 0 && diskNumber != 4)
        {
            if(currentAngle < (setAngle + 1) && currentAngle > (setAngle - 1))
            {
                rotationDirection = RotateMode.None;
                transform.eulerAngles = new Vector3(0, setAngle, 0);
                BoardScript.stopRotation();
                BoardScript.updateTiles();
            }
            else if (rotationDirection == RotateMode.Left)
            {
                transform.Rotate(new Vector3(0, 150, 0) * Time.deltaTime);
            }
            else if (rotationDirection == RotateMode.Right)
            {
                transform.Rotate(new Vector3(0, -150, 0) * Time.deltaTime);
            }
        }
    }

    public void RotateDiskLeft(int diskNumber)
    {
        if (BoardScript.isTheBoardMoving()) return;
        int roll = DieRollerScript.getDieValue(diskNumber);
        float tempAngle = (currentAngle + (roll * tileWidth)) % 360;
        if (tempAngle == currentAngle) return;
        BoardScript.startRotation();
        upperDiskScript.RotateDiskLeftAngle(roll * tileWidth);
        setAngle = tempAngle;
        Debug.Log("CurrentAngle: " + currentAngle + " SetAngle: " + setAngle + " roll: " + roll + " tileWidth: " + tileWidth);
        rotationDirection = RotateMode.Left;
    }

    public void RotateDiskLeftAngle(float angle)
    {
        if (diskNumber < 3)
            upperDiskScript.RotateDiskLeftAngle(angle);
        setAngle = (currentAngle + angle) % 360;
        rotationDirection = RotateMode.Left;
    }

    public void RotateDiskRight(int diskNumber)
    {
        if (BoardScript.isTheBoardMoving()) return;
        int roll = DieRollerScript.getDieValue(diskNumber);
        float tempAngle = (currentAngle + (360 - (roll * tileWidth))) % 360;
        if (tempAngle == currentAngle) return;
        BoardScript.startRotation();
        upperDiskScript.RotateDiskRightAngle(roll * tileWidth);
        setAngle = tempAngle;
        Debug.Log("CurrentAngle: " + currentAngle + " SetAngle: " + setAngle + " roll: " + roll + " tileWidth: " + tileWidth);
        rotationDirection = RotateMode.Right;
    }

    public void RotateDiskRightAngle(float angle)
    {
        if (diskNumber < 3)
            upperDiskScript.RotateDiskRightAngle(angle);
        setAngle = (currentAngle + (360 - angle)) % 360;
        rotationDirection = RotateMode.Right;
    }

    private void determineDisk()
    {
        diskNumber = (int)(transform.position.y * 10);
        if(diskNumber == 0)
        {
            diskName = "StartLine";
        } 
        else if(diskNumber == 1)
        {
            diskName = "Green";
        }
        else if(diskNumber == 2)
        {
            diskName = "Red";
        }
        else if(diskNumber == 3)
        {
            diskName = "Blue";
        }
        else if(diskNumber == 4)
        {
            diskName = "FinishLine";
        }

        tileWidth = (float)7.5;
        for (int i = diskNumber; i > 0; i--)
        {
            tileWidth = tileWidth * 2;
        }
    }
}
