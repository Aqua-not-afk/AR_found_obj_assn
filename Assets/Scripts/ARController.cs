using UnityEngine;
using Zappar;

[RequireComponent(typeof(ZapparImageTrackingTarget))]
public class ARController : MonoBehaviour
{
    [Header("AR Content")]
    [SerializeField] private GameObject arContentRoot;

    [Header("Animations")]
    [SerializeField] private Animator cdCaseAnimator;
    [SerializeField] private Animator pointerSideAnimator;
    [SerializeField] private Animator pointerDownAnimator;
    [SerializeField] private Animator pointerSideUpAnimator;

    private ZapparImageTrackingTarget trackingTarget;

    private void Awake()
    {
        trackingTarget = GetComponent<ZapparImageTrackingTarget>();
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

    private void SetAnimators(bool enabled)
    {
        if (cdCaseAnimator != null)
            cdCaseAnimator.enabled = enabled;
        if (pointerSideAnimator != null)
            pointerSideAnimator.enabled = enabled;
        if (pointerDownAnimator != null)
            pointerDownAnimator.enabled = enabled;
        if (pointerSideUpAnimator != null)
            pointerSideUpAnimator.enabled = enabled;
    }

    private void OnImageFound()
    {
        if (arContentRoot != null)
            arContentRoot.SetActive(true);
        SetAnimators(true);
    }

    private void OnImageLost()
    {
        if (arContentRoot != null)
            arContentRoot.SetActive(false);
        SetAnimators(false);
    }
}
