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

    private int debugCounter;

    private bool currentAngleStartedBelowSetAngle;

    private bool currentAngleHasPassedZero;

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
        // TODO
        //  have the disk recognize which disk number it is somehow 
        //  automatically so I don't have to set that shit up every time
        TheBoard = GameObject.Find("TheBoard");
        DieRoll = GameObject.Find("Die Roller");
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
        /// currentAngle = transform.eulerAngles.y;
        /// Debug.Log("CurrentAngle: " + currentAngle + " SetAngle: " + setAngle);
        /// if (diskNumber != (0|4) && currentAngle != setAngle && rotationDirection != RotateMode.None)
        /// {
        ///     if(currentAngle < (setAngle + 2) && currentAngle > (setAngle - 2))
        ///     {
        ///         rotationDirection = RotateMode.None;
        ///         transform.eulerAngles = new Vector3(0, setAngle, 0);
        ///         BoardScript.stopRotation();
        ///         BoardScript.updateTiles();
        ///     }
        ///     else if (rotationDirection == RotateMode.Left)
        ///     {   
        ///         transform.Rotate(new Vector3(0, 150, 0) * Time.deltaTime);
        ///     }
        ///     else if (rotationDirection == RotateMode.Right)
        ///     {
        ///         transform.Rotate(new Vector3(0, -150, 0) * Time.deltaTime);
        ///     }
        /// }
        if (diskNumber != (0|4) && rotationDirection != RotateMode.None)
        {
            if(rotationDirection == RotateMode.Left)
            {
                if(currentAngleHasPassedSetAngleDirectionLeft()){
                    rotationDirection = RotateMode.None;
                    //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, setAngle, transform.rotation.eulerAngles.z);
                    transform.eulerAngles = new Vector3(0, setAngle, 0);
                    BoardScript.stopRotation();
                    return;
                }
                if(BoardScript.isTheBoardMoving())
                    return;
                transform.Rotate(new Vector3(0, 150, 0) * Time.deltaTime);
                return;
            }
            if(rotationDirection == RotateMode.Right)
            {
                if(currentAngleHasPassedSetAngleDirectionRight()){
                    rotationDirection = RotateMode.None;
                    transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, setAngle, transform.rotation.eulerAngles.z);
                    BoardScript.stopRotation();
                    return;
                }
                if(BoardScript.isTheBoardMoving())
                    return;
                transform.Rotate(new Vector3(0, -150, 0) * Time.deltaTime);
                return;
            }
        }
    }

    /// <summary>
    /// This function is used to check if the current angle has passed the set angle            <br></br>
    /// in the left direction, and manages the currentAngleHasPassedZero variable.              <br></br>
    /// When checking rotation, we must be aware that the currentAngle might be above           <br></br>
    /// or below the setangle, and we must check for both cases. The currentAngleHasPassedZero  <br></br>
    /// and currentAngleStartedBelowSetAngle variables are used to keep track of this.          <br></br>
    /// </summary>
    private bool currentAngleHasPassedSetAngleDirectionLeft()
    {
        Debug.Log("CurrentAngle: " + transform.eulerAngles.y + " SetAngle: " + setAngle);
        if (transform.eulerAngles.y >= setAngle && (currentAngleStartedBelowSetAngle || currentAngleHasPassedZero))
        {
            return true;
        }
        if(!currentAngleStartedBelowSetAngle && !currentAngleHasPassedZero)
        {
            currentAngleHasPassedZero = true;
        }
        return false;
    }

    /// <summary>
    /// This function is used to check if the current angle has passed the set angle            <br></br>
    /// in the right direction, and manages the currentAngleHasPassedZero variable.             <br></br>
    /// When checking rotation, we must be aware that the currentAngle might be above           <br></br>
    /// or below the setangle, and we must check for both cases. The currentAngleHasPassedZero  <br></br>
    /// and currentAngleStartedBelowSetAngle variables are used to keep track of this.          <br></br>
    /// </summary>
    private bool currentAngleHasPassedSetAngleDirectionRight()
    {
        Debug.Log("CurrentAngle: " + transform.eulerAngles.y + " SetAngle: " + setAngle);
        if (transform.eulerAngles.y <= setAngle && (!currentAngleStartedBelowSetAngle || currentAngleHasPassedZero))
        {
            return true;
        }
        if(currentAngleStartedBelowSetAngle && !currentAngleHasPassedZero)
        {
            currentAngleHasPassedZero = true;
        }
        return false;
    }


    /// <summary>
    /// This function is used to initially start board rotation, called by die roller
    /// </summary>
    /// <param name="diskNumber">The disk number that is being rolled, so we can know which die value to use</param>
    public void RotateDiskLeft(int diskNumber)
    {  
        // checks if the board is moving or the angle to move to is what we are already at, if it is, it doesn't do anything
        if (BoardScript.isTheBoardMoving()) return;
        int roll = DieRollerScript.getDieValue(diskNumber);
        float tempAngle = (currentAngle + (roll * tileWidth)) % 360;
        if (tempAngle == currentAngle) return;
        // start rotation
        rotationDirection = RotateMode.Left;
        BoardScript.startRotation();
        setAngle = tempAngle;
        currentAngleHasPassedZero = false;
        if(currentAngle < setAngle)
        {
            currentAngleStartedBelowSetAngle = true;
        }
        else
        {
            currentAngleStartedBelowSetAngle = false;
        }
        
        if (diskNumber < 3)
            upperDiskScript.RotateDiskLeftAngle(roll * tileWidth);
        Debug.Log("CurrentAngle: " + currentAngle + " SetAngle: " + setAngle + " roll: " + roll + " tileWidth: " + tileWidth);
    }


    /// <summary>
    /// This function is used to rotate the disks above the disk that initially started the rotation.<br></br>
    /// It is always called by another disk. 
    /// </summary>
    /// <param name="angle">The angle to rotate the disk by</param>
    public void RotateDiskLeftAngle(float angle)
    {
        if (BoardScript.isTheBoardMoving()) return;

        rotationDirection = RotateMode.Left;
        setAngle = (currentAngle + angle) % 360;
        currentAngleHasPassedZero = false;
        if(currentAngle < setAngle)
        {
            currentAngleStartedBelowSetAngle = true;
        }
        else
        {
            currentAngleStartedBelowSetAngle = false;
        }
        if (diskNumber < 3)
            upperDiskScript.RotateDiskLeftAngle(angle);
    }

    /// <summary>
    /// This function is used to initially start board rotation, called by die roller
    /// </summary>
    /// <param name="diskNumber">The disk number that is being rolled, so we can know which die value to use</param>
    public void RotateDiskRight(int diskNumber)
    {
        // checks if the board is moving or the angle to move to is what we are already at, if it is, it doesn't do anything
        if (BoardScript.isTheBoardMoving()) return;
        int roll = DieRollerScript.getDieValue(diskNumber);
        float tempAngle = (currentAngle + (360 - (roll * tileWidth))) % 360;
        if (tempAngle == currentAngle) return;
        // start rotation
        rotationDirection = RotateMode.Right;
        BoardScript.startRotation();
        setAngle = tempAngle;
        currentAngleHasPassedZero = false;
        if(currentAngle < setAngle && currentAngle != 0)
        {
            currentAngleStartedBelowSetAngle = true;
        }
        else
        {
            currentAngleStartedBelowSetAngle = false;
        }
        Debug.Log("CurrentAngle: " + currentAngle + " SetAngle: " + setAngle + " roll: " + roll + " tileWidth: " + tileWidth);
        
        if (diskNumber < 3)
            upperDiskScript.RotateDiskRightAngle(roll * tileWidth);
    }

    /// <summary>
    /// This function is used to rotate the disks above the disk that initially started the rotation.   <br></br>
    /// It is always called by another disk. 
    /// </summary>
    /// <param name="angle">The angle to rotate the disk by</param>
    public void RotateDiskRightAngle(float angle)
    {
        setAngle = (currentAngle + (360 - angle)) % 360;
        currentAngleHasPassedZero = false;
        if(currentAngle < setAngle && currentAngle != 0)
        {
            currentAngleStartedBelowSetAngle = true;
        }
        else
        {
            currentAngleStartedBelowSetAngle = false;
        }
        rotationDirection = RotateMode.Right;
        if (diskNumber < 3)
            upperDiskScript.RotateDiskRightAngle(angle);
    }

    /// <summary>
    /// This function is used to determine which disk this one is, set the diskName variable,   <br></br>
    /// and set the tileWidth variable. The tileWidth variable is used to determine how much    <br></br>
    /// a certain roll will rotate the board. 
    /// </summary>
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
