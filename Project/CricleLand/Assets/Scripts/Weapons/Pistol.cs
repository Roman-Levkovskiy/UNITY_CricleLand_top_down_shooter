using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    public Vector3 shootDirection;
    public GameObject laserFirePoint;
    public GameObject player;
    public LineRenderer lineRenderer;
    private float damageMultipler;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
    }

    public override IEnumerator shoot()
    {
        lineRenderer = GameObject.FindGameObjectWithTag("Line").GetComponent<LineRenderer>();

        damage = 200;
        damageMultipler = GameObject.Find("GameController").GetComponent<GameController>().player.GetComponent<PlayerController>().damagePerkMultipler;
        player = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().player;
        laserFirePoint = player.transform.Find("FirePoint").gameObject;
     
        shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        shootDirection.Set(shootDirection.x, shootDirection.y, 0);


        float L = 1f;
        float x1 = laserFirePoint.transform.position.x;
        float y1 = laserFirePoint.transform.position.y;
        float x2 = shootDirection.x;
        float y2 = shootDirection.y;
        float LL = Mathf.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        float x = x1 + (x2 - x1) * L / LL;
        float y = y1 + (y2 - y1) * L / LL;

        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(laserFirePoint.transform.position.x , laserFirePoint.transform.position.y ), shootDirection - player.transform.position);
        if (hitInfo.transform.gameObject.tag == "Enemy")
        {
            hitInfo.transform.GetComponent<Enemy>().takeDamage(damage * damageMultipler);
        }

        Vector2 A = new Vector3(laserFirePoint.transform.position.x - 1, laserFirePoint.transform.position.y - 1, 0f);
        Vector2 B = new Vector3(hitInfo.point.x-1, hitInfo.point.y-1, 0);
        
        L = 2;
        LL = Vector2.Distance(A, B);
        x = A.x + (B.x - A.x) * L / LL;
        y = A.y + (B.y - A.y) * L / LL;
        float x_old, y_old;
        for (int i = 0; i <= (int)LL / L + 1; ++i)
        {
            x_old = x;
            y_old = y;

            x = A.x + (B.x - A.x) * (L * i) / LL;
            y = A.y + (B.y - A.y) * (L * i) / LL;

            var v = new Vector2(x - x_old, y - y_old);
            var l = (float)Mathf.Sqrt(v.x * v.x + v.y * v.y);
            v = new Vector2(v.x / l, v.y / l);
            var newStart = new Vector2(x_old - v.x * 1.5f, y_old - v.y * 1.5f);
            var newEnd = new Vector2(x + v.x * 1.5f, y + v.y * 1.5f);

            lineRenderer.SetPosition(0, newStart);

            //            L = i / 6 * Vector2.Distance(A, B);

            if (i <= (int)LL / L)
            {
                lineRenderer.SetPosition(1, newEnd);
            }
            else
            {
                lineRenderer.SetPosition(1, B);
            }
            yield return new WaitForSeconds(0.01f);
            if (hitInfo)
            {
                if (hitInfo.transform.gameObject.tag == "Enemy")
                {
                    hitInfo.transform.gameObject.GetComponent<Enemy>().takeDamageEffect();
                }
            }
        }

        yield return new WaitForSeconds(0.01f);
        lineRenderer.SetPosition(0, new Vector3(0, 0, 0));
        lineRenderer.SetPosition(1, new Vector3(0, 0, 0));
        //new Vector3(shootDirection.x - 1, shootDirection.y - 1, 0f)
    }
}
