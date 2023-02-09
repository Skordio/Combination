using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieRoller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DiceValues = new int[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //0 will be green, 1 will be red and 2 will be blue
    public int[] DiceValues;

    public int getDieValue(int diskNumber)
    {
        return DiceValues[diskNumber - 1];
    }

    public Sprite[] DiceFaces;

    public void RollTheDice() 
    {
        //We roll 3 6-sided dice to determine how the player can move the disks.

        //You could roll actual physics enabled dice here

        //For now going to use random number generation and show images

        for (int i = 0; i < DiceValues.Length; i++)
        {
            DiceValues[i] = Random.Range( 1, 7 );

            //Update Visuals in here to set dice roll in real time
            //TODO: add dice rolling animation of switching between multiple faces

            Debug.Log(this.transform);

            if (DiceValues[i] == 1)
            {
                this.transform.GetChild(DiceValues.Length-1 - i).GetComponent<Image>().sprite =
                    DiceFaces[0];
            }
            else if (DiceValues[i] == 2)
            {
                this.transform.GetChild(DiceValues.Length-1 - i).GetComponent<Image>().sprite =
                    DiceFaces[1];
            }
            else if (DiceValues[i] == 3)
            {
                this.transform.GetChild(DiceValues.Length-1 - i).GetComponent<Image>().sprite =
                    DiceFaces[2];
            }
            else if (DiceValues[i] == 4)
            {
                this.transform.GetChild(DiceValues.Length-1 - i).GetComponent<Image>().sprite =
                    DiceFaces[3];
            }
            else if (DiceValues[i] == 5)
            {
                this.transform.GetChild(DiceValues.Length-1 - i).GetComponent<Image>().sprite =
                    DiceFaces[4];
            }
            else if (DiceValues[i] == 6)
            {
                this.transform.GetChild(DiceValues.Length-1 - i).GetComponent<Image>().sprite =
                    DiceFaces[5];
            }
        }

        Debug.Log("Rolled Green:" + DiceValues[0] + " Red: " + DiceValues[1] + " Blue: " + DiceValues[2]);
    }

    public void RollDiceSpriteAnimation()
    {

    }
}
