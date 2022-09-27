using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class ThirdPersonCamera : MonoBehaviour {

	public bool lockCursor;
    [SerializeField] private CinemachineFreeLook freeLookCameraToZoom;
    [SerializeField] private float zoomSpeed = 20f;
    [SerializeField] private float zoomAcceleration = 5f;
    [SerializeField] private float zoomInnerRange = 25;
    [SerializeField] private float zoomOuterRange = 200;
    private float currentMiddleRigRadius = 35f;
    private float newMiddleRigRadius = 35f;
    [SerializeField] private float zoomYAxis = 0f;

    public float ZoomYAxis
    {
        get { return zoomYAxis; }
        set
        {
            if (zoomYAxis == value) return;
            zoomYAxis = value;
            AdjustCameraZoomIndex(ZoomYAxis);
        }
    }
    void Start() {
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
   
    private void Update()
    {
        ZoomYAxis = Input.GetAxis("Mouse ScrollWheel");
    }
   
    private void LateUpdate()
    {
        UpdateZoomLevel();
    }

    private void UpdateZoomLevel()
    {
        if (currentMiddleRigRadius == newMiddleRigRadius) { return; }
        currentMiddleRigRadius = Mathf.Lerp(currentMiddleRigRadius, newMiddleRigRadius, zoomAcceleration * Time.deltaTime);
        currentMiddleRigRadius = Mathf.Clamp(currentMiddleRigRadius, zoomInnerRange, zoomOuterRange);
        freeLookCameraToZoom.m_Orbits[1].m_Radius = currentMiddleRigRadius;
        freeLookCameraToZoom.m_Orbits[0].m_Height = freeLookCameraToZoom.m_Orbits[1].m_Radius;
        freeLookCameraToZoom.m_Orbits[2].m_Height = -freeLookCameraToZoom.m_Orbits[1].m_Radius;
    }

    public void AdjustCameraZoomIndex(float ZoomYAxis)
    {
        if (zoomYAxis == 0) { return; }
        if (zoomYAxis < 0)
        {
            newMiddleRigRadius = currentMiddleRigRadius + zoomSpeed;
        }
        if (zoomYAxis > 0)
        {
            newMiddleRigRadius = currentMiddleRigRadius - zoomSpeed;
        }

    }
}
