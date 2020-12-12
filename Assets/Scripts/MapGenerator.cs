using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class MapGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Coord))
            {
                return false;
            }
            return this == (Coord)obj;
        }

        public override int GetHashCode()
        {
            return new { x, y }.GetHashCode();
        }
    }

    [System.Serializable]
    public struct Map
    {
        public Coord mapSize;
        [Range(0, 1)] public float obstaclePercent;
        public int obstacleSeed;

        public float minObstacleHeight;
        public float maxObstacleHeight;
        public Color[] colorGradients;

        public Coord MapCenter {
            get
            {
                return new Coord(mapSize.x / 2, mapSize.y / 2);
            }
        }
    }

    public Transform tilePrefab;
    public Transform obstaclePrefab;
    public Map[] generatedMaps;
    
    [Range(0, 1)] public float outlinePercent;
    public int tileSize = 1;
    public bool obstacleGaps = false;

    private List<Coord> allTilesCoords;
    private Queue<Coord> shuffledTilesCoords;
    //public NavMeshSurface navSurface;

    private Queue<Coord> shuffledOpenTilesCoords;
    [HideInInspector] public Map currentMap;
    public int mapIndex;

    private Transform[,] tileTransformMap;
    public int heightSeed;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        currentMap = generatedMaps[mapIndex];

        if (tileSize > 0)
        {
            allTilesCoords = new List<Coord>();
            tileTransformMap = new Transform[currentMap.mapSize.x, currentMap.mapSize.y];

            string holderName = "Generated Map";
            Transform previousHolder = transform.Find(holderName);
            if (previousHolder != null)
            {
                DestroyImmediate(previousHolder.gameObject);
            }

            Transform mapHolder = new GameObject(holderName).transform;
            mapHolder.SetParent(transform);

            for (int x = 0; x < currentMap.mapSize.x; x++)
            {
                for (int y = 0; y < currentMap.mapSize.y; y++)
                {
                    allTilesCoords.Add(new Coord(x, y));
                    Vector3 tilePos = CoordToTilePosition(x, y);
                    Transform tileTransform = Instantiate(tilePrefab, tilePos, Quaternion.identity, mapHolder);
                    tileTransform.localScale = Vector3.up * tileTransform.localScale.y + (Vector3.forward + Vector3.right) * tileSize * (1 - outlinePercent);
                    tileTransformMap[x, y] = tileTransform;
                }
            }

            shuffledTilesCoords = new Queue<Coord>(Utility.SuffleArray(allTilesCoords.ToArray(), currentMap.obstacleSeed));
            if (obstaclePrefab != null)
                GenerateObstacles(mapHolder);
            //navSurface.BuildNavMesh();
        }
    }

    private void GenerateObstacles(Transform parent)
    {
        bool[,] obstacleMap = new bool[currentMap.mapSize.x, currentMap.mapSize.y];

        int obstacleCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y * currentMap.obstaclePercent);
        int currentObstacleCount = 0;

        List<Coord> allOpenTilesCoords = new List<Coord>(allTilesCoords);

        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;

            if (randomCoord != currentMap.MapCenter && IsMapConnected(obstacleMap, currentObstacleCount))
            {
                Vector3 obstaclePosition = CoordToTilePosition(randomCoord);
                float obstacleHeight = Mathf.Lerp(currentMap.minObstacleHeight, currentMap.maxObstacleHeight, Utility.GetRandomPercent(heightSeed));
                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity, parent);
                newObstacle.localScale = new Vector3(newObstacle.localScale.x, obstacleHeight, newObstacle.localScale.z);
                newObstacle.localScale *= obstacleGaps ? (1 - outlinePercent) : 1;
                newObstacle.position += new Vector3(0, newObstacle.localScale.y / 2, 0);

                allOpenTilesCoords.Remove(randomCoord);
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }

        shuffledOpenTilesCoords = new Queue<Coord>(Utility.SuffleArray(allOpenTilesCoords.ToArray(), Mathf.RoundToInt(Time.time / 100)));
    }

    private bool IsMapConnected(bool[,] obstacleMap, int currentObstacleCount)
    {
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(currentMap.MapCenter);
        mapFlags[currentMap.MapCenter.x, currentMap.MapCenter.y] = true;

        int accessibleCount = 1;

        while (queue.Count > 0)
        {
            Coord tileCoord = queue.Dequeue();
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighborX = tileCoord.x + x;
                    int neighborY = tileCoord.y + y;
                    if (x == 0 ^ y == 0)
                    {
                        if (neighborX >= 0 && neighborX < obstacleMap.GetLength(0) && neighborY >= 0 && neighborY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighborX, neighborY] && !obstacleMap[neighborX, neighborY])
                            {
                                mapFlags[neighborX, neighborY] = true;
                                queue.Enqueue(new Coord(neighborX, neighborY));
                                accessibleCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleCount = currentMap.mapSize.x * currentMap.mapSize.y - currentObstacleCount;
        return targetAccessibleCount == accessibleCount;
    }

    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffledTilesCoords.Dequeue();
        shuffledTilesCoords.Enqueue(randomCoord);

        return randomCoord;
    }

    public Transform GetRandomOpenTile()
    {
        Coord randomCoord = shuffledOpenTilesCoords.Dequeue();
        shuffledTilesCoords.Enqueue(randomCoord);

        return tileTransformMap[randomCoord.x, randomCoord.y];
    }

    public Transform GetTileFromPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / tileSize + (currentMap.mapSize.x - 1) / 2.0f);
        int y = Mathf.RoundToInt(position.z / tileSize + (currentMap.mapSize.y - 1) / 2.0f);
        x = Mathf.Clamp(x, 0, tileTransformMap.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, tileTransformMap.GetLength(1) - 1);

        return tileTransformMap[x, y];
    }

    public Vector3 CoordToTilePosition(int x, int y)
    {
        return new Vector3(-currentMap.mapSize.x / 2 + x + 0.5f, 0, -currentMap.mapSize.y / 2 + y + 0.5f) * tileSize;
    }

    public Vector3 CoordToTilePosition(Coord coord)
    {
        return new Vector3(-currentMap.mapSize.x / 2 + coord.x + 0.5f, 0, -currentMap.mapSize.y / 2 + coord.y + 0.5f) * tileSize;
    }
}
