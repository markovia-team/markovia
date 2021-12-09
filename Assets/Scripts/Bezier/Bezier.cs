using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

public class Bezier : MonoBehaviour {
    private static Vector3 current;
    public GameObject focusPoint;
    public Vector3[] bezierNodes;
    private BezierCurve bezier;

    void Start() {
        var v1 = new Vector3(-10, 5, 10);
        var v2 = new Vector3(-10, 5, -10);
        var v3 = new Vector3(10, 5, -10);
        var v4 = new Vector3(10, 5, 10);
        bezier = new BezierCurve(bezierNodes);
    }

    void Update() {
        transform.LookAt(focusPoint.transform);
        var c = bezier.Step(0.001f);
        if (c == Vector3.zero) {
            bezierNodes[0] = transform.position;
            bezier = new BezierCurve(bezierNodes);
        }
        else {
            transform.position = c;
        }
    }

    public void OnDrawGizmos() {
        Gizmos.color = Color.red;
    }
}