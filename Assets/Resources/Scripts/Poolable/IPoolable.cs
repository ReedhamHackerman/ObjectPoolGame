using UnityEngine;

public interface IPoolable
{
    void Pooled();
    void DePooled();
    GameObject GetGameObject { get; }
}
