using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class MapGeneration : NetworkBehaviour
{
    // Start is called before the first frame update
    public Material[] materials;
    public GameObject prefab;
    static public Vector3 mapSize = new Vector3(50,3,50);

    void Start()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int z = 0; z < mapSize.z; z++)
            {
                float noice = Mathf.PerlinNoise(x / mapSize.x, z / mapSize.z);
                int y = Mathf.RoundToInt(noice * mapSize.y);

                GameObject obj = Instantiate(prefab);
                obj.tag = "start cube";
                obj.transform.position = new Vector3(x, y - 20, z);
                obj.GetComponent<Renderer>().material = y < 1 ? materials[0] :
                                                        y < 2 ? materials[1] :
                                                        y < 3 ? materials[2] :
                                                                 materials[3];
                NetworkServer.Spawn(obj);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
