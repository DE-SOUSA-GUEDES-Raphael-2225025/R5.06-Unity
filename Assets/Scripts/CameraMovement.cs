using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform followTarget;

    [SerializeField] private float sensivity = 3.0f;
    [SerializeField] private float TopClamp = 70f;
    [SerializeField] private float BottomClamp = 70f;


    private float targetYaw;
    private float targetPitch;

    private void LateUpdate() {
        CameraLogic();
    }

    private void CameraLogic() {
        float mouseX = Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        Debug.Log(mouseX * 100);

        targetPitch = UpdateRotation(targetPitch, mouseY, BottomClamp, TopClamp, true);
        targetYaw = UpdateRotation(targetPitch, mouseX, float.MinValue, float.MaxValue, false);

        ApplyRotation(targetPitch,targetYaw);
    }

    private void ApplyRotation(float pitch, float yaw) {
        followTarget.rotation = Quaternion.Euler(pitch, yaw, followTarget.eulerAngles.z);
    }

    private float UpdateRotation(float currentRotation, float input, float min, float max, bool isXAxis) {
        currentRotation += isXAxis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);
    }
}
