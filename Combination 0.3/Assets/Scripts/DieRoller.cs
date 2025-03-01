using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DieRoller : MonoBehaviour
{
    private bool rolling;

    public bool canMove;

    private Image[] diceFaceImageComponentReferences;
    
    public Sprite[] DiceFaces;

    //0 will be green, 1 will be red and 2 will be blue
    public int[] DiceValues;

    public static int DICE_COUNT = 3;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        DiceValues = new int[DICE_COUNT];
        diceFaceImageComponentReferences = new Image[DICE_COUNT];
        rolling = false;
        for(int i = 0; i < diceFaceImageComponentReferences.Length; i++)
        {
            diceFaceImageComponentReferences[(DICE_COUNT-1)-i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getDieValue(int diskNumber)
    {
        return DiceValues[diskNumber - 1];
    }

    public void RollTheDice() 
    {
        //We roll 3 6-sided dice to determine how the player can move the disks.

        //You could roll actual physics enabled dice here

        //For now going to use random number generation and show images

        rolling = true;

        for (int i = 0; i < DiceValues.Length; i++)
        {
            DiceValues[i] = UnityEngine.Random.Range(1, 7);

            //Update Visuals in here to set dice roll in real time
            //TODO: add dice rolling animation of switching between multiple faces

            Sprite newFace = null;

            switch (DiceValues[i])
            {
                case 1:
                    newFace = DiceFaces[0];
                    break;
                case 2:
                    newFace = DiceFaces[1];
                    break;
                case 3:
                    newFace = DiceFaces[2];
                    break;
                case 4:
                    newFace = DiceFaces[3];
                    break;
                case 5:
                    newFace = DiceFaces[4];
                    break;
                case 6:
                    newFace = DiceFaces[5];
                    break;
            }

            diceFaceImageComponentReferences[i].sprite = newFace;

        }

        Debug.Log("Rolled Green:" + DiceValues[0] + " Red: " + DiceValues[1] + " Blue: " + DiceValues[2]);
    }

    public void RollDiceSpriteAnimation()
    {

    }

    public async void allowMovementAgain()
    {
        await Task.Delay(250);
        enableArrowInteraction();
        canMove = true;
        
    }

    public bool canDiskMove()
    {
        return canMove;
    }

    public void startDiskMove()
    {
        canMove = false;
        disableArrowInteraction();
    }

    private void disableArrowInteraction()
    {
        // Get the Transform component of the diskMover
        Transform diskMoveArrows = GameObject.Find("DiskMoveArrows").transform;

        // Iterate through each child Transform of the parent GameObject
        foreach (Transform child in diskMoveArrows)
        {
            // Get the Button component of the child GameObject
            Button button = child.GetComponent<Button>();

            // If the Button component is not null, disable the button
            if (button != null)
            {
                button.interactable = false;
            }
        }
    }

    private void enableArrowInteraction()
    {
        // Get the Transform component of the diskMover
        Transform diskMover = GameObject.Find("DiskMoveArrows").transform;

        // Iterate through each child Transform of the parent GameObject
        foreach (Transform child in diskMover)
        {
            // Get the Button component of the child GameObject
            Button button = child.GetComponent<Button>();

            // If the Button component is not null, enable the button
            if (button != null)
            {
                button.interactable = true;
            }
        }
    }
}
