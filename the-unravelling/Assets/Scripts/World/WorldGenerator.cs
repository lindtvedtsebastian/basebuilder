using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {
    private const int LOWLAND = 0;
    private const int HIGHLAND = 10;
    private const int DRY = 0;
    private const int MOIST = 5;

    public static IWorld generateWorld(int size = 256, int seed = 123) {
        IWorld world = new IWorld();
        Vector2 offset = new Vector2(0, 0);
        world.size = size;
        float[][] heightMap = Noise.generateNoiseMap(seed:seed++, offset: offset);
        float[][] moistureMap = Noise.generateNoiseMap(seed: seed++, offset: offset);


        for (int y = 0; y < size; y++) {
            for (int x = 0; x < size; x++) {
                world.terrain[y][x] = determineBiome(heightMap[y][x], moistureMap[y][x]);
            }
        }

		return world;
    }


    private static int determineBiome(float height, float moisture) {
        int biome = height < 0.4f ? LOWLAND : HIGHLAND;
        biome += moisture < 0.5f ? DRY : MOIST;

		if (biome < 10) return GameIDs.STONE;
		else if (biome == 10) return GameIDs.DIRT;
		else return GameIDs.GRASS;
    }
}

[Serializable]
public class IWorld {
    public WorldState state;
    public int size;
    public int[][] terrain;
    public int[][] background;
    public int[][] pathfindingMap;
    public List<IEntity> iEntities;
    public string mapName;

    public IWorld() {
        state = new WorldState();
        iEntities = new List<IEntity>();
    }
}

