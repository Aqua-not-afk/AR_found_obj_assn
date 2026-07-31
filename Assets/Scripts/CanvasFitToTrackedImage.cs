using UnityEngine;
using Zappar;

[RequireComponent(typeof(Canvas))]
public class CanvasFitToTrackedImage : MonoBehaviour
{
    [SerializeField] private ZapparImageTrackingTarget trackingTarget;

    private void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas.worldCamera == null)
        {
            ZapparCamera zc = FindAnyObjectByType<ZapparCamera>();
            if (zc != null)
                canvas.worldCamera = zc.GetComponent<Camera>();
        }

        if (trackingTarget == null)
            trackingTarget = GetComponentInParent<ZapparImageTrackingTarget>();

        if (trackingTarget?.PreviewImageObject == null) return;

        MeshFilter mf = trackingTarget.PreviewImageObject.GetComponent<MeshFilter>();
        if (mf?.sharedMesh == null) return;

        Bounds b = mf.sharedMesh.bounds;
        float w = b.size.x;
        float h = b.size.z;

        GetComponent<RectTransform>().sizeDelta = new Vector2(w * 1000f, h * 1000f);
    }
}