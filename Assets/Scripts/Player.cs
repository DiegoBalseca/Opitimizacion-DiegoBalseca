using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    private InputAction _shootAction;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _BulletSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _shootAction = InputSystem.actions["Attack"];
    }

    // Update is called once per frame
    void Update()
    {
        if(_shootAction.WasPerformedThisFrame())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //Instantiate(_bulletPrefab, _BulletSpawn.position, _BulletSpawn.rotation);

        GameObject bullet = PoolManager.Instance.GetPooledObject("Balas", _BulletSpawn.position, _BulletSpawn.rotation);
        bullet.SetActive(true);
    }
}
