using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estatico_bullet : MonoBehaviour
{

    public float speed;

    public float lifeTime;

    public bool left;
    private Rigidbody2D MyRb;

    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        MyRb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MyRb.velocity = transform.right * -speed;
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objeto = collision.gameObject;
        string etiqueta = objeto.tag;
        if (etiqueta != "Enemy" &&  etiqueta != "bala")
        {
            MyRb.velocity = transform.right * 0;
            myAnimator.SetTrigger("explota");

            Destroy(gameObject, 0.3f);
        }

    }
}
