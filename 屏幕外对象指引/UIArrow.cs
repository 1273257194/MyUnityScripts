using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 屏幕外对象指引
/// </summary>
[RequireComponent(typeof(RectTransform))]
// [ExecuteInEditMode]
public class UIArrow : MonoBehaviour
{
    [FormerlySerializedAs("TargetTransform")]
    public Transform target;

    [FormerlySerializedAs("img")] public Image uiArrow;
    [FormerlySerializedAs("cam")] public Camera playerCamera;
    public float offsetLeft;
    public float offsetDown;
    public float offsetRight;
    public float offsetUp;
    public bool switchMethod;

    private void LateUpdate()
    {
        if (switchMethod)
        {
            Method1();
        }
        else
        {
            Method2();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Method1()
    {
        var cameraTransform = playerCamera.transform;
        var delta = target.position - cameraTransform.position;
        var dot = Vector3.Dot(cameraTransform.forward, delta);
        Vector3 newPos;
        if (dot < 0)
        {
            newPos = ManualWorldToScreenPoint(target.position);
            if (newPos.z < 0)
            {
                uiArrow.transform.position = playerCamera.WorldToScreenPoint(target.position);
                return;
            }

            newPos = KClamp(newPos);
        }
        else
        {
            newPos = playerCamera.WorldToScreenPoint(target.position);
        }

        uiArrow.transform.position = newPos;
    }

    private Vector3 KClamp(Vector3 newPos)
    {
        var center = new Vector2(Screen.width / 2f, Screen.height / 2f);
        var k = (newPos.y - center.y) / (newPos.x - center.x);

        if (newPos.y - center.y > 0)
        {
            newPos.y = Screen.height - offsetUp;
            newPos.x = center.x + (newPos.y - center.y) / k;
        }
        else
        {
            newPos.y = offsetDown;
            newPos.x = center.x + (newPos.y - center.y) / k;
        }

        if (newPos.x > Screen.width - offsetRight)
        {
            newPos.x = Screen.width - offsetRight;
            newPos.y = center.y + (newPos.x - center.x) * k;
        }
        else if (newPos.x < offsetLeft)
        {
            newPos.x = offsetLeft;
            newPos.y = center.y + (newPos.x - center.x) * k;
        }

        return newPos;
    }

    private Vector3 ManualWorldToScreenPoint(Vector3 wp)
    {
        // calculate view-projection matrix
        Matrix4x4 mat = playerCamera.projectionMatrix * playerCamera.worldToCameraMatrix;

        // multiply world point by VP matrix
        Vector4 temp = mat * new Vector4(wp.x, wp.y, wp.z, 1f);

        if (temp.w == 0f)
        {
            // point is exactly on camera focus point, screen point is undefined
            // unity handles this by returning 0,0,0
            return Vector3.zero;
        }

        // convert x and y from clip space to window coordinates
        temp.x = (temp.x / temp.w + 1f) * .5f * playerCamera.pixelWidth;
        temp.y = (temp.y / temp.w + 1f) * .5f * playerCamera.pixelHeight;
        return new Vector3(-temp.x, temp.y, wp.z);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Method2()
    {
        var camTransform = playerCamera.transform;

        var vFov = playerCamera.fieldOfView;
        var radHFov = 2 * Mathf.Atan(Mathf.Tan(vFov * Mathf.Deg2Rad / 2) * playerCamera.aspect);
        var hFov = Mathf.Rad2Deg * radHFov;

        var position = camTransform.position;
        var deltaUnitVec = (target.position - position).normalized;

        /* How the angles work:
         * vdegobj: objective vs xz plane (horizontal plane). Upright = -90, straight down = 90.
         * vdegcam: camera forward vs xz plane. same as above.
         * vdeg: obj -> cam. if obj is higher, value is negative.
         */

        var vdegobj = Vector3.Angle(Vector3.up, deltaUnitVec) - 90f;
        var forward = camTransform.forward;
        var right = camTransform.right;
        var vdegcam = Vector3.SignedAngle(Vector3.up, forward, right) - 90f;

        var vdeg = vdegobj - vdegcam;

        var hdeg = Vector3.SignedAngle(Vector3.ProjectOnPlane(forward, Vector3.up),
            Vector3.ProjectOnPlane(deltaUnitVec, Vector3.up), Vector3.up);

        vdeg = Mathf.Clamp(vdeg, -89f, 89f);
        hdeg = Mathf.Clamp(hdeg, hFov * -0.5f, hFov * 0.5f);

        var projectedPos = Quaternion.AngleAxis(vdeg, right) *
                           Quaternion.AngleAxis(hdeg, camTransform.up) * forward;
        Debug.DrawLine(position, position + projectedPos, Color.red);

        var newPos = playerCamera.WorldToScreenPoint(position + projectedPos);

        if (newPos.x > Screen.width - offsetRight || newPos.x < offsetLeft || newPos.y > Screen.height - offsetUp ||
            newPos.y < offsetDown)
            newPos = KClamp(newPos);

        uiArrow.transform.position = newPos;
    }
}