using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "Bitmaskable World Entity" ,menuName = "Scriptable Objects/World/Bitmaskable World Entity")]
public class BitmaskableWorldEntity : WorldEntity
{
    private bool checkNorth;
    private bool checkWest;
    private bool checkEast;
    private bool checkSouth;
    private bool northExists;
    private bool westExists;
    private bool eastExists;
    private bool southExists;


    public TileBase SetSprite(int y, int x) {
        return this.sprites[bitmaskToSpriteIndex[calculateBitmask(y,x)]];
    }

    public int calculateBitmask(int y, int x) {
        resetBooleans();
        identifyDirections(y, x);
        int bitmask = calculateCardinals(y, x);
        bitmask += calculateCorners(y, x);
        return bitmask;
    }

    private void identifyDirections(int y, int x) {
        checkNorth = y - 1 >= 0;
        checkWest = x - 1 >= 0;
        checkEast = x + 1 < GameData.Get.world.worldSize;
        checkSouth = y + 1 < GameData.Get.world.worldSize;
    }


    public int calculateCardinals(int y, int x) {
        int bitmask = 0;
        if (checkNorth && WorldCheck(y-1,x,this.id)) {
            bitmask += GameData.N;
            northExists = true;
        }
        if (checkWest && WorldCheck(y,x-1,this.id)) {
            bitmask += GameData.W;
            westExists = true;
        }
        if (checkEast && WorldCheck(y,x+1,this.id)) {
            bitmask += GameData.E;
            eastExists = true;
        }
        if (checkSouth && WorldCheck(y+1,x,this.id)) {
            bitmask += GameData.S;
            southExists = true;
        }
        return bitmask;
    }

    public int calculateCorners(int y, int x) {
        int bitmask = 0;
        
        if ((checkNorth && checkWest && WorldCheck(y-1, x-1,this.id))
            && northExists && westExists) {
            bitmask += GameData.NW;
        }
        if ((checkNorth && checkEast && WorldCheck(y-1, x+1,this.id))
            && northExists && eastExists) {
            bitmask += GameData.NE;
        }
        if ((checkSouth && checkWest && WorldCheck(y+1, x-1,this.id))
            && southExists && westExists) {
            bitmask += GameData.SW;
        }
        if ((checkSouth && checkEast && WorldCheck(y+1, x+1,this.id))
            && southExists && eastExists) {
            bitmask += GameData.SE;
        }
        return bitmask;
    }

    public bool IsWorldPosTile(int y, int x, int id) {
        return GameData.Get.world.map[y, x] == id;
    }

    public virtual bool WorldCheck(int y, int x, int id) {
        return IsWorldPosTile(y, x, id);
    }

    private void resetBooleans() {
        checkNorth = false;
        checkWest = false;
        checkEast = false;
        checkSouth = false;
        northExists = false;
        westExists = false;
        eastExists = false;
        southExists = false;
    }


    private Dictionary<int, int> bitmaskToSpriteIndex = new Dictionary<int,int>()
        {
            {0,0},
            {2,1},
            {8,2},
            {10,3},
            {11,4},
            {16,5},
            {18,6},
            {22,7},
            {24,8},
            {26,9},
            {27,10},
            {30,11},
            {31,12},
            {64,13},
            {66,14},
            {72,15},
            {74,16},
            {75,17},
            {80,18},
            {82,19},
            {86,20},
            {88,21},
            {90,22},
            {91,23},
            {94,24},
            {95,25},
            {104,26},
            {106,27},
            {107,28},
            {120,29},
            {122,30},
            {123,31},
            {126,32},
            {127,33},
            {208,34},
            {210,35},
            {214,36},
            {216,37},
            {218,38},
            {219,39},
            {222,40},
            {223,41},
            {248,42},
            {250,43},
            {251,44},
            {254,45},
            {255,46}
        };

}
