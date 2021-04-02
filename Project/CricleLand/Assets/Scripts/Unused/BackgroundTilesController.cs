using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTilesController : MonoBehaviour
{
    public GameObject cube;
    public List<GameObject> allBack;
    public struct cubeInfo
    {
        public float x;
        public float y;
        public GameObject cube;

        public cubeInfo(float x, float y, GameObject cube)
        {
            this.x = x;
            this.y = y;
            this.cube = cube;
        }
    }

    public cubeInfo[][] cubes;

    void Start()
    {
        allBack = new List<GameObject>();

        cubes = new cubeInfo[201][];
        for (int i = 0; i < 201; ++i)
        {
            cubes[i] = new cubeInfo[121];
        }

        for (float i = -50; i <= 50; i += 0.5f)
        {
            for (float j = -30; j <= 30; j += 0.5f)
            {
                allBack.Add(Instantiate(cube, new Vector3(i, j, 0), new Quaternion()));
                cubes[(int)(i * 2 + 100)][(int)(j * 2 + 60)] = new cubeInfo(i, j, allBack[allBack.Count - 1]);
            }
        }
        for (int i = 0; i <= 24320; ++i)
        {
            if (i < 24200)
            {
                allBack[i].GetComponent<Back>().right = allBack[i + 41];
            }
            if (i > 120)
            {
                allBack[i].GetComponent<Back>().left = allBack[i - 41];
            }
            if (i % 121 != 0 && i > 0)
            {
                allBack[i].GetComponent<Back>().down = allBack[i - 1];
            }
            if (i % 121 - 120 != 0 && i < 24320)
            {
                allBack[i].GetComponent<Back>().up = allBack[i + 1];
            }
            allBack[i].GetComponent<Back>().number = i;

            allBack[i].transform.parent = gameObject.transform;
            allBack[i].gameObject.name = "Tile" + allBack[i].GetComponent<Back>().number;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

}
