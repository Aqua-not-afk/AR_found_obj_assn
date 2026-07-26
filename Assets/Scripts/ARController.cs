using UnityEngine;
using Zappar;

[RequireComponent(typeof(ZapparImageTrackingTarget))]
public class ARController : MonoBehaviour
{
    [Header("AR Content")]
    [SerializeField] private GameObject arContentRoot;

    [Header("Animations")]
    [SerializeField] private Animator cdCaseAnimator;
    [SerializeField] private Animator[] pointerAnimators;

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

    private void OnImageFound()
    {
        if (arContentRoot != null)
            arContentRoot.SetActive(true);

        if (cdCaseAnimator != null)
            cdCaseAnimator.enabled = true;

        foreach (var anim in pointerAnimators)
        {
            if (anim != null)
                anim.enabled = true;
        }
    }

    private void OnImageLost()
    {
        if (arContentRoot != null)
            arContentRoot.SetActive(false);

        if (cdCaseAnimator != null)
            cdCaseAnimator.enabled = false;

        foreach (var anim in pointerAnimators)
        {
            if (anim != null)
                anim.enabled = false;
        }
    }
}
