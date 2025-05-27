using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public int MovementSpeed = 1;
    public Rigidbody2D rb;
    public Transform objectB;

    void Update()
    {
        
         
        transform.position += transform.up * Time.deltaTime * MovementSpeed;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        objectB = collision.gameObject.transform;
        Vector2 direction;
        float angle = 0f;
        float angleLT=0;
        float angleRB=0;
        float angleLB=0;
        float angleRT=0;

        if (collision.gameObject.tag=="MainCamera") {
            edgeDetection obj1 = collision.gameObject.GetComponent<edgeDetection>();
             angleLT = obj1.angleLT;
             angleRB = obj1.angleRB;
             angleLB = obj1.angleLB;
             angleRT = obj1.angleRT;
        }
        if (collision.gameObject.tag == "Brick")
        {
            angleLT = 135;
            angleRB = 315;
            angleLB = 225;
            angleRT = 45;
            Debug.Log("Collided with Brick ");
        }

        if (collision.gameObject.tag != "Paddle")
        {
            direction = objectB.position - this.transform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = NormalizeAngle(angle);

            // Log the calculated angle
            Debug.Log("Angle enter " + angle);
            if (angle >= angleRT && angle < angleLT) { angle = 90; Debug.Log("Angle exit " + angle); }  //a>45 && a<135
            else if (angle >= angleLT && angle < angleLB) { angle = 180; Debug.Log("Angle exit " + angle); }  //a>135 && a<225
            else if (angle >= angleLB && angle < angleRB){ angle = 270; Debug.Log("Angle exit " + angle); } //a<225 && a<315
            else if ((angle >= angleRB && angle < 360) || (angle > 0 && angle < angleRT)){ angle = 0 ; Debug.Log("Angle exit " + angle);} //a>315 && a<360 || a>0 && a<45

        Quaternion rotation1 = this.transform.rotation;
            Vector3 thisRotation = rotation1.eulerAngles;
            Vector3 finalRotation = thisRotation - 2 * (thisRotation - new Vector3(0, 0, angle)); ;
            Quaternion resultRotation = Quaternion.Euler(finalRotation);
            this.transform.rotation = resultRotation;

            

        }
        if (collision.gameObject.tag == "Brick") collision.gameObject.SetActive(false);

    }
    float NormalizeAngle(float angle)
    {
        angle %= 360f;

        if (angle < 0f)
        {
            angle += 360f;
        }

        return angle;
    }
}