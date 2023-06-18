using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour
{
    public Animator playerAnimator;
    public SpriteRenderer mySprite;
    public float moveDistance = 1;
    public GameObject player;
    public Vector2 endPosition;

    private Vector2 targetPosition;
    private Touch firstFinger;
    private Vector2 startPosition;
    private Vector2 direction;

    void Start()
    {
        targetPosition = player.transform.position;
    }

    void Update()
    {
        float speed = 2;
        float step = speed * Time.deltaTime;
        float distance = Vector3.Distance(player.transform.position, targetPosition);
        playerAnimator.SetFloat("Speed", distance);
        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, step);
        direction = (endPosition - startPosition).normalized;   
        if(Input.touchCount > 0)
        {
            firstFinger = Input.GetTouch(0);
            switch(firstFinger.phase)
            {
                case TouchPhase.Began:
                    startPosition = Camera.main.ScreenToWorldPoint(firstFinger.position);
                    endPosition = startPosition;
                    break;
                case TouchPhase.Moved:
                    endPosition = Camera.main.ScreenToWorldPoint(firstFinger.position);
                    break;
                case TouchPhase.Ended:
                    MoveDirection();
                    CheckSwipe();
                    break;
            }
        }   
    }

    void OnDouble()
    {
        Vector2 newPosition = Camera.main.ScreenToWorldPoint(firstFinger.position);
        player.transform.position = newPosition;
    }

    private void Move(float offset)
    {
        targetPosition = player.transform.position + new Vector3(offset, 0, 0);
    }

    void MoveDirection()
    {
        Vector3 playerPoint = Camera.main.WorldToScreenPoint(player.transform.position);
        float distance = Vector3.Distance(firstFinger.position, playerPoint);
        if(distance > 150 && firstFinger.position.x < playerPoint.x)
        {
            Debug.Log("left");
            if (GameManager.Instance.isSuperPlayer)
            {
                Move(-moveDistance * 1.4f);
            }
            else
            {
                Move(-moveDistance);
            }
            mySprite.flipX = true;
        } else if(distance > 150 && firstFinger.position.x > playerPoint.x)
        {
            Debug.Log("Right");
            if (GameManager.Instance.isSuperPlayer)
            {
                Move(moveDistance * 1.4f);
            }
            else
            {
                Move(moveDistance);
            }
            mySprite.flipX = false;
        }
    }
    private void CheckSwipe()
    {
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0)
            {
                Move(moveDistance * 2);
            } else
            {
                Move(-moveDistance * 2);
            }
        }
    }
}
