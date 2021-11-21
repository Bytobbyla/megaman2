using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estatico_1 : MonoBehaviour
{

    private float waitedTime;

    public float waitTimeToAttack = 0.1f;
    [SerializeField] AudioClip sfx_explosion;
    [SerializeField] AudioClip sfx_bullet;

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
       
     
        if (DetectarJugador())
        {
            if (waitedTime <= 0)
            {
                waitedTime = waitTimeToAttack;
                Invoke("LauchBullet",0f);

            }
            else
            {
                waitedTime -= Time.deltaTime;
            }
        }
    }
    bool DetectarJugador()
    {
        if (player_pos.position.x > this.transform.position.x)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 5f, LayerMask.GetMask("Player"));
            Debug.DrawRay(transform.position, Vector2.right * 5f, Color.green);
            return hit.collider != null;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, 5f, LayerMask.GetMask("Player"));
            Debug.DrawRay(transform.position, Vector2.left * 5f, Color.green);
            return hit.collider != null;
        }
        
       
        
    }
    public void LauchBullet()
    {
        GameObject newBullet;
        if (!destruye)
        {
            newBullet = Instantiate(bulletPrefab, lauchSpawnPoint.position, lauchSpawnPoint.rotation);
            AudioSource.PlayClipAtPoint(sfx_bullet, Camera.main.transform.position);
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
