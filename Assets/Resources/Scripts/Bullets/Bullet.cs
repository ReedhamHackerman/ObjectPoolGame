using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]

public class Bullet : MonoBehaviour , IPoolable 
{
    public BulletInfo bi;

    public GameObject GetGameObject { get { return gameObject; } }

    public  void Initialize(Vector2 direction)
    {
        bi.rb = GetComponent<Rigidbody2D>();
        bi.spriteName = gameObject.GetComponent<SpriteRenderer>().name;
        direction =  GoInRandomDirection();
        
    }

    public Vector2 GoInRandomDirection()
    {
        bi.rb.velocity = (bi.forceOfTheBullet/bi.sizeOfBullet) * Random.insideUnitCircle.normalized;
        return bi.rb.velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Wall"))
        {

            DestroyBullet();
           
        }
       
 
    }


    public void DestroyBullet()
    {
        BulletManager.Instance.RemoveBullet(this);
    }


    public void Pooled()
    {
        transform.position = new Vector2(0, 0);
       // transform.position = GoInRandomDirection();
    }

    public void DePooled()
    {
         transform.position = GoInRandomDirection();
        //Debug.Log("Depooled");
    }

    [System.Serializable]
    public class BulletInfo
    {
        public Rigidbody2D rb;
        public float forceOfTheBullet, sizeOfBullet;
        public string spriteName;
    }

}
