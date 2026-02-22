using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputAction _shootAction;

    [Header("Disparo")]
    [SerializeField] private Transform _BulletSpawn;

    void Start()
    {
        _shootAction = InputSystem.actions["Attack"];
    }

    void Update()
    {
        if (_shootAction != null && _shootAction.WasPerformedThisFrame())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = PoolManager.Instance.GetPooledObject("Balas", _BulletSpawn.position, _BulletSpawn.rotation);

        if (bullet != null)
        {
            bullet.GetComponent<Bullet>().SetDirection(Vector3.forward); // hacia arriba
            bullet.SetActive(true);
        }
    }
}