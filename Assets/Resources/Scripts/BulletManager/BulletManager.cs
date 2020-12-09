
using System.Collections.Generic;
using System.Diagnostics;

public class BulletManager
{

    #region Singleton
    private static BulletManager instance;

    public static BulletManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BulletManager();
            }
            return instance;
        }
    }
    #endregion

    Dictionary<string, List<Bullet>> bulletDict;
    Stack<Bullet> bulletToRemoveStack;
    Stack<Bullet> bulletToAddStack;

    private BulletManager()
    {
        bulletDict = new Dictionary<string, List<Bullet>>();
        bulletToRemoveStack = new Stack<Bullet>();
        bulletToAddStack = new Stack<Bullet>();
    }

    public void UpdateBulletManager()
    {
       
        while (bulletToRemoveStack.Count > 0)
        {
            Bullet toRemove = bulletToRemoveStack.Pop();
            string bulletName = toRemove.name; 
            if (!bulletDict.ContainsKey(bulletName) || !bulletDict[bulletName].Contains(toRemove))
            {

                Debug.Print(bulletName + "Doesn't found in bulletDict");
            }
            else
            {
                bulletDict[bulletName].Remove(toRemove);
                ObjectPool.Instance.AddToPool(toRemove.name, toRemove);
                if (bulletDict[bulletName].Count == 0)
                    bulletDict.Remove(bulletName);
            }
        }



        while (bulletToAddStack.Count > 0)
        {
            Bullet toAdd = bulletToAddStack.Pop();
            string BulletName = toAdd.name;

            if (!bulletDict.ContainsKey(BulletName))
            {
                bulletDict.Add(BulletName, new List<Bullet>() { toAdd });
            }
            else if (!bulletDict[BulletName].Contains(toAdd))
            {
                bulletDict[BulletName].Add(toAdd);
            }
            else
            {

                Debug.Print("This Bullet Is Already in Directory");
            }
        }



    }

    public void AddBullet(Bullet toAdd)
    {
        bulletToAddStack.Push(toAdd);
    }

    public void RemoveBullet(Bullet toRemove)
    {
        bulletToRemoveStack.Push(toRemove);
    }

}
