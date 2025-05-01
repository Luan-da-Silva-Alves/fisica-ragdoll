using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]

public class JenkaPiece : MonoBehaviour
{
    [Header("Drag settings")]
    public float maxDragDistance = 2f;


    [Header("Push Settings")]
    public float pushForce = 5f;
    public float doubleClickForceMultiplier = 2f;
    public float doubleClickThreshold = 0.3f;

    private Rigidbody rb;
    private Camera mainCamera;
    private bool IsDragging = false;
    private Vector3 offset;
    private Vector3 startDragposition;
    private float lastClickTime = -1f;




    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        //detecta clique duplo
        float timeSinceLastClick = Time.time - lastClickTime;
        lastClickTime = Time.time;

        //se for duplo clique aplica mais forca e n inicia o arrasto
        if (timeSinceLastClick <= doubleClickThreshold)
        {
            ApplyPush(pushForce * doubleClickForceMultiplier);
            return;
        }

        //se for clique simples, aplica forca normal
        ApplyPush(pushForce);


        //inicia arrasto
        IsDragging = true;
        rb.isKinematic = true;



        //calculca off set entre mouse e a pos atual da peca
        Vector3 mousePos = GetMouseWorldPosition();
        offset = transform.position - mousePos;
        startDragposition = transform.position;
    }


    private void OnMouseDrag()
    {
        if (!IsDragging) return;

        //obtem posição mousen o mundo
        Vector3 mousePos = GetMouseWorldPosition();
        Vector3 tagetPos = mousePos + offset;


        //limita a distancai do arrasto
        Vector3 dragVector = tagetPos - startDragposition;
        if(dragVector.magnitude > maxDragDistance)
        {
            dragVector = dragVector.normalized * maxDragDistance;
        }
        transform.position = startDragposition + dragVector;
        

    }

    private void OnMouseUp()
    {
        //libera arrasto e ativa fisica novamente
        IsDragging = false;
        rb.isKinematic = false;
    }

    private void ApplyPush(float force)
    {
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
        rb.AddForce(direction * force,ForceMode.Impulse);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        if(plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return transform.position;
    }
}
