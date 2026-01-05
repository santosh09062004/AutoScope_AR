using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class arspanola : MonoBehaviour
{
    [Header("AR Object Placement")]
    public GameObject arObjectPrefab; // The object to place
    private GameObject placedObject;  // Reference to the placed object
    private ARRaycastManager raycastManager;

    [Header("AR Plane Management")]
    private ARPlaneManager planeManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [Header("Gesture Control")]
    private float initialDistance = 0f; // Initial distance between two fingers
    private Vector3 initialScale;       // Initial scale of the object

    void Start()
    {
        // Get AR Managers
        raycastManager = GetComponent<ARRaycastManager>();
        planeManager = GetComponent<ARPlaneManager>();

        if (raycastManager == null || planeManager == null)
        {
            Debug.LogError("ARRaycastManager or ARPlaneManager is missing in the scene.");
        }
    }

    void Update()
    {
        if (placedObject == null)
        {
            HandleTouchInput(); // Allow placement until the object is placed
        }

        HandlePinchToScale(); // Always allow scaling
    }

    // Handles object placement on a plane
    private void HandleTouchInput()
    {
        if (Input.touchCount == 1) // Single touch for object placement
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
            {
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;

                    if (placedObject == null)
                    {
                        // Instantiate the object at the hit position
                        placedObject = Instantiate(arObjectPrefab, hitPose.position, hitPose.rotation);

                        // Disable ARPlaneManager after placing the object
                        DisablePlaneDetection();
                    }
                }
            }
        }
    }

    // Handles pinch gesture to scale the object
    private void HandlePinchToScale()
    {
        if (Input.touchCount == 2) // Two-finger touch for scaling
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // Calculate the distance between the two touches
            float currentDistance = Vector2.Distance(touch1.position, touch2.position);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                initialDistance = currentDistance;
                if (placedObject != null)
                {
                    initialScale = placedObject.transform.localScale;
                }
            }
            else if (placedObject != null && initialDistance > 0)
            {
                // Calculate the scale factor
                float scaleFactor = currentDistance / initialDistance;

                // Apply the scale factor to the object
                placedObject.transform.localScale = initialScale * scaleFactor;
            }
        }
    }

    // Disables plane detection and hides planes
    private void DisablePlaneDetection()
    {
        planeManager.enabled = false; // Disable plane detection
        SetPlanesActive(false);      // Hide all currently detected planes
        Debug.Log("Plane detection disabled after object placement.");
    }

    // Activates or deactivates detected planes
    private void SetPlanesActive(bool isActive)
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(isActive);
        }
    }
}
