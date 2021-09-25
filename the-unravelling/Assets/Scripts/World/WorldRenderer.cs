using UnityEngine;
using UnityEngine.Tilemaps;

public class WorldRenderer : MonoBehaviour {
    public Tilemap gameWorld;
    
    // Start is called before the first frame update
    void Start()
    {
        TileBase tile = WorldData.Get.GRASS.baseSprites[0];
        int halfMapSize = (int)WorldData.Get.mapSize / 2; // Know that the mapsize is in the power of 2
        for (int y = 0; y < WorldData.Get.mapSize; y++) {
            for (int x = 0; x < WorldData.Get.mapSize; x++) {
                // Find a much better way of doing this, this is not scalable
                switch (WorldData.Get.map[x,y]) {
                    case 1: tile = WorldData.Get.GRASS.GetRandomBaseSprite(); break;
                    case 2: tile = WorldData.Get.DIRT.GetRandomBaseSprite(); break;
                    case 3: tile = WorldData.Get.STONE.GetRandomBaseSprite(); break;
                }
                gameWorld.SetTile(new Vector3Int(x - halfMapSize, y - halfMapSize, 0), tile);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
