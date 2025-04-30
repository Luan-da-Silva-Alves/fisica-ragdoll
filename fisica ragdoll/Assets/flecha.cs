using UnityEngine;

public class flecha : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasHit = false;
    public GameObject arrowPrefab;
    public Transform shootPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * 50f; 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;

        hasHit = true;

        
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        
        transform.parent = collision.transform;


        ContactPoint contact = collision.contacts[0];
        transform.position = contact.point;
    }
    

    void Shoot()
    {
        Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
    }
}