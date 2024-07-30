using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PredictPlayerPos
{
    public static Vector2 GetPredictedPos(Rigidbody2D target, Vector3 pos, Vector2 objectVelocity, float projectileSpeed)
    {
        if(CalculateTargetDirection(target.transform.position, pos, target.velocity, objectVelocity, projectileSpeed, out Vector2 direction))
        {
            return direction * projectileSpeed;
        }
        else
        {
            return (target.transform.position - pos).normalized * projectileSpeed;
        }
    }
    
    private static bool CalculateTargetDirection(Vector2 a, Vector2 b, Vector2 vA, Vector2 vB, float sB, out Vector2 direction)
    {
        //a = target pos
        //b = current pos
        //vA = target velocity
        //vB = object (enemy) velocity
        //sB = projectile speed

        Vector2 aToB = b - a;
        float dC = aToB.magnitude;
        float alpha = Vector2.Angle(aToB, vA) * Mathf.Deg2Rad;
        float sA = vA.magnitude;
        float r = sA / sB;

        if(SolveQuadratic(1 - r * r, 2 * r * dC * Mathf.Cos(alpha), -(dC * dC), out float root1, out float root2) == 0)
        {
            direction = Vector2.zero;
            return false;
        }
        
        float dA = Mathf.Max(root1, root2);
        float t = dA / sB;
        Vector2 c = a + vA * t;

        Vector2 adjustedVelocity = vB * t;
        direction = ((c - adjustedVelocity) - b).normalized;
        
        return true;
    }

    private static int SolveQuadratic(float a, float b, float c, out float root1, out float root2)
    {
        float discriminent = b * b - 4 * a * c;

        if(discriminent < 0)
        {
            root1 = Mathf.Infinity;
            root2 = -root1;
            return 0;
        }

        float sqrt = Mathf.Sqrt(discriminent);
        float divisor = 2 * a;
        
        root1 = (-b + sqrt) / divisor;
        root2 = (-b - sqrt) / divisor;
        return discriminent > 0 ? 2 : 1;
    }
}
