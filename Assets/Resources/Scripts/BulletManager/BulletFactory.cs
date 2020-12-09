using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class BulletFactory 
{
    private static BulletFactory instance;

    public static BulletFactory Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BulletFactory();
            }
            return instance;
        }
    }

    Dictionary<string, GameObject> bulletPrefabs;

    string bulletPrefabPath = "Prefabs/Bullets/";
    string[] bulletName;


    public void InitializeBulletFactory()
    {
        //Loads all prefabs into the factory
        bulletPrefabs = new Dictionary<string, GameObject>();
        GameObject[] allPrefabs = Resources.LoadAll<GameObject>(bulletPrefabPath);
        foreach (GameObject prefab in allPrefabs)
        {
             
            Bullet newBullet = prefab.GetComponent<Bullet>();
            newBullet.bi.spriteName = newBullet.GetComponent<SpriteRenderer>().name;
            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.gravityScale = 0;
            bulletPrefabs.Add(newBullet.bi.spriteName, prefab);
          
        }
       bulletName = bulletPrefabs.Keys.ToArray();
       
    }


    // pos, dir, velo to create bullet


    public Bullet CreateBullet(string bulletName,Transform position , Vector2 direction)
    {
        GameObject toRetObj = null;
        IPoolable poolable = ObjectPool.Instance.RetrieveFromPool(bulletName); 
        if (poolable != null)
            toRetObj = poolable.GetGameObject;
        else
            toRetObj = _CreateBullet(bulletName,position,direction).gameObject;
        Bullet toRet = toRetObj.GetComponent<Bullet>();
        if (!toRet)
            Debug.LogError(  toRetObj.name + " did not contain a Bullet script. Returning Null");
        else
        {
            BulletManager.Instance.AddBullet(toRet);
        }
        return toRet;
    }

    public string RandomStringName()
    {
        return bulletPrefabs.Keys.ElementAt(Random.Range(0, bulletPrefabs.Count));
    }
    public Vector2 RandomDirection()
    {
        return Random.insideUnitCircle.normalized;
    }


    private Bullet _CreateBullet(string bulletName,Transform position, Vector2 direction)
    {
        if (!bulletPrefabs.ContainsKey(bulletName))
        {
            Debug.LogError("Bullet Of Name: " + bulletName + " not found in bulletPrefabsDict");
            return null;
        }
        GameObject newBulletObj = GameObject.Instantiate(bulletPrefabs[bulletName]);
        newBulletObj.name = bulletName; 
        Bullet newBullet = newBulletObj.GetComponent<Bullet>();
        newBullet.Initialize(direction);
        return newBullet;
    }

}
