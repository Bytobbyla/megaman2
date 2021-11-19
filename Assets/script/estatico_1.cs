using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estatico_1 : MonoBehaviour
{

    private float waitedTime;

    public float waitTimeToAttack = 3;

    public Animator animator;

    public GameObject bulletPrefab;
    public float vidas;
   
    public Transform lauchSpawnPoint;
    public Transform player_pos;
    Animator myAnimator;
    private BoxCollider2D myCollider;

    bool destruye = false;
    // Start is called before the first frame update
    void Start()
    {
        waitedTime = waitTimeToAttack;
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player_pos.position.x > this.transform.position.x && !destruye)
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
        if (!destruye)
        {
            newBullet = Instantiate(bulletPrefab, lauchSpawnPoint.position, lauchSpawnPoint.rotation);
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objeto = collision.gameObject;
        string etiqueta = objeto.tag;
        if (etiqueta == "bala")
        {
            vidas--;
            if (vidas == 0)
            {
                destruye = true;
                myCollider.enabled = false;
                Destroy(gameObject, 0.7f);
                myAnimator.SetTrigger("explota");
            }
        }

    }
   
        
    
}
