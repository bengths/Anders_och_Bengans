using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nisse_fireball : MonoBehaviour {

    public float speed = 3 * 3.1415926535f;
    private float horzDir = 0.0f;

    public void setHorz_dir(float x)
    {
        horzDir = x;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(horzDir * speed, 0.0f);
    }


void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player" || trig.gameObject.tag == "ground")
            Destroy(this.gameObject);
    }

}
