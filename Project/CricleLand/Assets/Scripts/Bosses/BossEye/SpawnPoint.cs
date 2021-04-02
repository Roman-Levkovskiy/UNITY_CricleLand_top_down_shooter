using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    SpriteRenderer sr;
    public GameObject laserSprite;
    GameObject laser;
    public LineRenderer line;
    bool isShootingLaser;
    private void Start()
    {
        laserSprite.SetActive(false);
    }
    private void Update()
    {
        if(isShootingLaser)
        {
            RaycastHit2D[] hitInfo = Physics2D.RaycastAll(transform.parent.transform.position, transform.position, 1000.0f);
            if(hitInfo[hitInfo.Length-1])
            {
//                Debug.Log("" + transform.parent.transform.position + " " + transform.position);
                //float targetHit = 0;
                Vector3 targetHit = new Vector3();
                Debug.Log("Points:");
                foreach(RaycastHit2D ray in hitInfo)
                {
//                    Debug.Log("" + ray.point);
                    if(ray.transform.tag == "Border")
                    {
                        Debug.Log("" + ray.point);
                        targetHit = ray.point;  // Vector3.Distance(ray.point, transform.position);// - Vector3.Distance(transform.parent.transform.position, transform.position);
                        //break;
                    }
                }


                //float lngth = targetHit;
                //float lngth = Mathf.Sqrt(((transform.position).x - targetHit.x) * ((transform.position ).x - targetHit.x) + ((transform.position ).y - targetHit.y) * ((transform.position ).y - targetHit.y));
                //Vector3 parentPos = transform.parent.transform.position;
                //laser.transform.position = new Vector3(startPos.x + endPos.x, startPos.y + endPos.y) / 2f;
                //float lngth = Mathf.Sqrt((startPos.x - endPos.x) * (startPos.x - endPos.x) + (startPos.y - endPos.y) * (startPos.y - endPos.y));
                //laser.transform.localScale = new Vector3(2, lngth, 1);

                //Vector3 dir = startPos - parentPos;
                //Quaternion rotation = Quaternion.Euler(0, dir.y, 0);
                //transform.rotation = rotation;
                line.SetPosition(0, Vector3.zero);
                line.SetPosition(1, targetHit); //new Vector3(0,lngth,0));
            }
        }
    }
    public IEnumerator shootLaser()
    {
        laserSprite.SetActive(true);
        isShootingLaser = true;
        laser= Instantiate(laserSprite);
        yield return new WaitForSeconds(8);
        Destroy(laser);
        //isShootingLaser = false;
        laserSprite.SetActive(false);
    }

}
