using UnityEngine;
using UnityEngine.UI;
using Zappar;

[RequireComponent(typeof(ZapparImageTrackingTarget))]
public class ARController : MonoBehaviour
{
    [Header("AR Content")]
    [SerializeField] private GameObject arContentRoot;

    private ZapparImageTrackingTarget trackingTarget;
    private Canvas createdCanvas;

    private void Awake()
    {
        trackingTarget = GetComponent<ZapparImageTrackingTarget>();
    }

    private void Start()
    {
        ZapparCamera zc = FindAnyObjectByType<ZapparCamera>();
        if (zc == null) return;

        Camera cam = zc.GetComponent<Camera>();
        if (cam == null) return;

        if (arContentRoot == null) return;

        Canvas oldCanvas = arContentRoot.GetComponentInChildren<Canvas>(true);
        if (oldCanvas == null) return;

        CanvasFitToTrackedImage fitter = oldCanvas.GetComponent<CanvasFitToTrackedImage>();
        if (fitter != null) Destroy(fitter);

        createdCanvas = oldCanvas;
        createdCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        createdCanvas.worldCamera = cam;
        createdCanvas.planeDistance = 10f;
        createdCanvas.sortingOrder = 1000;

        oldCanvas.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        trackingTarget.OnSeenEvent.AddListener(OnImageFound);
        trackingTarget.OnNotSeenEvent.AddListener(OnImageLost);
    }

    private void OnDisable()
    {
        trackingTarget.OnSeenEvent.RemoveListener(OnImageFound);
        trackingTarget.OnNotSeenEvent.RemoveListener(OnImageLost);
    }

    private void OnImageFound()
    {
        if (arContentRoot != null)
            arContentRoot.SetActive(true);
    }

    private void OnImageLost()
    {
        if (arContentRoot != null)
            arContentRoot.SetActive(false);
    }
}
