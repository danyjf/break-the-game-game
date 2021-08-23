using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Environment") {
            Destroy(this.gameObject);
        }else if(collision.tag == "Enemy") {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }else if(collision.tag == "Destroyable") {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
