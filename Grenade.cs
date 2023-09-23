using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private Rigidbody2D rd;

    private Animator anim;
    private bool isTouched = false;
    private void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Init();


    }

    private void Update()
    {
        if (isTouched == false)
        {
            rd.transform.Rotate(new Vector3(0, 0, 1), Space.Self);
        }
       
    }
    //初始化炸弹运动
    private void Init()
    {
        anim.enabled = true;
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player.rotation.y == 180)
        {
            rd.AddForce(new Vector2(200f, 300f));
        }
        else
        { 
            rd.AddForce(new Vector2(-200f, 300f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().Hurt();
        }
        if (other.tag == "Boss")
        {

        }
        isTouched = true;

        rd.velocity = Vector2.zero;
        rd.transform.rotation = Quaternion.Euler(0, 0, 0);
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        anim.enabled = true;
        SoundMgr.Instance.PlayMusicByName("GrenadeExplosion");
    }

    public void DestroyBoom()
    {
        GameObject.Destroy(gameObject);
    }
}
