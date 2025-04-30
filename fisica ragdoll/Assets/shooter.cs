using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform shootPoint;
    public float fireRate = 0.5f; 
    private float fireTimer = 0f;
    private bool isShooting = false;

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
            isShooting = true;
        if (Input.GetMouseButtonUp(0))
            isShooting = false;

        if (isShooting)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                ShootArrow();
                fireTimer = fireRate;
            }
        }
    }

    void ShootArrow()
    {
        Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
    }
}
