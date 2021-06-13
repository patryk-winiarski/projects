using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // An old script made to manage dynamic sprite switching based on player direction and speed.

    private Animator anim;

    public string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    int lastDirection;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    public void SetDirection(Vector2 _direction)
    {
        string[] directionArray = null;

        if (_direction.magnitude < 1)
        {
            directionArray = staticDirections;
        }
        else
        {
            directionArray = runDirections;

            lastDirection = DirectionToIndex(_direction);
        }

        anim.Play(directionArray[lastDirection]);
    }


    private int DirectionToIndex(Vector2 _direction)
    {
        Vector2 norDir = _direction.normalized;

        float step = 360 / 8;
        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;

        if (angle < 0)
        {
            angle += 360;
        }

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }

}
