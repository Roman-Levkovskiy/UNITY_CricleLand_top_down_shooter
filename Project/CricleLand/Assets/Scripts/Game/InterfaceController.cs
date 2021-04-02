using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    public AudioClip battle;
    public Image fullHeart;
    public Image damagedHeart;
    public static bool isPaused = false;
    public GameObject pausePanel;
    public GameObject mainCanvas;
    public GameObject player;

    void Start()
    {
        mainCanvas = GameObject.FindGameObjectWithTag("Canvas");
        Camera.main.GetComponent<AudioSource>().Play();
        damagedHeart.gameObject.SetActive(false);
    }

    public void changeHeartToDeath()
    {
        damagedHeart.gameObject.SetActive(true);
        fullHeart.gameObject.SetActive(false);
    }
    public void changeHeartToFull()
    {
        damagedHeart.gameObject.SetActive(false);
        fullHeart.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    private void FixedUpdate()
    {
        player = GetComponent<GameController>().player;

        //killcounter bar and counter
        float lastKillTime = Time.time - player.GetComponent<PlayerController>().lastKill;
        if (lastKillTime <= 4)
        {
            mainCanvas.transform.Find("KillsBar").GetComponent<Slider>().value = (4 - (lastKillTime)) / 4;
        }
        else
        {
            mainCanvas.transform.Find("KillsBar").GetComponent<Slider>().value = 0;
            player.GetComponent<PlayerController>().killsCount = 0;
        }
        mainCanvas.transform.Find("KillsBar").transform.Find("KillsCount").GetComponent<Text>().text = "" + player.GetComponent<PlayerController>().killsCount;
        
        //activate ability bar and counter
        string ability = player.GetComponent<PlayerController>().currentActivateAbility;
        int needed = player.GetComponent<PlayerController>().activateAbilitiesTypes.transform.Find(ability).GetComponent<ActivateAbility>().neededPoints;
        int current = player.GetComponent<PlayerController>().currentPoints[ability];

        mainCanvas.transform.Find("AbilityBar").transform.Find("PointsCount").GetComponent<Text>().text = "" + current + "/" + needed;
        mainCanvas.transform.Find("AbilityBar").GetComponent<Slider>().value = (float)current / needed;
    }
    public Sprite pistol;
    public Sprite rifle;
    public Sprite plasma;
    public Image weaponIcon;
    public void changeWeaponIcon()
    {
        switch (player.GetComponent<PlayerController>().currentWeapon)
        {
            case "Pistol":
                weaponIcon.sprite = pistol;
                break;
            case "Rifle":
                weaponIcon.sprite = rifle;
                break;
            case "Plasma":
                weaponIcon.sprite = plasma;
                break;
        }
    }

    public Sprite grenade;
    public Sprite medkit;
    public Sprite grenade2;
    public Image consumableIcon;
    public void changeConsumableIcon()
    {
        switch (player.GetComponent<ConsumableController>().currentConsumable)
        {
            case "Grenade":
                consumableIcon.sprite = grenade;
                break;
            case "Medkit":
                consumableIcon.sprite = medkit;
                break;
            case "Grenade2":
                consumableIcon.sprite = grenade2;
                break;
        }
    }

    public void resume()
    {
        Camera.main.GetComponent<AudioListener>().enabled = true;
        pausePanel.GetComponent<AudioSource>().Pause();
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Camera.main.GetComponent<AudioSource>().Play();
    }

    public void pause()
    {
        Camera.main.GetComponent<AudioSource>().Pause();
        pausePanel.SetActive(true);
        pausePanel.GetComponent<AudioSource>().Play();
        Time.timeScale = 0;
        isPaused = true;
    }

    public void exit()
    {
        Application.Quit();
    }
}
