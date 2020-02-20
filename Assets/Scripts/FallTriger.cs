using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTriger : MonoBehaviour
{
    public Transform obj;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Player"))
      {
            EnableRB();
      }
    }
    public void EnableRB()
    {
        obj.GetComponent<Rigidbody2D>().simulated = true;
    }
}
