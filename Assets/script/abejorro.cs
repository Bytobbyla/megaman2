using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abejorro : MonoBehaviour
{
    Animator myAnimator;
    private Rigidbody2D MyRb;
    public Pathfinding.AIPath aipath;
    [SerializeField] AudioClip sfx_explosion;


    private Vector2 actualPos;

    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        MyRb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aipath.desiredVelocity.x >= 0.01f)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (aipath.desiredVelocity.x <= -0.01f)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject objeto = collision.gameObject;
        string etiqueta = objeto.tag;
        if (etiqueta == "bala")
        {
            aipath.enabled = false;
            MyRb.velocity = transform.right * 0;
            myAnimator.SetTrigger("explota");
            Destroy(gameObject, 0.7f);
            AudioSource.PlayClipAtPoint(sfx_explosion, Camera.main.transform.position);
        }

    }
   
}
