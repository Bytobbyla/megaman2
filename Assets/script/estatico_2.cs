using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estatico_2 : MonoBehaviour
{
    private float waitedTime;
    public Animator animator;

    public float waitTimeToAttack;
    public Transform player_pos;

    public GameObject bulletPrefab;
    public float vidas;

    public Transform lauchSpawnPoint1;
    public Transform lauchSpawnPoint2;
    Animator myAnimator;
    private BoxCollider2D myCollider;

    [SerializeField] AudioClip sfx_explosion;
    [SerializeField] AudioClip sfx_bullet;
    bool destruye = false;
    // Start is called before the first frame update
    void Start()
    {
        
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        disparo();


    }
    public void disparo()
    {
        if (DetectarJugador() && Time.time >= waitedTime)
        {
            //myAnimator.SetLayerWeight(1, 1);
            myAnimator.SetBool("shoot", true);
            Invoke("LauchBullet", 0f);
            AudioSource.PlayClipAtPoint(sfx_bullet, Camera.main.transform.position);
            waitedTime = Time.time + waitTimeToAttack;


        }
        else if (waitedTime < Time.time)
        {

            //myAnimator.SetLayerWeight(1, 0);
            myAnimator.SetBool("shoot", false);
        }
    }
    public void LauchBullet()
    {
        GameObject newBullet;
        if (!destruye)
        {
            
            newBullet = Instantiate(bulletPrefab, lauchSpawnPoint1.position, lauchSpawnPoint1.rotation);
            newBullet = Instantiate(bulletPrefab, lauchSpawnPoint2.position, lauchSpawnPoint2.rotation);
            
        }
      
    }
    bool DetectarJugador()
    {
        if (player_pos.position.x > this.transform.position.x)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 10f, LayerMask.GetMask("Player"));
            Debug.DrawRay(transform.position, Vector2.right * 10f, Color.green);
            return hit.collider != null;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 10f, LayerMask.GetMask("Player"));
            Debug.DrawRay(transform.position, Vector2.left * 10f, Color.green);
            return hit.collider != null;
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
                AudioSource.PlayClipAtPoint(sfx_explosion, Camera.main.transform.position);
            }
        }

    }
}
