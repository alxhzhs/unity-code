using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bullet : MonoBehaviour
{
    float speed = 140f;
  /*  private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "" + other.tag);
        if (other.CompareTag("Enermy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            ScoreManager.score += 10;
            GameManagerLogic.spawn_control = true;
        }
    }*/

    void Start()
    {
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        float amount = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * amount);
    }
  

}
