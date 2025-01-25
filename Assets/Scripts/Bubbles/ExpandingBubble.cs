using System;
using UnityEngine;

public class ExpandingBubble : SimpleBubble
{
    [Range(0.05f, 0.5f)]
    [SerializeField] private float _scalingSpeed = .1f;


    private float _scalingAmount = 0;


    protected new void FixedUpdate()
    {
        base.FixedUpdate();

        float scaling = 1 + (float)Math.Sin(_scalingAmount);
        transform.localScale = new Vector3(scaling, scaling);

        _scalingAmount += _scalingSpeed;
    }
}
