using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    //Here we are gonna have a list of a bunch of the board's components like disks and stuff and then we can use those references instead of GameObject.Find

    public GameObject[] AllTiles;

    public List<Tile> allTilesScripts;

    public List<Tile> greenTiles;

    public List<Tile> redTiles;

    public List<Tile> blueTiles;

    private int TilesNum;

    private bool isMoving;


    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        TilesNum = 24*2 + 12 + 6;
        buildTileLists();
        StartCoroutine(startUpdateTiles());
    }

    IEnumerator startUpdateTiles()
    {
        yield return new WaitForSeconds((float)0.25);
        updateTiles();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateTiles()
    {
        for(int i = 0; i < allTilesScripts.Count; i++)
        {
            allTilesScripts[i].findParentTile();
        }
    }

    public void startRotation()
    {
        isMoving = true;
    }

    public void stopRotation()
    {
        isMoving = false;
        updateTiles();
    }

    public bool isTheBoardMoving()
    {
        return isMoving;
    }

    private void buildTileLists()
    {
        for (int i = 0; i < AllTiles.Length && AllTiles[i] != null; i++)
        {
            Tile newTile = AllTiles[i].GetComponent<Tile>();
            newTile.tileTest();
            allTilesScripts.Add(newTile);
            if (newTile.getDiskNumber() == 1)
                greenTiles.Add(newTile);
            else if (newTile.getDiskNumber() == 2)
                redTiles.Add(newTile);
            else if (newTile.getDiskNumber() == 3)
                blueTiles.Add(newTile);
        }
    }

    public List<Tile> getGreenTiles()
    {
        return greenTiles;
    }

    public List<Tile> getRedTiles()
    {
        return redTiles;
    }

    public List<Tile> getBlueTiles()
    {
        return blueTiles;
    }
}
