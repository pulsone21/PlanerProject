using System;
using UnityEngine;
namespace UISystem
{
    public class CameraController : MonoBehaviour
    {
        public Transform Target;
        [Serializable]
        class CameraState
        {
            [Header("Boundary Settings")]
            public bool Boundary;
            public float Bottom, Top, Left, Right;
            private float x, y, z;
            public void SetFromTransform(Transform t)
            {
                x = t.position.x;
                y = t.position.y;
                z = t.position.z;
            }
            public void Translate(Vector3 translation)
            {
                x += translation.x;
                y += translation.y;
                z += translation.z;

                if (Boundary)
                {
                    //Stoping movement over horizontal boundries
                    if (x > Right) x = Right;
                    if (x < Left) x = Left;
                    //Stoping movement over vertical boundries
                    if (y > Top) y = Top;
                    if (y < Bottom) y = Bottom;
                }
            }
            public void LerpTowards(CameraState target, float positionLerpPct)
            {
                x = Mathf.Lerp(x, target.x, positionLerpPct);
                y = Mathf.Lerp(y, target.y, positionLerpPct);
                z = Mathf.Lerp(z, target.z, positionLerpPct);
            }
            public void UpdateTransform(Transform t)
            {
                t.position = new Vector3(x, y, z);
            }
        }
        [SerializeField] private CameraState m_TargetCameraState = new CameraState();
        CameraState m_InterpolatingCameraState = new CameraState();
        [Header("Movement Settings")]
        [Tooltip("Exponential boost factor on translation."), Range(3.5f, 6f)]
        public float boost = 5f;
        [Tooltip("Time it takes to interpolate camera position 99% of the way to the target."), Range(0.001f, 1f)]
        public float positionLerpTime = 0.2f;
        void OnEnable()
        {
            m_TargetCameraState.SetFromTransform(transform);
            m_InterpolatingCameraState.SetFromTransform(transform);
        }
        Vector3 GetInputTranslationDirection()
        {
            Vector3 direction = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector3.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector3.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector3.right;
            }
            return direction;
        }
        void FixedUpdate()
        {
            Vector3 translation = Vector3.zero;
            // Translation
            translation = GetInputTranslationDirection() * (Time.fixedDeltaTime / Time.timeScale);
            // Speed up movement when shift key held
            if (Input.GetKey(KeyCode.LeftShift))
            {
                translation *= 10.0f;
            }
            translation *= Mathf.Pow(2.0f, boost);
            m_TargetCameraState.Translate(translation);
            // Framerate-independent interpolation
            // Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
            var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * (Time.fixedDeltaTime / Time.timeScale));
            m_InterpolatingCameraState.LerpTowards(m_TargetCameraState, positionLerpPct);
            m_InterpolatingCameraState.UpdateTransform(Target);
        }
    }
}