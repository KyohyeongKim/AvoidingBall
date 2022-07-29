using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private GameManager manager;
    private SpriteRenderer render;

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        render = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnLeftButtonClick();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            OnRightButtonClick();
        }
    }
    
    // 왼쪽 최대 -10, 오른쪽 최대 9

    public void OnLeftButtonClick()
    {
        if (manager.Health <= 0)
        {
            return;
        }

        if (transform.position.x >= -10f)
        {
            render.flipX = false;
            transform.Translate(-1f,0f,0f);
        }
        else
        {
            transform.position = new Vector2(-9f, -2.5f);
        }

    }

    public void OnRightButtonClick()
    {
        if (manager.Health <= 0)
        {
            return;
        }
                
        if (transform.position.x >= -10f)
        {
            render.flipX = true;
            transform.Translate(1f, 0f, 0f);
        }
        else
        {
            transform.position = new Vector2(9f, -2.5f);
        }
    }
}
