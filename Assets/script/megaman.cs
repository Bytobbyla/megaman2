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
        float movH = Input.GetAxis("Horizontal");


        if (movH != 0f)
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
    private void FixedUpdate()
    {
        disparo();
    }
    public void disparo()
    {
       
        if (Input.GetKeyDown(KeyCode.X) && Time.time >= Reload)
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
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myAnimator.SetBool("itsFalling", false);
            myAnimator.SetBool("takeof", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MyRb.AddForce(new Vector2(0, altura), ForceMode2D.Impulse);
                myAnimator.SetTrigger("jumping");
                myAnimator.SetBool("takeof", true);
            }
        }

       
    }
}
