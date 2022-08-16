using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class speedUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text tmp;
    public Rigidbody rb;


    // Update is called once per frame
    void Update()
    {
        tmp.text = rb.velocity.magnitude.ToString();
    }
}
