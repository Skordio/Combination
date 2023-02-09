using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public List<Material> materials;

    //The angle of the center of the tile
    private float DefaultCenterAngle;

    //The current angle of the center of the tile
    private float CenterAngle;

    //The disk number that this tile is on;; 0 is start level, 1 is green, 2 is red and 3 is blue, and 4 is top
    private int diskNumber;

    //The width of the tile, used to determine the width of the whole tile
    private int tileWidth;

    //The name of the disk that this tile is on mainly for debug purposes
    private string diskName;

    public GameObject TheBoard;

    public GameObject tileView;

    public Board BoardScript;

    public Tile ParentTile;

    public bool bombed;


    // Start is called before the first frame update
    void Start()
    {
        TheBoard = GameObject.Find("TheBoard");
        BoardScript = TheBoard.GetComponent<Board>();
        if(bombed)
        {
            tileView.GetComponent<MeshRenderer>().material = materials[1];
        }
        setDisks();
    }

    // Update is called once per frame
    void Update()
    {
        this.CenterAngle = this.getCenterAngle();
    }

    public float getCenterAngle()
    {
        //Find the board so that you can find the angle of this tile
        GameObject TheBoard = GameObject.Find("TheBoard");

        Vector3 direction = this.transform.position - TheBoard.transform.position;
        Debug.DrawRay(transform.position, direction, Color.green);

        float centerAngle = Mathf.Atan2(direction.z, direction.x) * Mathf.Rad2Deg;

        return centerAngle;
    }

    public float getRightAngle()
    {
        return this.CenterAngle - (float) (diskNumber * 7.5);
    }

    public float getLeftAngle()
    {
        return this.CenterAngle + (float) (diskNumber * 7.5);
    }

    private void setDisks()
    {
        float height = transform.position.y;
        if (height > 0 && height < .1)
        {
            diskNumber = 0;
            tileWidth = 1;
            diskName = "StartLine";
        }
        if (height > .1 && height < .2)
        {
            diskNumber = 1;
            tileWidth = 1;
            diskName = "Green";
        }
        else if (height > .2 && height < .3)
        {
            diskNumber = 2;
            tileWidth = 2;
            diskName = "Red";
        }
        else if (height > .3 && height < .4)
        {
            diskNumber = 3;
            tileWidth = 4;
            diskName = "Blue";
        }
        else if (height > .4)
        {
            diskNumber = 4;
            diskName = "FinishLine";
        }
    } 

    public int getDiskNumber()
    {
        //0 is start level, 1 is green, 2 is red and 3 is blue, and 4 is top
        return diskNumber;
    }

    public void findParentTile()
    {
        ParentTile = null;
        List<Tile> parentDiskTiles = new List<Tile> {};
        if (diskNumber == 0)
        {
            parentDiskTiles = BoardScript.getGreenTiles();
        }
        else if(diskNumber == 1)
        {

            parentDiskTiles = BoardScript.getRedTiles();
        }
        else if(diskNumber == 2)
        {

            parentDiskTiles = BoardScript.getBlueTiles();
        }
        for (int i = 0; i < parentDiskTiles.Count; i++)
        {
            if (this.getCenterAngle() > parentDiskTiles[i].getRightAngle() && this.getCenterAngle() < parentDiskTiles[i].getLeftAngle())
            {
                ParentTile = parentDiskTiles[i];
                if(bombed && ParentTile.bombed)
                {
                    Debug.Log("BOMB");
                }
            }
        }
    }

    //Todo: remove these two methods later after testing
    IEnumerator moveUpTest()
    {
        transform.position = transform.position + new Vector3(0, 15, 0);
        yield return new WaitForSeconds(2);
        transform.position = transform.position + new Vector3(0, -15, 0);
    }

    public void testPiece()
    {
        StartCoroutine(moveUpTest());
    }

    public void testParent()
    {
        Debug.Log("Tile tested");
        StartCoroutine(ParentTile.moveUpTest());
    }

    public void tileTest()
    {
        Debug.Log("Tile tested");
    }
}
