using System;
using UnityEngine;
using UnityEngine.U2D;

public class WallWobbler : MonoBehaviour
{
    [SerializeField] private SpriteShapeController _shape;

    // void Update()
    // {
    //     var random = new System.Random(UnityEngine.Random.Range(-100, 100));
    //     for (int i = 0; i < _shape.spline.GetPointCount(); i++)
    //     {
    //         var moveAmount = Math.Clamp(random.Next(), -30, +30);

    //         var pointPos = _shape.spline.GetPosition(i);
    //         pointPos.x *= (float)Math.Sin(moveAmount) * Time.deltaTime;

    //         _shape.spline.SetPosition(i, pointPos);
    //     }
    // }
}