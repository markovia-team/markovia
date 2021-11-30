﻿using System.Collections.Generic;
using UnityEngine;

namespace CodeMonkey.MonoBehaviours {
    public class CameraFollowSetup : MonoBehaviour {

        [SerializeField] private CameraFollow cameraFollow = null;
        [SerializeField] private Transform followTransform = null;
        [SerializeField] private float zoom = 50f;

        private void Start() {
            if (followTransform == null) {
                Debug.LogError("followTransform is null! Intended?");
                cameraFollow.Setup(() => Vector3.zero, () => zoom, true, true);
            } else {
                cameraFollow.Setup(() => followTransform.position, () => zoom, true, true);
            }
        }
    }

}