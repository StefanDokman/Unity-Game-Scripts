using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class edgeDetection : MonoBehaviour
{
    public float angleLT;
    public float angleRB;
    public float angleLB;
    public float angleRT;
    void Awake()
    {
        AddCollider();
    }

    void AddCollider()
    {
        Vector3 position = transform.position;
        if (Camera.main == null) { Debug.LogError("Camera.main not found, failed to create edge colliders"); return; }

        var cam = Camera.main;
        if (!cam.orthographic) { Debug.LogError("Camera.main is not Orthographic, failed to create edge colliders"); return; }

        var leftBot = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        var leftTop = (Vector2)cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
        var rightTop = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight, cam.nearClipPlane));
        var rightBot = (Vector2)cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, 0, cam.nearClipPlane));


        var edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

        var edgePoints = new[] { leftBot, leftTop, rightTop, rightBot, leftBot };
        edge.points = edgePoints;

        Vector2 leftTopAngle = leftTop - (Vector2)position;
        angleLT = Mathf.Atan2(leftTopAngle.y, leftTopAngle.x) * Mathf.Rad2Deg;
        if (angleLT < 0f)
        {
            angleLT += 360f;
        }

        Vector2 rightTopAngle = rightTop - (Vector2)position;
        angleRT = Mathf.Atan2(rightTopAngle.y, rightTopAngle.x) * Mathf.Rad2Deg;
        if (angleRT < 0f)
        {
            angleRT += 360f;
        }

        Vector2 leftBotAngle = leftBot - (Vector2)position;
        angleLB = Mathf.Atan2(leftBotAngle.y, leftBotAngle.x) * Mathf.Rad2Deg;
        if (angleLB < 0f)
        {
            angleLB += 360f;
        }

        Vector2 rightBotAngle = rightBot - (Vector2)position;
        angleRB = Mathf.Atan2(rightBotAngle.y, rightBotAngle.x) * Mathf.Rad2Deg;
        if (angleRB < 0f)
        {
            angleRB += 360f;
        }
        //Debug.Log("angleLT: " + angleLT + " " + "angleRT: " + angleRT + " " + "angleLB: " + angleLB + " " + "angleRB: " + angleRB);
        //Debug.Log("leftTop: " + leftTop + " " + "rightTop: " + rightTop + " " + "leftBot: " + leftBot + " " + "rightBot: " + rightBot);
        //Debug.Log("leftTopAngle: " + leftTopAngle + " " + "rightTopAngle: " + rightTopAngle + " " + "leftBotAngle: " + leftBotAngle + " " + "rightBotAngle: " + rightBotAngle + " This.transform.position: " + (Vector2)position);
        //Debug.Log("This.transform.position: " + position);
        //Debug.Log("This.transform.rotation.eulerAngles: " + this.transform.rotation.eulerAngles);
    }
}
