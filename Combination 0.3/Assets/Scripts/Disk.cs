using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    // Which disk this is: 0 is startline, 1 is green, 2 is red, 3 is blue, and 4 is finishline
    public int diskNumber;
    public string diskName;

    private DieRoller DieRoller;
    private Disk upperDisk;

    private float setAngle;
    private float tileWidth;
    private float startedAngle;
    private bool currentAngleStartedBelowSetAngle;
    private bool currentAngleHasPassedZero;
    private RotateMode rotationDirection;

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


    // Start is called before the first frame update
    void Start()
    {
        // TODO
        //  have the disk recognize which disk number it is somehow 
        //  automatically so I don't have to set that shit up every time
        DieRoller = GameObject.Find("DieRoller").GetComponent<DieRoller>();
        currentAngleHasPassedZero = false;
        currentAngleStartedBelowSetAngle = false;

        tileWidth = 7.5f;
        for (int i = diskNumber; i > 0; i--)
        {
            tileWidth = tileWidth * 2;
        }

        if (diskNumber < 4)
            upperDisk = GameObject.Find($"Disk{diskNumber + 1}").GetComponent<Disk>();
    }

    // Update is called once per frame
    void Update()
    {
        if (diskNumber == 0 || diskNumber == 4)
            rotationDirection = RotateMode.None;
        if (rotationDirection == RotateMode.None)
            return;

        if (rotationDirection == RotateMode.Left)
        {
            if (currentAngleHasPassedSetAngleDirectionLeft())
            {
                rotationDirection = RotateMode.None;
                transform.eulerAngles = new Vector3(0, setAngle, 0);
                currentAngleHasPassedZero = false;
                currentAngleStartedBelowSetAngle = false;
                DieRoller.allowMovementAgain();
                return;
            }
            transform.Rotate(new Vector3(0, 250, 0) * Time.deltaTime);
            return;
        }
        if (rotationDirection == RotateMode.Right)
        {
            if (currentAngleHasPassedSetAngleDirectionRight())
            {
                rotationDirection = RotateMode.None;
                transform.eulerAngles = new Vector3(0, setAngle, 0);
                currentAngleHasPassedZero = false;
                currentAngleStartedBelowSetAngle = false;
                DieRoller.allowMovementAgain();
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
        if (transform.eulerAngles.y < startedAngle && !currentAngleHasPassedZero)
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
        if (!DieRoller.canDiskMove()) return;
        int roll = DieRoller.getDieValue(diskNumber);
        float tempAngle = (transform.eulerAngles.y + (roll * tileWidth)) % 360;
        if (tempAngle == transform.eulerAngles.y) return;
        //start rotation
        DieRoller.startDiskMove();
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
            upperDisk.RotateDiskLeftAngle(roll * tileWidth);
    }

    /// <summary>
    /// This function is used to initially start board rotation, called by die roller
    /// </summary>
    /// <param name="diskNumber">The disk number that is being rolled, so we can know which die value to use</param>
    public void RotateDiskRight()
    {
        Debug.Log("RotateDiskRight");
        // checks if the board is moving or the angle to move to is what we are already at, if it is, it doesn't do anything
        if (!DieRoller.canDiskMove()) return;
        Debug.Log("got past canDiskMove");
        
        int roll = DieRoller.getDieValue(diskNumber);
        float targetAngle = (transform.eulerAngles.y + (360 - (roll * tileWidth))) % 360;
        if (targetAngle == transform.eulerAngles.y) return;
        //start rotation
        Debug.Log("starting disk move");
        DieRoller.startDiskMove();
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
            upperDisk.RotateDiskRightAngle(roll * tileWidth);
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
            upperDisk.RotateDiskLeftAngle(angle);
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
            upperDisk.RotateDiskRightAngle(angle);
    }
}
