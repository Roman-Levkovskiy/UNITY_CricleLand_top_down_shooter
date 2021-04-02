using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftController : MonoBehaviour
{
    public GameObject player;

    public GameObject panel;
    public Vector3 panelStartPos;
    public Vector3 panelDownPos;
    public bool isMovingUp, isMovingDown, isPanelActive;

    public int Mat1Count, Mat2Count, Mat3Count;
    void Start()
    {
        Mat1Count = Mat2Count = Mat3Count = 0;   
    }
    //called even if obj is unactive
    private void Awake()
    {
        panel = GameObject.FindGameObjectWithTag("Canvas").transform.Find("Panel").gameObject;
        Vector3 pos = panel.transform.position;
        panelStartPos = pos;
        panel.transform.position = new Vector3(pos.x, pos.y - 400, pos.z);
        panelDownPos = new Vector3(panel.transform.position.x, -400, panel.transform.position.z);
    }

    void Update()
    {
        //hides the panel during the pause
        if (InterfaceController.isPaused)
        {
            panel.SetActive(false);    
        }
        else
        {
            panel.SetActive(true);
        }
        

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!InterfaceController.isPaused)
            {
                if (panel.transform.position.y > panelStartPos.y - 1)
                {
                    StartCoroutine(moveDown());
                }
                else
                {
                    StartCoroutine(moveUp());
                }
            }
        }

        if (!InterfaceController.isPaused)
        {
            if (!isPanelActive)
            {
                panel.transform.parent.transform.Find("CricleText").transform.Find("Text").GetComponent<Text>().text =
                ": " + player.GetComponent<PlayerController>().materials["Cricle"];

                panel.transform.parent.transform.Find("FireText").gameObject.GetComponent<Image>().transform.Find("Text").GetComponent<Text>().text =
                ": " + player.GetComponent<PlayerController>().materials["Fire"];

                panel.transform.parent.transform.Find("TriangleText").gameObject.GetComponent<Image>().transform.Find("Text").GetComponent<Text>().text =
                ": " + player.GetComponent<PlayerController>().materials["Triangle"];


                panel.transform.Find("GrenadeText").GetComponent<Text>().text = "";

                panel.transform.Find("MedkitText").GetComponent<Text>().text = "";

                panel.transform.Find("Grenade2Text").GetComponent<Text>().text = "";
            }
            else
            {
                panel.transform.Find("CricleText").transform.Find("Text").GetComponent<Text>().text =
                ": " + player.GetComponent<PlayerController>().materials["Cricle"];

                panel.transform.Find("FireText").gameObject.GetComponent<Image>().transform.Find("Text").GetComponent<Text>().text =
                ": " + player.GetComponent<PlayerController>().materials["Fire"];

                panel.transform.Find("TriangleText").gameObject.GetComponent<Image>().transform.Find("Text").GetComponent<Text>().text =
                ": " + player.GetComponent<PlayerController>().materials["Triangle"];


                panel.transform.Find("GrenadeText").GetComponent<Text>().text =
                    "" + player.GetComponent<PlayerController>().grenadeCount;

                panel.transform.Find("MedkitText").GetComponent<Text>().text =
                    "" + player.GetComponent<PlayerController>().medkitCount;

                panel.transform.Find("Grenade2Text").GetComponent<Text>().text =
                    "" + player.GetComponent<PlayerController>().grenade2Count;
            }
        }
    }

    public IEnumerator moveUp()
    {
        if (!isMovingUp)
        {
            isMovingUp = true;

            if (!isMovingDown)
            {
                while (panel.transform.position.y < panelStartPos.y)
                {
                    Vector3 pos = panel.transform.position;
                    panel.transform.position = new Vector3(pos.x, pos.y + 20, pos.z);
                    yield return new WaitForSeconds(0.01f);
                }
                isPanelActive = true;

                panel.transform.Find("CricleText").transform.Find("Text").GetComponent<Text>().gameObject.SetActive(true);

                panel.transform.Find("FireText").transform.Find("Text").GetComponent<Text>().gameObject.SetActive(true);

                panel.transform.Find("TriangleText").transform.Find("Text").GetComponent<Text>().gameObject.SetActive(true);

                panel.transform.parent.transform.Find("CricleText").gameObject.SetActive(false);

                panel.transform.parent.transform.Find("FireText").gameObject.SetActive(false);

                panel.transform.parent.transform.Find("TriangleText").gameObject.SetActive(false);

            }
            isMovingUp = false;
        }
    }

    public IEnumerator moveDown()
    {
        if (!isMovingDown)
        {
            isMovingDown = true;

            panel.transform.Find("CricleText").transform.Find("Text").GetComponent<Text>().gameObject.SetActive(false);

            panel.transform.Find("FireText").transform.Find("Text").GetComponent<Text>().gameObject.SetActive(false);

            panel.transform.Find("TriangleText").transform.Find("Text").GetComponent<Text>().gameObject.SetActive(false);

            panel.transform.parent.transform.Find("CricleText").gameObject.SetActive(true);

            panel.transform.parent.transform.Find("FireText").gameObject.SetActive(true);

            panel.transform.parent.transform.Find("TriangleText").gameObject.SetActive(true);

            if (!isMovingUp)
            {
                isPanelActive = false;
                while (panel.transform.position.y > panelDownPos.y)
                {
                    Vector3 pos = panel.transform.position;
                    panel.transform.position = new Vector3(pos.x, pos.y - 20, pos.z);
                    yield return new WaitForSeconds(0.01f);
                }
            }
            isMovingDown = false;
        }
    }

    //called from buttons on panel
    public void craftGrenade()
    {
        if (player.GetComponent<PlayerController>().materials["Fire"] > 0 && player.GetComponent<PlayerController>().materials["Cricle"] > 0)
        {
            --player.GetComponent<PlayerController>().materials["Fire"];
            --player.GetComponent<PlayerController>().materials["Cricle"];
            ++player.GetComponent<PlayerController>().grenadeCount;
        }

    }

    public void craftMedkit()
    {
        if (player.GetComponent<PlayerController>().materials["Triangle"] > 0)
        {
            --player.GetComponent<PlayerController>().materials["Triangle"];
            ++player.GetComponent<PlayerController>().medkitCount;
        }
    }

    public void craftGrenade2()
    {
        if (player.GetComponent<PlayerController>().materials["Cricle"] > 0)
        {
            --player.GetComponent<PlayerController>().materials["Cricle"];
            ++player.GetComponent<PlayerController>().grenade2Count;
        }
    }
}
