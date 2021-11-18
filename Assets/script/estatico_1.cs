using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estatico_1 : MonoBehaviour
{

    private float waitedTime;

    public float waitTimeToAttack = 3;

    public Animator animator;

    public GameObject bulletPrefab;

   
    public Transform lauchSpawnPoint;
    public Transform player_pos;

    // Start is called before the first frame update
    void Start()
    {
        waitedTime = waitTimeToAttack;
    }

    // Update is called once per frame
    void Update()
    {
        if(player_pos.position.x > this.transform.position.x)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
       
        if (waitedTime <= 0)
        {
            waitedTime = waitTimeToAttack;
            Invoke("LauchBullet", 0.5f);
           
        }
        else
        {
            waitedTime -= Time.deltaTime;
        }
    }
    public void LauchBullet()
    {
        GameObject newBullet;
        newBullet = Instantiate(bulletPrefab, lauchSpawnPoint.position, lauchSpawnPoint.rotation);
    }
}
