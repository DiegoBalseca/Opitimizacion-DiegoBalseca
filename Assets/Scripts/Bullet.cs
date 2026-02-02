using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody _rigidbody;
    [SerializeField] private float _bulletSpeed = 10f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody.AddForce(transform.forward * _bulletSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        gameObject.SetActive(false);
    }
}
