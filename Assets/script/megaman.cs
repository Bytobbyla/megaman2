using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class megaman : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float altura;
    [SerializeField] float fireRate1;
    [SerializeField] float TiempoAnim;
    Animator myAnimator;
    private Rigidbody2D MyRb;
    private BoxCollider2D myCollider;
    public bool Dash;
    public float Dash_T;
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


        if (movH != 0f && !Dash)
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
        

        





    }
   
    void FixedUpdate()
    {
        disparo();
        Dash_Skill();
        falling();
    }
    void Dash_Skill()
    {
        

        if (Input.GetKey(KeyCode.X) && myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
       
        if (Input.GetKeyDown(KeyCode.E) && Time.time >= Reload)
        {
            myAnimator.SetLayerWeight(1, 1);
            Instantiate(Bullet, Firepoint.position, Firepoint.rotation);
            Reload = Time.time + fireRate1;
            
        }
        else if (Reload <= Time.time)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                MyRb.AddForce(new Vector2(0, altura), ForceMode2D.Impulse);
                myAnimator.SetTrigger("jumping");
                myAnimator.SetBool("takeof", true);
                doblesalto = true;
            }
            else if (doblesalto)
            {
                MyRb.velocity = Vector2.zero;
                MyRb.AddForce(new Vector2(0, altura), ForceMode2D.Impulse);
                myAnimator.SetTrigger("jumping");
                myAnimator.SetBool("takeof", true);
                doblesalto = false;
            }
        }


    }
    public void falling()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("itsFalling", false);
            myAnimator.SetBool("takeof", false);
        }
    }
}
