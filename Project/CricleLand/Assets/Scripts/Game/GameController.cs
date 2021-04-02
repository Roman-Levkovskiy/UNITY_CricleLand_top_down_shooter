using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameController : MonoBehaviour
{
    public List<GameObject> allMobs;
    public GameObject playerPrefab;
    public GameObject finalScreen;
    public bool endSpawn;
    public Text mobCountText;
    public int allMobsCount = 0;
    public GameObject swarm;
    public int nextLevel;
    GameObject perkController;

    bool isChoosingPerk;

    //CAMERA
    private Vector3 velocity = Vector3.zero;
    public float dampTime = 0.15f;
    public Camera camera;

    public GameObject player;
    public GameObject zombie;
    public GameObject mechSphere;
    public GameObject bossEye;
    public Text hpCounter;
    public Text ammoCounter;
    public static List<Vector2> firstLevelCoordinates;
    void Start()
    {
        firstLevelCoordinates = new List<Vector2>();

        GameObject.FindGameObjectWithTag("PickupController").GetComponent<PickupController>().player =
        GameObject.FindGameObjectWithTag("CraftController").GetComponent<CraftController>().player =
        player = Instantiate(playerPrefab, new Vector3(0, 0, -1), new Quaternion());

        perkController = GameObject.FindGameObjectWithTag("PerkController").GetComponent<PerkController>().gameObject;

        finalScreen.SetActive(false);

        
        allMobs = new List<GameObject>();
        nextLevel = 1;
        StartCoroutine(waitForSpawnStart(6f));
        StartCoroutine(startLevels());
    }
    //this method used to change levels and start perk selection
    IEnumerator startLevels()
    {
        if (nextLevel > 1)
        {
            isChoosingPerk = true;
            player.GetComponent<PlayerController>().levelUp();
            StartCoroutine(perkController.GetComponent<PerkController>().showPerkPanel());
            yield return new WaitWhile(() => perkController.GetComponent<PerkController>().isRunning);
            isChoosingPerk = false;
        }
        Text waveText = GameObject.FindGameObjectWithTag("Canvas").transform.Find("WaveText").GetComponent<Text>();
        waveText.text = "WAVE " +(nextLevel == 0 ? (nextLevel + 1) : nextLevel);
        StartCoroutine(GameObject.FindGameObjectWithTag("Canvas").transform.Find("WaveText").GetComponent<FadeText>().FadeTextToZeroAlpha(1, waveText));
        yield return new WaitForSeconds(0.5f);
     
        switch(nextLevel)
        {
            case 1:
                StartCoroutine(firstLevel());
                break;

            case 2:
                StartCoroutine(secondLevel());
                break;
            case 3:
                StartCoroutine(thirdLevel());
                break;
            case 4:
                StartCoroutine(fourthLevel());
                break;
        }
        ++nextLevel;
    }
    //level methods add coordinates to coordinates list and then invoke create voids
    IEnumerator firstLevel()
    {
        firstLevelCoordinates.Add(new Vector2(-20, -10));

        firstLevelCoordinates.Add(new Vector2(-20, -10));
        firstLevelCoordinates.Add(new Vector2(-18, -10));
        firstLevelCoordinates.Add(new Vector2(-22, -10));
        firstLevelCoordinates.Add(new Vector2(-20, -15));
        firstLevelCoordinates.Add(new Vector2(-18, -15));
        firstLevelCoordinates.Add(new Vector2(-22, -15));

        firstLevelCoordinates.Add(new Vector2(-20, 10));
        firstLevelCoordinates.Add(new Vector2(-22, 10));
        firstLevelCoordinates.Add(new Vector2(-18, 10));
        firstLevelCoordinates.Add(new Vector2(-20, 15));
        firstLevelCoordinates.Add(new Vector2(-22, 15));
        firstLevelCoordinates.Add(new Vector2(-18, 15));

        firstLevelCoordinates.Add(new Vector2(20, 2));
        firstLevelCoordinates.Add(new Vector2(20, 0));
        firstLevelCoordinates.Add(new Vector2(20, -2));
        firstLevelCoordinates.Add(new Vector2(15, 2));
        firstLevelCoordinates.Add(new Vector2(15, 0));
        firstLevelCoordinates.Add(new Vector2(15, -2));
        //Invoke("createSwarm", 0.1f);
        //Invoke("createSwarm", 1f);
        //Invoke("createSwarm", 2f);

        Invoke("createZombie", 0.5f);
        Invoke("createZombie", 0.55f);
        Invoke("createZombie", 0.6f);
        Invoke("createZombie", 0.5f);
        Invoke("createZombie", 0.55f);
        Invoke("createZombie", 0.6f);

        Invoke("createZombie", 1f);
        Invoke("createZombie", 1.05f);
        Invoke("createZombie", 1.1f);
        Invoke("createZombie", 1f);
        Invoke("createZombie", 1.05f);
        Invoke("createZombie", 1.1f);

        Invoke("createZombie", 1.5f);
        Invoke("createZombie", 1.55f);
        Invoke("createZombie", 1.6f);
        Invoke("createZombie", 1.5f);
        Invoke("createZombie", 1.55f);

        Invoke("createMechSphere", 2.5f);

        player.GetComponent<PlayerController>().levelXP = 950;

        yield return null;
    }
    IEnumerator secondLevel()
    {
        firstLevelCoordinates.Add(new Vector2(5, 10));
        firstLevelCoordinates.Add(new Vector2(5, 0));
        firstLevelCoordinates.Add(new Vector2(5, -10));


        firstLevelCoordinates.Add(new Vector2(-20, -10));
        firstLevelCoordinates.Add(new Vector2(-18, -10));
        firstLevelCoordinates.Add(new Vector2(-22, -10));
        firstLevelCoordinates.Add(new Vector2(-20, -15));
        firstLevelCoordinates.Add(new Vector2(-18, -15));
        firstLevelCoordinates.Add(new Vector2(-22, -15));

        firstLevelCoordinates.Add(new Vector2(-20, 10));
        firstLevelCoordinates.Add(new Vector2(-22, 10));
        firstLevelCoordinates.Add(new Vector2(-18, 10));
        firstLevelCoordinates.Add(new Vector2(-20, 15));
        firstLevelCoordinates.Add(new Vector2(-22, 15));
        firstLevelCoordinates.Add(new Vector2(-18, 15));

        firstLevelCoordinates.Add(new Vector2(20, 2));
        firstLevelCoordinates.Add(new Vector2(20, 0));
        firstLevelCoordinates.Add(new Vector2(20, -2));
        firstLevelCoordinates.Add(new Vector2(15, 2));
        firstLevelCoordinates.Add(new Vector2(15, 0));
        firstLevelCoordinates.Add(new Vector2(15, -2));

        firstLevelCoordinates.Add(new Vector2(20, 2));
        firstLevelCoordinates.Add(new Vector2(20, 0));
        firstLevelCoordinates.Add(new Vector2(20, -2));
        firstLevelCoordinates.Add(new Vector2(15, 2));
        firstLevelCoordinates.Add(new Vector2(15, 0));
        firstLevelCoordinates.Add(new Vector2(15, -2));

        firstLevelCoordinates.Add(new Vector2(20, 2));
        firstLevelCoordinates.Add(new Vector2(20, 0));
        firstLevelCoordinates.Add(new Vector2(20, -2));
        firstLevelCoordinates.Add(new Vector2(15, 2));
        firstLevelCoordinates.Add(new Vector2(15, 0));
        firstLevelCoordinates.Add(new Vector2(15, -2));

        //Invoke("createSwarm", 0.1f);
        //Invoke("createSwarm", 1f);
        //Invoke("createSwarm", 2f);

        Invoke("createZombie", 0.5f);
        Invoke("createZombie", 0.55f);
        Invoke("createZombie", 0.6f);
        Invoke("createZombie", 0.5f);
        Invoke("createZombie", 0.55f);
        Invoke("createZombie", 0.6f);

        Invoke("createZombie", 1f);
        Invoke("createZombie", 1.05f);
        Invoke("createZombie", 1.1f);
        Invoke("createZombie", 1f);
        Invoke("createZombie", 1.05f);
        Invoke("createZombie", 1.1f);

        Invoke("createZombie", 1.5f);
        Invoke("createZombie", 1.55f);
        Invoke("createZombie", 1.6f);
        Invoke("createZombie", 1.5f);
        Invoke("createZombie", 1.55f);
        Invoke("createZombie", 1.6f);

        Invoke("createZombie", 2f);
        Invoke("createZombie", 2.05f);
        Invoke("createZombie", 2.1f);
        Invoke("createZombie", 2f);
        Invoke("createZombie", 2.05f);
        Invoke("createZombie", 2.1f);

        Invoke("createZombie", 2.5f);
        Invoke("createZombie", 2.55f);
        Invoke("createZombie", 2.6f);
        Invoke("createZombie", 2.5f);
        Invoke("createZombie", 2.55f);
        Invoke("createZombie", 2.6f);


        Invoke("createMechSphere", 3.5f);
        Invoke("createMechSphere", 3.55f);
        Invoke("createMechSphere", 3.6f);

        player.GetComponent<PlayerController>().levelXP = 1800;

        yield return null;
    }
    IEnumerator thirdLevel()
    {
        firstLevelCoordinates.Add(new Vector2(10, 10));
        firstLevelCoordinates.Add(new Vector2(10, 0));
        firstLevelCoordinates.Add(new Vector2(10, -10));
        firstLevelCoordinates.Add(new Vector2(5, 10));
        firstLevelCoordinates.Add(new Vector2(5, 0));
        firstLevelCoordinates.Add(new Vector2(5, -10));


        firstLevelCoordinates.Add(new Vector2(-20, -10));
        firstLevelCoordinates.Add(new Vector2(-18, -10));
        firstLevelCoordinates.Add(new Vector2(-22, -10));
        firstLevelCoordinates.Add(new Vector2(-20, -15));
        firstLevelCoordinates.Add(new Vector2(-18, -15));
        firstLevelCoordinates.Add(new Vector2(-22, -15));

        firstLevelCoordinates.Add(new Vector2(-20, 10));
        firstLevelCoordinates.Add(new Vector2(-22, 10));
        firstLevelCoordinates.Add(new Vector2(-18, 10));
        firstLevelCoordinates.Add(new Vector2(-20, 15));
        firstLevelCoordinates.Add(new Vector2(-22, 15));
        firstLevelCoordinates.Add(new Vector2(-18, 15));

        firstLevelCoordinates.Add(new Vector2(20, 2));
        firstLevelCoordinates.Add(new Vector2(20, 0));
        firstLevelCoordinates.Add(new Vector2(20, -2));
        firstLevelCoordinates.Add(new Vector2(15, 2));
        firstLevelCoordinates.Add(new Vector2(15, 0));
        firstLevelCoordinates.Add(new Vector2(15, -2));

        firstLevelCoordinates.Add(new Vector2(20, 2));
        firstLevelCoordinates.Add(new Vector2(20, 0));
        firstLevelCoordinates.Add(new Vector2(20, -2));
        firstLevelCoordinates.Add(new Vector2(15, 2));
        firstLevelCoordinates.Add(new Vector2(15, 0));
        firstLevelCoordinates.Add(new Vector2(15, -2));

        firstLevelCoordinates.Add(new Vector2(20, 2));
        firstLevelCoordinates.Add(new Vector2(20, 0));
        firstLevelCoordinates.Add(new Vector2(20, -2));
        firstLevelCoordinates.Add(new Vector2(15, 2));
        firstLevelCoordinates.Add(new Vector2(15, 0));
        firstLevelCoordinates.Add(new Vector2(15, -2));

        //Invoke("createSwarm", 0.1f);
        //Invoke("createSwarm", 1f);
        //Invoke("createSwarm", 2f);

        Invoke("createZombie", 0.5f);
        Invoke("createZombie", 0.55f);
        Invoke("createZombie", 0.6f);
        Invoke("createZombie", 0.5f);
        Invoke("createZombie", 0.55f);
        Invoke("createZombie", 0.6f);

        Invoke("createZombie", 1f);
        Invoke("createZombie", 1.05f);
        Invoke("createZombie", 1.1f);
        Invoke("createZombie", 1f);
        Invoke("createZombie", 1.05f);
        Invoke("createZombie", 1.1f);

        Invoke("createZombie", 1.5f);
        Invoke("createZombie", 1.55f);
        Invoke("createZombie", 1.6f);
        Invoke("createZombie", 1.5f);
        Invoke("createZombie", 1.55f);
        Invoke("createZombie", 1.6f);

        Invoke("createZombie", 2f);
        Invoke("createZombie", 2.05f);
        Invoke("createZombie", 2.1f);
        Invoke("createZombie", 2f);
        Invoke("createZombie", 2.05f);
        Invoke("createZombie", 2.1f);

        Invoke("createZombie", 2.5f);
        Invoke("createZombie", 2.55f);
        Invoke("createZombie", 2.6f);
        Invoke("createZombie", 2.5f);
        Invoke("createZombie", 2.55f);
        Invoke("createZombie", 2.6f);


        Invoke("createMechSphere", 3.5f);
        Invoke("createMechSphere", 3.55f);
        Invoke("createMechSphere", 3.6f);
        Invoke("createMechSphere", 3.5f);
        Invoke("createMechSphere", 3.55f);
        Invoke("createMechSphere", 3.6f);

        player.GetComponent<PlayerController>().levelXP = 2100;

        yield return null;
    }
    IEnumerator fourthLevel()
    {
        firstLevelCoordinates.Add(new Vector2(0, 15));
        Invoke("createBossEye", 0f);
        yield return null;
    }
    //Time while next level can't be started. I don't shure if I need it now, maybe I will delete it in future versions
    IEnumerator waitForSpawnStart(float time)
    {
        endSpawn = false;
        yield return new WaitForSeconds(time);
        endSpawn = true;
    }
    //create voids creates one enemy of chosen type in last coordinates of coordinates list
    public void createBossEye()
    {

        GameObject currentEye = Instantiate(bossEye, firstLevelCoordinates[firstLevelCoordinates.Count - 1], new Quaternion());
        currentEye.transform.parent = gameObject.transform;
        firstLevelCoordinates.RemoveAt(firstLevelCoordinates.Count - 1);
        allMobs.Add(currentEye);
    }
    public void createMechSphere()
    {

        GameObject currentMechSphere = Instantiate(mechSphere, firstLevelCoordinates[firstLevelCoordinates.Count - 1], new Quaternion());
        currentMechSphere.transform.parent = gameObject.transform;
        firstLevelCoordinates.RemoveAt(firstLevelCoordinates.Count - 1);
        allMobs.Add(currentMechSphere);
    }
    public void createZombie()
    {

        GameObject currentZombie = Instantiate(zombie, firstLevelCoordinates[firstLevelCoordinates.Count - 1], new Quaternion());
        currentZombie.transform.parent = gameObject.transform;
        firstLevelCoordinates.RemoveAt(firstLevelCoordinates.Count - 1);
        allMobs.Add(currentZombie);
    }
    void Update()
    {
        //I decided to calculate some interface stuff here becauce the code looks clearer this way
        mobCountText.text = "Enemies left: " + (allMobsCount-1);

        for (int i = allMobs.Count - 1; i > -1; i--)
        {
            if (allMobs[i] == null)
            {
                allMobs.RemoveAt(i);
            }
        }

        if (player.GetComponent<PlayerController>().hp > 0)
        {
            hpCounter.text = "" + player.GetComponent<PlayerController>().hp + "/150";
            gameObject.GetComponent<InterfaceController>().changeHeartToFull();
        }
        else
        {
            hpCounter.text = "DEAD";
            gameObject.GetComponent<InterfaceController>().changeHeartToDeath();
        }

        if (player.GetComponent<PlayerController>().currentAmmo > 0)
        {
            ammoCounter.text = "" + player.GetComponent<PlayerController>().currentAmmo + "/" + player.GetComponent<PlayerController>().maxAmmo;
        }
        else
        {
            ammoCounter.text = "RELOADING";
        }







        //this part of code starts new level(wave) or activates the final sreen
        if (endSpawn && allMobsCount <= 1 && !isChoosingPerk)
        {
            if (nextLevel == 5)
            {
                finalScreen.gameObject.SetActive(true);
                finalScreen.GetComponent<VideoPlayer>().Play();
                Camera.main.GetComponent<AudioSource>().Pause();
            }
            else
            {
                if (!perkController.GetComponent<PerkController>().isRunning)
                {
                    StartCoroutine(waitForSpawnStart(3f));
                    StartCoroutine(startLevels());
                }
            }
        }






        //camera follows the player
        if (player.transform)
        {
            camera.GetComponent<Rigidbody2D>().velocity = (player.transform.position-camera.transform.position)*20;
            
        }

    }
    public void restart()
    {
        StopAllCoroutines();
        Destroy(player);
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject.FindGameObjectWithTag("PickupController").GetComponent<PickupController>().player =
        GameObject.FindGameObjectWithTag("CraftController").GetComponent<CraftController>().player =
        player = Instantiate(playerPrefab, new Vector3(0, 0, 0), new Quaternion());
        player.SetActive(true);

        for (int i = 0; i < GameObject.FindGameObjectWithTag("PickupController").GetComponent<PickupController>().allPickups.Count; ++i)
        {
            Destroy(GameObject.FindGameObjectWithTag("PickupController").GetComponent<PickupController>().allPickups[i]);
        }

        GameObject.FindGameObjectWithTag("PickupController").GetComponent<PickupController>().allPickups = new List<GameObject>();
        GameObject.FindGameObjectWithTag("PickupController").GetComponent<PickupController>().weaponPicupsCount = 0;
        GameObject.FindGameObjectWithTag("PickupController").GetComponent<PickupController>().picupsCount = 0;

        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            obj.GetComponent<Enemy>().target = player;
        }

        Camera.main.GetComponent<AudioSource>().Play();
        Camera.main.GetComponent<AudioSource>().Pause();

        nextLevel = 1;
        endSpawn = false;
        finalScreen.gameObject.SetActive(false);
        StartCoroutine(waitForSpawnStart(6f));
        StartCoroutine(startLevels());
    }
}