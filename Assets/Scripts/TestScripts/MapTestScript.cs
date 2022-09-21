using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTestScript : MonoBehaviour
{
    public GameObject _plane;
    // Start is called before the first frame update
    Texture2D _mapTest;
    string _mapName;
    void Start()
    {
        //_mapName = "Prefabs/Map_Prefab/CreateTestPrefab/Nature/Tile/tile-plain_grass";
        _mapName = "tile-plain_grass";
        _mapTest = Resources.Load<Texture2D>(_mapName);
        _plane.GetComponent<MeshRenderer>().material.mainTexture = _mapTest;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
