using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;

    [Header("Idle")]
    [SerializeField] private List<Sprite> nSprites_Idle;
    [SerializeField] private List<Sprite> neSprites_Idle;
    [SerializeField] private List<Sprite> eSprites_Idle;
    [SerializeField] private List<Sprite> seSprites_Idle;
    [SerializeField] private List<Sprite> sSprites_Idle;
    [SerializeField] private float idleFrameRate;


    [Header("Run")]
    [SerializeField] private List<Sprite> nSprites_Run;
    [SerializeField] private List<Sprite> neSprites_Run;
    [SerializeField] private List<Sprite> eSprites_Run;
    [SerializeField] private List<Sprite> seSprites_Run;
    [SerializeField] private List<Sprite> sSprites_Run;
    [SerializeField] private float runFrameRate;

    [SerializeField] private float moveSpeed;


    private float idleTime;
    private Vector2 direction;

    private void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        rb.linearVelocity = direction * moveSpeed;

        HandleSpriteFlip();

        SetSprite();
    }

    private void SetSprite()
    {
        List<Sprite> directionSprites = GetSpriteDirection();

        if (directionSprites != null)
        {
            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * runFrameRate);
            int frame = totalFrames % directionSprites.Count;

            sr.sprite = directionSprites[frame];
        }
        else
        {
            // Idle 상태일 때
            idleTime = Time.time;

            List<Sprite> idleSprites = GetIdleSpriteDirection();
            if (idleSprites != null && idleSprites.Count > 0)
            {
                float playTime = Time.time;
                int totalFrames = (int)(playTime * idleFrameRate);
                int frame = totalFrames % idleSprites.Count;

                sr.sprite = idleSprites[frame];
            }
        }
    }

    private void HandleSpriteFlip()
    {
        if (!sr.flipX && direction.x < 0)
        {
            sr.flipX = true;
        }
        else if (sr.flipX && direction.x > 0)
        {
            sr.flipX = false;
        }
    }

    private List<Sprite> GetIdleSpriteDirection()
    {
        List<Sprite> selectedSprites = null;

        if (direction.y > 0)
        {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = neSprites_Idle;
            }
            else
            {
                selectedSprites = nSprites_Idle;
            }
        }
        else if (direction.y < 0)
        {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = seSprites_Idle;
            }
            else
            {
                selectedSprites = sSprites_Idle;
            }
        }
        else
        {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = eSprites_Idle;
            }
            else
            {
                selectedSprites = sSprites_Idle; // 기본 Idle 방향
            }
        }

        return selectedSprites;
    }

    private List<Sprite> GetSpriteDirection()
    {
        List<Sprite> selectedSprites = null;

        if (direction.y > 0)
        {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = neSprites_Run;
            }
            else
            {
                selectedSprites = nSprites_Run;
            }
        }
        else if (direction.y < 0)
        {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = seSprites_Run;
            }
            else
            {
                selectedSprites = sSprites_Run;
            }
        }
        else
        {
            if (Mathf.Abs(direction.x) > 0)
            {
                selectedSprites = eSprites_Run;
            }
        }

        return selectedSprites;
    }
}
