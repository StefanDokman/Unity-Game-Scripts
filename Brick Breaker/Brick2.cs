using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick2 : MonoBehaviour
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
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();

        float spriteWidth = sRenderer.sprite.bounds.size.x;
        float spriteHeight = sRenderer.sprite.bounds.size.y;

        Vector2 leftTop = new Vector2(-spriteWidth / 2f, spriteHeight / 2f);
        Vector2 rightTop = new Vector2(spriteWidth / 2f, spriteHeight / 2f);
        Vector2 leftBot = new Vector2(-spriteWidth / 2f, -spriteHeight / 2f);
        Vector2 rightBot = new Vector2(spriteWidth / 2f, -spriteHeight / 2f);


        var edge = GetComponent<EdgeCollider2D>() == null ? gameObject.AddComponent<EdgeCollider2D>() : GetComponent<EdgeCollider2D>();

        var edgePoints = new[] { leftBot, leftTop, rightTop, rightBot, leftBot };
        edge.points = edgePoints;

        angleLT = Mathf.Atan2(leftTop.y, leftTop.x) * Mathf.Rad2Deg;
        angleRT = Mathf.Atan2(rightTop.y, rightTop.x) * Mathf.Rad2Deg;
        angleLB = Mathf.Atan2(leftBot.y, leftBot.x) * Mathf.Rad2Deg;
        angleRB = Mathf.Atan2(rightBot.y, rightBot.x) * Mathf.Rad2Deg;

        Debug.Log("angleLT: " + angleLT + " angleRT: " + angleRT + " angleLB: " + angleLB + " angleRB: " + angleRB);
    }

}
