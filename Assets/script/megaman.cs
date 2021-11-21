using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float altura;
    [SerializeField] float fireRate1;
    [SerializeField] float TiempoAnim;
    [SerializeField] AudioClip sfx_bullet;
    [SerializeField] AudioClip sfx_death;

    Animator myAnimator;
    private Rigidbody2D MyRb;
    private BoxCollider2D myCollider;
    public bool Dash;
    public float Dash_T;
    public float vidas;

    public float Speed_Dash;
    bool doblesalto;
    float Reload;

    public Transform Firepoint;
    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        MyRb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movH = Input.GetAxis("Horizontal") ;
        if (movH != 0f && !Dash && !ChocaPared())
        {
            if (movH < 0f)
            {
                myAnimator.SetBool("itsRunning", true);
                Vector2 movimiento = new Vector2(-movH * Time.deltaTime * speed, 0);
                transform.Translate(movimiento);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            if (movH > 0f)
            {
                myAnimator.SetBool("itsRunning", true);
                Vector2 movimiento = new Vector2(movH * Time.deltaTime * speed, 0);
                transform.Translate(movimiento);
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
        else
        {
            myAnimator.SetBool("itsRunning", false);
        }
        salto();
        caer();
        disparo();
    }
    void FixedUpdate()
    {   
        Dash_Skill();
        falling();
    }
    bool ChocaPared()
    {
        float movH = Input.GetAxis("Horizontal");
        if (movH < 0f)
        {
            RaycastHit2D colision_pared = Physics2D.Raycast(myCollider.bounds.center, Vector2.left, myCollider.bounds.extents.x + 0.1f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(myCollider.bounds.center, Vector2.left * (myCollider.bounds.extents.x + 0.1f), Color.yellow);
            return colision_pared.collider != null;

        }
        if (movH > 0f)
        {
            RaycastHit2D colision_pared = Physics2D.Raycast(myCollider.bounds.center, Vector2.right, myCollider.bounds.extents.x + 0.1f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(myCollider.bounds.center, Vector2.right * (myCollider.bounds.extents.x + 0.1f), Color.yellow);
            return colision_pared.collider != null;
        }
        
        return true;

    }
    bool EstaEnSuelo()
    { 
        RaycastHit2D colision_suelo = Physics2D.Raycast(myCollider.bounds.center,Vector2.down,myCollider.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(myCollider.bounds.center, Vector2.down * (myCollider.bounds.extents.y + 0.1f), Color.green);
        return colision_suelo.collider != null;
    }
    void Dash_Skill()
    {
        if (Input.GetKey(KeyCode.X) && EstaEnSuelo())
        {
            Dash_T += 1 * Time.deltaTime;
            if (Dash_T < 0.5f)
            {
                Dash = true;
                myAnimator.SetBool("dash", true);
                transform.Translate(Vector3.right * Speed_Dash * Time.deltaTime);
                
            }
            else
            {
                Dash = false;
                myAnimator.SetBool("dash", false);

            }
        }
        else
        {
            Dash = false;
            myAnimator.SetBool("dash", false);
            Dash_T = 0;
        }

    }
    public void disparo()
    {
       
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= Reload && !Dash)
        {
            myAnimator.SetLayerWeight(1, 1);
            Instantiate(Bullet, Firepoint.position, Firepoint.rotation);
            AudioSource.PlayClipAtPoint(sfx_bullet, Camera.main.transform.position);
            Reload = Time.time + fireRate1;
            Dash = false;
            
        }
        else if (Reload < Time.time)
        {
            myAnimator.SetLayerWeight(1, 0);
        }

    }
    public void caer()
    {
        if(MyRb.velocity.y < 0 && !myAnimator.GetBool("takeof"))
        {
            myAnimator.SetBool("itsFalling", true);
        }
    }
    public void terminarsalto()
    {
        myAnimator.SetBool("itsFalling", true);
        myAnimator.SetBool("takeof", false);
    }
    public void salto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !Dash)
        {
            
            if (EstaEnSuelo())
            {

                doblesalto = true;
                MyRb.AddForce(new Vector2(0, altura), ForceMode2D.Impulse);
                myAnimator.SetTrigger("jumping");
                myAnimator.SetBool("takeof", true);
                Dash = false;
                
            }
            else if (doblesalto)
            {
                
                doblesalto = false;
                MyRb.velocity = Vector2.zero;
                MyRb.AddForce(new Vector2(0, altura), ForceMode2D.Impulse);
                myAnimator.SetTrigger("jumping");
                myAnimator.SetBool("takeof", false);
                
            }
        }


    }
    public void falling()
    {
        if (EstaEnSuelo())
        {
            
            myAnimator.SetBool("itsFalling", false);
            myAnimator.SetBool("takeof", false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objeto = collision.gameObject;
        string etiqueta = objeto.tag;
        if (etiqueta == "Enemy" || etiqueta == "balaEnemigo")
        {
            vidas--;
            if (vidas == 0)
            {
               
                AudioSource.PlayClipAtPoint(sfx_death, Camera.main.transform.position);
                Time.timeScale = 0;
                (GameObject.Find("GameManager").GetComponent<GameManager>()).GameOvermenu();

            }
        }

    }
}
