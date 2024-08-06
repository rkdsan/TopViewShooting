using UnityEngine;

public class ParticleAutoDestroy : MonoBehaviour
{
    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    private void LateUpdate()
    {
        bool isAlive = _particle.IsAlive();
        if (!isAlive)
        {
            Destroy(gameObject);
        }
    }


}
