using System.Linq;
using UnityEngine;

public class FireBullet : MonoBehaviour
{

    string bulletName;
    public Transform position;
    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        BulletFactory.Instance.InitializeBulletFactory();
        BulletManager.Instance.UpdateBulletManager();

    }

    // Update is called once per frame
    void Update()
    {
        BulletManager.Instance.UpdateBulletManager();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireTheBullet();
        }
    }
    void FireTheBullet()
    {
        direction = BulletFactory.Instance.RandomDirection();
        bulletName = BulletFactory.Instance.RandomStringName();
        Bullet fakeBullet = BulletFactory.Instance.CreateBullet(bulletName, position, direction);
        Debug.Log("Pooled Objects : " +   ObjectPool.Instance.pooledObjects.Count());
    }
}
