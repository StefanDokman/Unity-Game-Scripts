using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovementRB : MonoBehaviour
{

    float maxLeft = -7.888f;
    float maxRight = 7.888f;
    public float speed = 10f;
    public float angleLT;
    public float angleRB;
    public float angleLB;
    public float angleRT;
    public float MaxBounceAngle = 75;
 
    void Update()
    {


        if (Input.GetAxis("Mouse X") < 0 && this.transform.position.x >maxLeft)
        {
            this.transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);
          //  print("Mouse moved left");
        }
        if (Input.GetAxis("Mouse X") > 0 && this.transform.position.x < maxRight)
        {
            this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, maxLeft, maxRight), this.transform.position.y, 0);


           // print("Mouse moved right");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "MainCamera")
        {
            BallMovement Ball = collision.gameObject.GetComponent<BallMovement>();
            Vector3 position = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = position.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;
            float currentAngle = collision.gameObject.transform.rotation.z;
            float bounceAngle = (offset / width) * MaxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -MaxBounceAngle, MaxBounceAngle);

            Quaternion rotatition = Quaternion.AngleAxis(newAngle, Vector3.forward);

            collision.gameObject.transform.rotation = rotatition;
        }
    }
}

