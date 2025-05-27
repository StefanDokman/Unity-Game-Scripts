using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GranadeThrow : MonoBehaviour
{
    public GameObject granadePre;
    public Transform BulletPoint;
    public float bulletForce;
    public GameObject granadeNumberText;
    public int granadeNumber;

    private void Start()
    {
        granadeNumberText = GameObject.Find("GranadeText");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && granadeNumber>0)
        {
            Shoot();
        }
    }
    void Shoot()
    {

        GameObject granade = Instantiate(granadePre, BulletPoint.position, BulletPoint.rotation);
        Rigidbody rb = granade.GetComponent<Rigidbody>();
        rb.AddForce(BulletPoint.forward * bulletForce, ForceMode.Impulse);
        granadeNumber--;
        granadeNumberText.GetComponent<TextMeshProUGUI>().SetText(granadeNumber.ToString());

    }
}
