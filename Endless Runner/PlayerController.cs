using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public GameObject center,right,left,up;
    GameObject currentPosition;
    public float moveDuration = 1f;
    public float jumpDuration = 1f;
    public float airTime = 1f;
    public ParticleSystem groundingEffect;
    private bool canMove = true;
    private bool canRoll = true;
    void Start()
    {
        anim = GetComponent<Animator>();
        currentPosition = center;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("isGrounded")) 
        {
            anim.SetTrigger("Jump");
            StartCoroutine(SwaperY(jumpDuration));

        }        
        if(Input.GetKeyDown(KeyCode.S) && canRoll) 
        {
            StartCoroutine(RollColliderSmall());
            StartCoroutine(CanRollAgain());
            anim.SetTrigger("RollForward");

        }        
        if(Input.GetKeyDown(KeyCode.A) && canMove) 
        {
                if (currentPosition == center)
                {
                    if (anim.GetBool("isGrounded"))
                    anim.SetTrigger("RollLeft");
                    canMove = false;
                    StartCoroutine(canMoveAgain());
                    StartCoroutine(SwaperX(left, moveDuration));
                    currentPosition = left;
                }
                if (currentPosition == right)
                {
                    if (anim.GetBool("isGrounded"))
                    anim.SetTrigger("RollLeft");
                    canMove = false;
                    StartCoroutine(canMoveAgain());
                    StartCoroutine(SwaperX(center, moveDuration));
                    currentPosition = center;
                }

        }        
        if(Input.GetKeyDown(KeyCode.D) && canMove) 
        {
                if (currentPosition == center)
                {
                    if (anim.GetBool("isGrounded"))
                    anim.SetTrigger("RollRight");
                    canMove = false;
                    StartCoroutine(canMoveAgain());
                    StartCoroutine(SwaperX(right, moveDuration));
                    currentPosition = right;

                }
                if (currentPosition == left)
                {
                    if(anim.GetBool("isGrounded"))
                    anim.SetTrigger("RollRight");
                    canMove = false;
                    StartCoroutine(canMoveAgain());
                    StartCoroutine(SwaperX(center, moveDuration));
                    currentPosition = center;
                }

        }
    }

    public IEnumerator SwaperX(GameObject location, float duration)
    {
        Vector3 position = this.transform.position;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            this.transform.position = new Vector3
                (
                    Mathf.Lerp(position.x, location.transform.position.x, t / duration),
                    transform.position.y,
                    transform.position.z
                );
            yield return null;
        }

        this.transform.position = new Vector3 (location.transform.position.x, transform.position.y,transform.position.z);
    }


    public IEnumerator SwaperY(float duration)
    {
        Vector3 position = this.transform.position;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            this.transform.position = new Vector3
                (
                    transform.position.x,
                    Mathf.Lerp(position.y, up.transform.position.y, t / duration),
                    transform.position.z
                );
            yield return null;
        }

        this.transform.position = new Vector3(transform.position.x, up.transform.position.y, transform.position.z);

        yield return new WaitForSeconds(airTime);

        position = this.transform.position;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            this.transform.position = new Vector3
                (
                    transform.position.x,
                    Mathf.Lerp(position.y, center.transform.position.y, t / duration),
                    transform.position.z
                );
            yield return null;
        }

        this.transform.position = new Vector3(transform.position.x, center.transform.position.y, transform.position.z);
    }


    bool IsPlaying(string stateName)
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(stateName) && stateInfo.normalizedTime < 1.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            anim.SetBool("isGrounded",true);
            groundingEffect.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            anim.SetBool("isGrounded", false);
        }
    }

    IEnumerator canMoveAgain()
    {
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }

    IEnumerator RollColliderSmall()
    {
        CapsuleCollider collider = GetComponentInChildren<CapsuleCollider>();
        Vector3 oldCenter = collider.center;
        collider.center = new Vector3(0, 0.35f, 0);
        float oldRadius = collider.radius;
        collider.radius = 0.4f;
        float oldHeight = collider.height;
        collider.height = 0.5f;

        yield return new WaitForSeconds(0.7f);

        collider.center = oldCenter;
        collider.radius = oldRadius;
        collider.height = oldHeight;

    }
    IEnumerator CanRollAgain()
    {
        canRoll = false;
        yield return new WaitForSeconds(0.7f);
        canRoll = true;

    }

}
