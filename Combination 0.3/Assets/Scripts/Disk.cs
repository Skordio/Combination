using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    // Which disk this is: 0 is startline, 1 is green, 2 is red, 3 is blue, and 4 is finishline
    public int diskNumber;

    public string diskName;

    public GameObject TheBoard;

    private Board BoardScript;

    public GameObject DieRoll;

    public DieRoller DieRollerScript;

    public float currentAngle;

    public float setAngle;

    public float tileWidth;

    public float startedAngle;

    public GameObject higherDisk;

    public Disk upperDiskScript;

    public int debugCounter;

    public bool currentAngleStartedBelowSetAngle;

    public bool currentAngleHasPassedZero;

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
        currentAngleHasPassedZero = false;
        currentAngleStartedBelowSetAngle = false;

        /*rotationDirection = RotateMode.Left;*/
        determineDisk();
        if(diskNumber < 4)
            upperDiskScript = higherDisk.GetComponent<Disk>();
    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = transform.eulerAngles.y;
        if (diskNumber == 0 || diskNumber == 4)
        {
            rotationDirection = RotateMode.None;
            return;
        }
        if(rotationDirection == RotateMode.Left)
        {
            if(currentAngleHasPassedSetAngleDirectionLeft()){
                rotationDirection = RotateMode.None;
                transform.eulerAngles = new Vector3(0, setAngle, 0);
                currentAngleHasPassedZero = false;
                currentAngleStartedBelowSetAngle = false;
                DieRollerScript.allowMovementAgain();
                return;
            }
            transform.Rotate(new Vector3(0, 250, 0) * Time.deltaTime);
            return;
        }
        if(rotationDirection == RotateMode.Right)
        {
            if(currentAngleHasPassedSetAngleDirectionRight()){
                rotationDirection = RotateMode.None;
                transform.eulerAngles = new Vector3(0, setAngle, 0);
                currentAngleHasPassedZero = false;
                currentAngleStartedBelowSetAngle = false;
                DieRollerScript.allowMovementAgain();
                return;
            }
            transform.Rotate(new Vector3(0, -250, 0) * Time.deltaTime);
            return;
        }
    }

    /// <summary>
    /// This function is used to check if the current angle has passed by the set angle         <br></br>
    /// in the left direction, and manages the currentAngleHasPassedZero variable.              <br></br>
    /// When checking rotation, we must be aware that the currentAngle might be above           <br></br>
    /// or below the setangle, and we must check for both cases. The currentAngleHasPassedZero  <br></br>
    /// and currentAngleStartedBelowSetAngle variables are used to keep track of this.          <br></br>
    /// </summary>
    private bool currentAngleHasPassedSetAngleDirectionLeft()
    {
        if (transform.eulerAngles.y >= (setAngle % 360) && (currentAngleStartedBelowSetAngle || currentAngleHasPassedZero))
        {
            return true;
        }
        if(transform.eulerAngles.y < startedAngle && !currentAngleHasPassedZero)
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
        if ((transform.eulerAngles.y <= setAngle && (!currentAngleStartedBelowSetAngle || currentAngleHasPassedZero)) || ((int)setAngle == 0 && transform.eulerAngles.y > startedAngle))
        {
            return true;
        }
        if(transform.eulerAngles.y > startedAngle && !currentAngleHasPassedZero)
        {
            currentAngleHasPassedZero = true;
        }
        return false;
    }


    /// <summary>
    /// This function is used to initially start board rotation, called by die roller
    /// </summary>
    public void RotateDiskLeft()
    {  
        // checks if the board is moving or the angle to move to is what we are already at, if it is, it doesn't do anything
        if (!DieRollerScript.canDiskMove()) return;
        int roll = DieRollerScript.getDieValue(diskNumber);
        float tempAngle = (transform.eulerAngles.y + (roll * tileWidth)) % 360;
        if (tempAngle == transform.eulerAngles.y) return;
        //start rotation
        DieRollerScript.startDiskMove();
        setAngle = tempAngle;
        currentAngleHasPassedZero = false;
        startedAngle = transform.eulerAngles.y;
        if(transform.eulerAngles.y < setAngle)
        {
            currentAngleStartedBelowSetAngle = true;
        }
        else
        {
            currentAngleStartedBelowSetAngle = false;
        }
        rotationDirection = RotateMode.Left;
        if (diskNumber < 3)
            upperDiskScript.RotateDiskLeftAngle(roll * tileWidth);
    }


    /// <summary>
    /// This function is used to rotate the disks above the disk that initially started the rotation.<br></br>
    /// It is always called by another disk. 
    /// </summary>
    public void RotateDiskLeftAngle(float angle)
    {
        setAngle = (transform.eulerAngles.y + angle) % 360;
        currentAngleHasPassedZero = false;
        startedAngle = transform.eulerAngles.y;
        if (transform.eulerAngles.y < setAngle)
        {
            currentAngleStartedBelowSetAngle = true;
        }
        else
        {
            currentAngleStartedBelowSetAngle = false;
        }
        rotationDirection = RotateMode.Left;
        if (diskNumber < 3)
            upperDiskScript.RotateDiskLeftAngle(angle);
    }

    /// <summary>
    /// This function is used to initially start board rotation, called by die roller
    /// </summary>
    /// <param name="diskNumber">The disk number that is being rolled, so we can know which die value to use</param>
    public void RotateDiskRight()
    {
        // checks if the board is moving or the angle to move to is what we are already at, if it is, it doesn't do anything
        if (!DieRollerScript.canDiskMove()) return;
        
        int roll = DieRollerScript.getDieValue(diskNumber);
        float targetAngle = (transform.eulerAngles.y + (360 - (roll * tileWidth))) % 360;
        if (targetAngle == transform.eulerAngles.y) return;
        //start rotation
        DieRollerScript.startDiskMove();
        setAngle = targetAngle;
        currentAngleHasPassedZero = false;
        startedAngle = transform.eulerAngles.y;
        if (transform.eulerAngles.y < setAngle)
        {
            currentAngleStartedBelowSetAngle = true;
        }
        else
        {
            currentAngleStartedBelowSetAngle = false;
        }
        rotationDirection = RotateMode.Right;
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
        setAngle = (transform.eulerAngles.y + (360 - angle)) % 360;
        currentAngleHasPassedZero = false;
        startedAngle = transform.eulerAngles.y;
        if (transform.eulerAngles.y < setAngle)
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
