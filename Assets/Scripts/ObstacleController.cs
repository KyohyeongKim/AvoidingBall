using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private GameManager manager;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    
    private void FixedUpdate()
    {
        if (transform.position.y <= -5.75f)
        {
            // 점수 추가
            manager.OnPlayerAvoid();
            Destroy(gameObject);
        }
        else
        {
            transform.Translate(0f, -0.2f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            manager.OnPlayerHit();

            Destroy(gameObject);
        }
    }
}
