using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    public GameObject leftTeleportationRay;
    public GameObject rightTeleportationRay;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel;

    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;

    void Update()
    {

        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos, out Vector3 leftNormal, out int leftPosInLine, out bool isLeftValid);
        bool isRightRayHovering = leftRay.TryGetHitInfo(out Vector3 rightPos, out Vector3 rightNormal, out int rightPosInLine, out bool isrightValid);

        if(leftTeleportationRay != null)
            leftTeleportationRay.SetActive(!isLeftRayHovering && leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);
        if(rightTeleportationRay != null)
            rightTeleportationRay.SetActive(!isRightRayHovering && rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
