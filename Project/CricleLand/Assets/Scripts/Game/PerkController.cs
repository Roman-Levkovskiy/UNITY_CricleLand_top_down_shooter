using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkController : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject perkPanel;
    public GameObject player;

    public int choosesLeft;
    public int perkNumberAttack;
    public int perkNumberHeal;
    public int perkNumberCraft;

    public bool isRunning = false;
    public bool isActive;
    void Start()
    {
        mainCanvas = GameObject.FindGameObjectWithTag("Canvas");
        perkPanel = mainCanvas.transform.Find("PerkPanel").gameObject;
        perkPanel.SetActive(false);
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
    }
    public IEnumerator showPerkPanel()
    {
        perkPanel.SetActive(true);
                
        while (choosesLeft > 0)
        {
            IEnumerator hideCoroutine = hidePerkPanel(5);
            StartCoroutine(hidePerkPanel(10));
            isRunning = true;

            perkNumberAttack = Random.Range(1, 3);
            perkNumberHeal = Random.Range(1, 3);
            perkNumberCraft = Random.Range(1, 3);
            Invoke("changeTextAttack" + perkNumberAttack, 0);
            Invoke("changeTextHeal" + perkNumberHeal, 0);
            Invoke("changeTextCraft" + perkNumberCraft, 0);
            yield return new WaitWhile(() => isRunning);
            StopCoroutine(hideCoroutine);
            --choosesLeft;
        }
        StartCoroutine(hidePerkPanel(0));
    }
    public IEnumerator hidePerkPanel(float time)
    {
        yield return new WaitForSeconds(time);
        isRunning = false;
        perkPanel.SetActive(false);
    }
    //called from perk buttons
    public void attackButtonWasClicked()
    {
        isRunning = false;
        Invoke("attack"+perkNumberAttack, 0);
    }

    public void healingButtonWasClicked()
    {
        Invoke("healing" + perkNumberHeal, 0);
        isRunning = false;
    }

    public void craftButtonWasClicked()
    {
        Invoke("craft" + perkNumberCraft, 0);
        isRunning = false;
    }

    void changeTextAttack1()
    {
        perkPanel.transform.Find("Attack").transform.Find("Text").GetComponent<Text>().text = "Damage multipler";
    }
    void changeTextAttack2()
    {
        perkPanel.transform.Find("Attack").transform.Find("Text").GetComponent<Text>().text = "Fire rate multipler";
    }
    void changeTextHeal1()
    {
        perkPanel.transform.Find("Healing").transform.Find("Text").GetComponent<Text>().text = "Get 3 medkits";
    }
    void changeTextHeal2()
    {
        perkPanel.transform.Find("Healing").transform.Find("Text").GetComponent<Text>().text = "Restore 100 hp";
    }
    void changeTextCraft1()
    {
        perkPanel.transform.Find("Craft").transform.Find("Text").GetComponent<Text>().text = "Get 3 Grenadays";
    }
    void changeTextCraft2()
    {
        perkPanel.transform.Find("Craft").transform.Find("Text").GetComponent<Text>().text = "Get 3 Freeze grenadays";
    }

    void attack1()
    {
        player.GetComponent<PlayerController>().damagePerkMultipler += 0.2f;
        ++player.GetComponent<PlayerController>().attackAbilityLevel;
    }
    void attack2()
    {
        player.GetComponent<PlayerController>().shotCDPerkMultipler *= 0.9f;
        ++player.GetComponent<PlayerController>().attackAbilityLevel;
    }
    void healing1()
    {
        player.GetComponent<PlayerController>().medkitCount += 3;
        ++player.GetComponent<PlayerController>().healAbilityLevel;
    }
    void healing2()
    {
        player.GetComponent<PlayerController>().hp += 100;
        if(player.GetComponent<PlayerController>().hp>150)
        {
            player.GetComponent<PlayerController>().hp = 150;
        }
        ++player.GetComponent<PlayerController>().healAbilityLevel;
    }
    void craft1()
    {
        ++player.GetComponent<PlayerController>().speedPerkMultipler;
        player.GetComponent<PlayerController>().grenadeCount += 3;
        ++player.GetComponent<PlayerController>().craftAbilityLevel;
    }
    void craft2()
    {
        ++player.GetComponent<PlayerController>().speedPerkMultipler;
        player.GetComponent<PlayerController>().grenade2Count += 3;
        ++player.GetComponent<PlayerController>().craftAbilityLevel;
    }
}
