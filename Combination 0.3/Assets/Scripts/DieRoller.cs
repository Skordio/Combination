using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieRoller : MonoBehaviour
{
    private bool rolling;

    public bool canRoll;

    private Image[] diceFaceImageComponentReferences;
    
    public Sprite[] DiceFaces;

    //0 will be green, 1 will be red and 2 will be blue
    public int[] DiceValues;

    public static int DICE_COUNT = 3;

    // Start is called before the first frame update
    void Start()
    {
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
            DiceValues[i] = UnityEngine.Random.Range( 1, 7 );

            //Update Visuals in here to set dice roll in real time
            //TODO: add dice rolling animation of switching between multiple faces

            if (DiceValues[i] == 1)
            {
                diceFaceImageComponentReferences[i].sprite = DiceFaces[0];
            }
            else if (DiceValues[i] == 2)
            {
                diceFaceImageComponentReferences[i].sprite = DiceFaces[1];
            }
            else if (DiceValues[i] == 3)
            {
                    diceFaceImageComponentReferences[i].sprite = DiceFaces[2];
            }
            else if (DiceValues[i] == 4)
            {
                diceFaceImageComponentReferences[i].sprite = DiceFaces[3];
            }
            else if (DiceValues[i] == 5)
            {
                diceFaceImageComponentReferences[i].sprite = DiceFaces[4];
            }
            else if (DiceValues[i] == 6)
            {
                diceFaceImageComponentReferences[i].sprite = DiceFaces[5];
            }
        }

        Debug.Log("Rolled Green:" + DiceValues[0] + " Red: " + DiceValues[1] + " Blue: " + DiceValues[2]);
    }

    public void RollDiceSpriteAnimation()
    {

    }

    public void allowRolling()
    {
        System.Threading.Thread.Sleep(1000);
        canRoll = true;
    }

    public bool canWeRoll()
    {
        return canRoll;
    }

    public void noRoll()
    {
        canRoll = false;
    }
}
