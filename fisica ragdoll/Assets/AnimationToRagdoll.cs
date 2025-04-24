using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AnimationToRagdoll : MonoBehaviour
{
    [SerializeField] Collider myCollider;
    [SerializeField] float respawnTime = 30f;
    Rigidbody[] rigidbodies;
    bool bIsRagdoll = false;
     bool Recover = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Colisão detectada com: " + $" {JsonUtility.ToJson(collision)} ");
        //Debug.Log("Colisão detectada com: " + $" {collision.gameObject.name}" + $"- Tag: {collision.gameObject.tag} - bIsragdoll: " + $" {bIsRagdoll}");
        if (collision.gameObject.CompareTag("Inimigo") && !bIsRagdoll)
        {
            ToggleRagdoll(false);
            StartCoroutine(GetBackUp());
        }
    }

    private void ToggleRagdoll(bool bisAnimating)
    {
        bIsRagdoll = !bisAnimating;
        myCollider.enabled = bisAnimating;
        foreach (Rigidbody ragdollBoone in rigidbodies)
        {
            ragdollBoone.isKinematic = bisAnimating;
        }

       GetComponent<Animator>().enabled = bisAnimating;
        if (bisAnimating)
        {
            Animator animator = GetComponent<Animator>();

        }
       
    }

    private IEnumerator GetBackUp()
    {
        Animator animator = GetComponent<Animator>();
        yield return new WaitForSeconds(respawnTime);
        animator.SetTrigger("Recover");
        ToggleRagdoll(true);
    }

    /*void RandomAnimation()
    {
        int randomNum = Random.Range(0, 2);
        //Debug.Log(randomNum);
        Animator animator = GetComponent<Animator>();

        if(randomNum == 0)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }*/

}
