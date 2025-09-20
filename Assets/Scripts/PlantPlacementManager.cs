using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlantPlacementManager : MonoBehaviour
{
    public GameObject[] flowers;

    public XROrigin sessionOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    private void Update()
    {
        // ✅ Make sure there’s at least one touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ✅ Only act when the touch begins
            if (touch.phase == TouchPhase.Began)
            {
                // Raycast from touch position, not mouse position
                if (raycastManager.Raycast(touch.position, raycastHits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = raycastHits[0].pose;

                    // ✅ Random.Range upper bound is exclusive, so use flowers.Length
                    GameObject _object = Instantiate(flowers[Random.Range(0, flowers.Length)]);
                    _object.transform.position = hitPose.position;
                }

                // ✅ Disable detected planes after placement
                foreach (var plane in planeManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
                planeManager.enabled = false;
            }
        }
    }
}
