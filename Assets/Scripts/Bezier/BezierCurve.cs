using System;
using UnityEngine;

public class BezierCurve
{

    private Vector3[] points; 
    private readonly int order;
    private Vector3 startPoint;
    private Vector3 finishPoint; 
    private BezierCurve bc1 = null;
    private BezierCurve bc2 = null;
    private float t = 0; 
        
    public BezierCurve(Vector3[] points)
    {
        this.points = points; 
        order = points.Length - 1;
        startPoint = points[0];
        finishPoint = points[order];
        if (order > 0)
        {
            var auxary1 = new Vector3[points.Length - 1];
            Array.Copy(points, 1, auxary1, 0, auxary1.Length);
            bc1 = new BezierCurve(auxary1);
                
            var auxary2 = new Vector3[points.Length - 1];
            Array.Copy(points, 0, auxary2, 0, auxary1.Length);
            bc2 = new BezierCurve(auxary2);
        } 
    }

    public void Reset()
    {
        t = 0; 
    }
        
    public Vector3 Step(float increment)
    {
        t += increment;
        t = (t > 1 ? 1 : t);
        if (order > 0)
        {
            bc1.Step(increment);
            bc2.Step(increment);
        }

        return GenerateVector3(); 
    }   
    

    public Vector3 GenerateVector3()
    {
        if ( t >= 1 ) return Vector3.zero;
        if (order == 0) return finishPoint;
        return t * bc1.GenerateVector3() + (1-t)*bc2.GenerateVector3();
    }
}