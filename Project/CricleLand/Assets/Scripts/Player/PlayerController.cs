using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //UNSORTED
    public Animator animator;
    public float hp;
    public float speed = 5f;
    public int levelXP;
    public bool isTakingDamage = false;
    public Vector2 movement;
    public GameObject lookDir;
    public float totalXP;
    public Rigidbody2D rb;
    public Camera mainCamera;

    //WEAPONS
    public bool isShooted;
    public string currentWeapon;
    public GameObject plasma;
    public GameObject rifleBullet;
    public GameObject weaponTypes;
    public GameObject activateAbilitiesTypes;
    public int maxAmmo = 4;
    public int currentAmmo;
    bool isReloading;
    public string currentActivateAbility;

    //CONSUMABLE
    public int grenadeCount, medkitCount, grenade2Count;
    public Dictionary<string, int> materials;

    //AUDIO
    public AudioClip pistolAudio;
    public AudioClip rifleAudio;
    public AudioClip plasmaAudio;

    //Killing section
    public float lastKill;
    public float killsCount;

    //DASHING
    private bool isDashing;
    private float dashTimeLeft;
    private float lastDash = -100f;
    private Vector2 startVelocity;
    private Vector2 lastImagePos;

    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashColldown;

    //PERKS
    public int attackAbilityLevel;
    public int healAbilityLevel;
    public int craftAbilityLevel;

    public Dictionary<string, int> currentPoints;
    public bool wasActivated = false;
    public int shieldPoints;

    public float damagePerkMultipler;
    public float shotCDPerkMultipler;

    public float speedPerkMultipler;
    void Start()
    {
        currentWeapon = "Plasma";
        currentActivateAbility = "Explosion";

        grenadeCount = 3;
        medkitCount = 3;
        grenade2Count = 3;

        attackAbilityLevel = 6;
        healAbilityLevel = 1;
        craftAbilityLevel = 1;

        currentPoints = new Dictionary<string, int>();
        currentPoints.Add("Explosion", 0);
        currentPoints.Add("Shield", 0);
        currentPoints.Add("Components", 0);

        lastKill = -100;

        hp = 150;

        damagePerkMultipler = 1;
        shotCDPerkMultipler = 1;
        speedPerkMultipler = 1;


        rb = GetComponent<Rigidbody2D>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;


        materials = new Dictionary<string, int>();

        materials.Add("Cricle", 0);
        materials.Add("Fire", 0);
        materials.Add("Triangle", 0);

        isReloading = false;
        currentAmmo = maxAmmo;
        isShooted = false;

        lookDir = Instantiate(new GameObject(), new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), new Quaternion());
        lookDir.transform.parent = gameObject.transform;
        lookDir.name = "Look Direction";
    }
    void Update()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<InterfaceController>().changeWeaponIcon();
        GameObject.FindGameObjectWithTag("GameController").GetComponent<InterfaceController>().changeConsumableIcon();

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        inputCheck();

        if (hp <=0){
            Destroy(gameObject);
        }
    }
    void inputCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Time.time >= lastDash + dashColldown)
                attemptToDash();
        }
        if (((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && !isShooted && !isReloading) && !InterfaceController.isPaused)
        {
            IEnumerator coroutine = shoot();
            StartCoroutine(coroutine);
        }
        if (currentAmmo < maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            IEnumerator coroutine = reload();
            StartCoroutine(coroutine);
        }
        if (!wasActivated && Input.GetKeyDown(KeyCode.Space))
        {
            if (activateAbilitiesTypes.transform.Find(currentActivateAbility).GetComponent<ActivateAbility>().neededPoints <= currentPoints[currentActivateAbility])
            {
                IEnumerator coroutine = activateAbility();
                StartCoroutine(coroutine);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentActivateAbility = "Explosion";
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentActivateAbility = "Shield";
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentActivateAbility = "Components";
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<ConsumableController>().prevConsumable();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<ConsumableController>().nextConsumable();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Vector2 shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float L = 1f;
            float x1 = transform.position.x;
            float y1 = transform.position.y;
            float x2 = shootDirection.x;
            float y2 = shootDirection.y;
            float LL = Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            float x = x1 + (x2 - x1) * L / LL;
            float y = y1 + (y2 - y1) * L / LL;
            transform.Find("FirePoint").gameObject.transform.position = new Vector3(x, y, 0);
            x = x1 + (x2 - x1) * 3 / LL;
            y = y1 + (y2 - y1) * 3 / LL;
            GetComponent<ConsumableController>().useConsumable(new Vector2(x, y));
        }

    }
    //when shift is pressed, player will try to dash
    private void attemptToDash()
    {
        isDashing = true;
        startVelocity = rb.velocity.normalized;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instace.getFromPool();
        lastImagePos = transform.position;
    }
    //doing stuff when is dashing
    private void checkDash()
    {
        if(isDashing)
        {
            if (dashTimeLeft > 0)
            {
                rb.MovePosition(rb.position + movement * (speed + speedPerkMultipler ) * dashSpeed * Time.fixedDeltaTime * 4);
                dashTimeLeft -= Time.deltaTime;
                if (Vector2.Distance(transform.position, lastImagePos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instace.getFromPool();
                    lastImagePos = transform.position;
                }
            }
            else
            {
                isDashing = false;
            }
        }
    }
    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.MovePosition(rb.position + movement * (speed + speedPerkMultipler) * Time.fixedDeltaTime * 4);
        }
        checkDash();
        rotate();
    }
    //taking damage from enemies. This coroutine calls from another gameObjects, so it needs to call realTakeDamage otherwise it will stops with objects destroying
    public IEnumerator takeDamage(float damage)
    {
        if(!isTakingDamage)
        {
            StartCoroutine(realTakeDamage(damage));
        }
        yield return null;
    }
    private IEnumerator realTakeDamage(float damage)
    {
        isTakingDamage = true;
        if (shieldPoints > 0)
        {
            --shieldPoints;
        }
        else
        {
            hp -= damage;
        }
        yield return new WaitForSeconds(0.5f);
        isTakingDamage = false;
    }
    //called from enemies script
    public void killedAnEnemy(float exp)
    {
        if (Time.time - lastKill < 5f)
        {
            totalXP += exp * (6 - (Time.time - lastKill));
        }
        else
        {
            totalXP += exp;
        }

        lastKill = Time.time;
        ++killsCount;
        if(activateAbilitiesTypes.transform.Find(currentActivateAbility).GetComponent<ActivateAbility>().neededPoints > currentPoints[currentActivateAbility])
        {
            ++currentPoints[currentActivateAbility];
        }
    }
    public void levelUp()
    {
        int currentWaveLevel = 0;
        while(totalXP>=levelXP)
        {
            ++currentWaveLevel;
            totalXP -= levelXP;
        }
        GameObject.FindGameObjectWithTag("PerkController").GetComponent<PerkController>().choosesLeft += currentWaveLevel;
    }
    public IEnumerator activateAbility()
    {
        wasActivated = true;
        currentPoints[currentActivateAbility] = 0;
        switch(currentActivateAbility)
        {
            case "Explosion":
                StartCoroutine(activateAbilitiesTypes.transform.Find("Explosion").GetComponent<Explosion>().activate());
                break;
            case "Shield":
                StartCoroutine(activateAbilitiesTypes.transform.Find("Shield").GetComponent<Shield>().activate());
                break;
            case "Components":
                StartCoroutine(activateAbilitiesTypes.transform.Find("Components").GetComponent<Components>().activate());
                break;
        }

        yield return new WaitForSeconds(1f);
        wasActivated = false;
    }
    //current weapon can be changed, so it remember current wepon at the start moment
    IEnumerator reload()
    {
        string weapon = currentWeapon;
        isReloading = true;
        animator.SetBool("isWearing"+weapon, false);
        animator.SetBool("isReloading"+weapon, true);
        currentAmmo = 0;
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        animator.SetBool("isReloading" + weapon, false);
        animator.SetBool("isWearing" + currentWeapon, true);
        isReloading = false;
    }
    public IEnumerator shoot()
    {
        shotSoundPlay();

        isShooted = true;

        Vector2 shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float L = 1f;
        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float x2 = shootDirection.x;
        float y2 = shootDirection.y;
        float LL = Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        float x = x1 + (x2 - x1) * L / LL;
        float y = y1 + (y2 - y1) * L / LL;

        --currentAmmo;

        switch (currentWeapon)
        {
            case "Plasma":
                plasma = Instantiate(weaponTypes.transform.Find("Plasma").gameObject, new Vector2(x, y), new Quaternion());
                plasma.SetActive(true);
                StartCoroutine(plasma.GetComponent<Plasma>().shoot());
                yield return new WaitForSeconds(0.5f * shotCDPerkMultipler);
                break;

            case "Pistol":
                transform.Find("FirePoint").gameObject.transform.position = new Vector3(x, y, 0);
                StartCoroutine(weaponTypes.transform.Find("Pistol").GetComponent<Pistol>().shoot());
                yield return new WaitForSeconds(0.5f * shotCDPerkMultipler);
                break;

            case "Rifle":
                rifleBullet = Instantiate(weaponTypes.transform.Find("Rifle").gameObject, new Vector2(x, y), new Quaternion());
                rifleBullet.SetActive(true);
                StartCoroutine(rifleBullet.GetComponent<Rifle>().shoot());
                yield return new WaitForSeconds(0.2f * shotCDPerkMultipler);
                break;
        }

        if (currentAmmo <= 0)
        {
            IEnumerator coroutine = reload();
            StartCoroutine(coroutine);
        }

        isShooted = false;
    }
    public void shotSoundPlay()
    {
        if (!isShooted)
        {
            GameObject shotAudio = Instantiate(transform.Find("ShotAudio").gameObject);
            shotAudio.transform.parent = null;
            shotAudio.SetActive(true);
            switch (currentWeapon)
            {
                case "Pistol":
                    shotAudio.GetComponent<AudioSource>().clip = pistolAudio;
                    break;
                case "Rifle":
                    shotAudio.GetComponent<AudioSource>().clip = rifleAudio;
                    break;
                case "Plasma":
                    shotAudio.GetComponent<AudioSource>().clip = plasmaAudio;
                    break;
            }
            shotAudio.GetComponent<AudioSource>().Play();
            Destroy(shotAudio, 3f);
        }
    }
    public void rotate()
    {
        var relativePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lookDir.transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = rotation;
    }

}
