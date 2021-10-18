using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Animator myAnimator;
    private Rigidbody2D MyRb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        MyRb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objeto = collision.gameObject;
        string etiqueta = objeto.tag;
        if(etiqueta != "Player")
        {
            MyRb.velocity = transform.right * 0;
            myAnimator.SetTrigger("explota");
            
            Destroy(gameObject,0.3f);
        }
        
    }
}
