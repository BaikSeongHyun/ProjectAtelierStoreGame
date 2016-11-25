using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    
	// main camera
	[SerializeField] Camera viewCamera;

	// field position
	[SerializeField] float xPosition = 10f;
	[SerializeField] float yPosition = 10f;
	[SerializeField] float zPosition = 10f;

	// field - rotation
	[SerializeField] float xRotation;
	[SerializeField] float yRotation;

	[SerializeField] Transform charPos;

    // pc test 
    [SerializeField] Transform target; // GameDataControl <- 끌어다가 넣으면 될것같아요
    [SerializeField] float dist = 10.0f; 
    [SerializeField] float height = 5.0f;
    [SerializeField] float dampRotate = 5.0f;
    [SerializeField] float TurnSpeed = 2f;
    [SerializeField] float camPos = 2f;

    // mobile
    [SerializeField] Transform charTarget;
    [SerializeField] Camera cameratarget;
    [SerializeField] Vector2 PrevPoint;

    [SerializeField] float moveSensitivityX = 1.0f;
    [SerializeField] float moveSensitivityY = 1.0f;
    [SerializeField] bool updateZoomSensitivity = true;
    [SerializeField] float orthoZoomSpeed = 0.05f;
    [SerializeField] float minZoom = 1.0f;
    [SerializeField] float maxZoom = 20.0f;
    [SerializeField] float perspectiveZoomSpeed = .5f;

    [SerializeField] Transform tr;




    // property

    public float XPosition { get { return xPosition; } set { xPosition = Mathf.Clamp( value, -30f, 30f ); } }

	public float YPosition { get { return yPosition; } set { yPosition = Mathf.Clamp( value, 0f, 100f ); } }

	public float ZPosition { get { return zPosition; } set { zPosition = Mathf.Clamp( value, -30f, 30f ); } }

	public float XRotation { get { return xRotation; } set { xRotation = Mathf.Clamp( value, 30f, 90f ); } }

	public float YRotation { get { return yRotation; } set { yRotation = Mathf.Clamp( value, 180f, 270f ); } }

	// unity method
	// awake
	void Awake()
	{
		viewCamera = GetComponent<Camera>();
        tr = GetComponent<Transform>();
        cameratarget = Camera.main;
    }

	// public method
	// camera set method
	public void SetCameraDefault( GameManager.GameMode state )
	{
		
	}

	// camera position use update
	public void SetCameraPosition()
	{	

		int count = Input.touchCount;
		// one touch
		if ( count == 1 )
		{
			if ( Input.GetTouch( 0 ).phase == TouchPhase.Moved )
			{			
				XPosition += Input.GetTouch( 0 ).deltaPosition.x * Time.deltaTime / Input.GetTouch( 0 ).deltaTime / 50f;
				ZPosition -= Input.GetTouch( 0 ).deltaPosition.x * Time.deltaTime / Input.GetTouch( 0 ).deltaTime / 50f;

				XPosition += Input.GetTouch( 0 ).deltaPosition.y * Time.deltaTime / Input.GetTouch( 0 ).deltaTime / 50f;
				ZPosition += Input.GetTouch( 0 ).deltaPosition.y * Time.deltaTime / Input.GetTouch( 0 ).deltaTime / 50f;
			}
		}	
		// two touch
		else if ( count == 2 )
		{
			// check y position
			if ( Input.GetAxis( "Mouse ScrollWheel" ) < 0 )
				YPosition = transform.position.y + Time.deltaTime * 10f;
			else if ( Input.GetAxis( "Mouse ScrollWheel" ) > 0 )
				YPosition = transform.position.y - Time.deltaTime * 10f;
		}
		viewCamera.transform.position = new Vector3( xPosition, yPosition, zPosition );
	}

	public void MoveObject()
	{
		//오류떠서 주석칩니다.
		//viewCamera.transform.position = charPos.position + new Vector3( xPosition, yPosition, zPosition );
	}

    public void SetCameraPositionPC()
    {
        Vector3 PositionInfo = tr.position - target.position;     // 주인공과 카메라 사이의 백터정보
        PositionInfo = Vector3.Normalize(PositionInfo);     // 화면 확대가 너무 급격히 일어나지 않도록 정규화~

        target.transform.Rotate(0, Input.GetAxis("Horizontal") * TurnSpeed, 0);      //마우스 입력이 감지되면 오브젝트 회전
        transform.RotateAround(target.position, Vector3.right, Input.GetAxis("Mouse Y") * TurnSpeed);     //마우스 Y(Pitch) 움직임 인지하여 화면 회전
        transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * TurnSpeed);     //마우스 Y(Pitch) 움직임 인지하여 화면 회전
        tr.position = tr.position - (PositionInfo * Input.GetAxis("Mouse ScrollWheel") * TurnSpeed);     // 마우스 휠로 화면확대 축고
    }

    public void SetCameraPositionMobile()
    {
        Vector3 PositionInfo = tr.position - charTarget.position;
        PositionInfo = Vector3.Normalize(PositionInfo);

        if (updateZoomSensitivity)
        {
            moveSensitivityX = cameratarget.orthographicSize / 5.0f;
            moveSensitivityY = cameratarget.orthographicSize / 5.0f;
        }

        Touch[] touches = Input.touches;

        if (cameratarget)
        {
            if (Input.touchCount == 1)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    PrevPoint = Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition;

                    charTarget.transform.Rotate(0, -(Input.GetTouch(0).position.x - PrevPoint.x), 0);
                    cameratarget.transform.RotateAround(charTarget.position, Vector3.right, -(Input.GetTouch(0).position.y - PrevPoint.y) * 0.5f);
                    cameratarget.transform.RotateAround(charTarget.position, Vector3.up, -(Input.GetTouch(0).position.x - PrevPoint.x) * 0.5f);

                    PrevPoint = Input.GetTouch(0).position;
                }
            }
        }
        if (cameratarget)
        {
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                float deltaMagnitudediff = prevTouchDeltaMag - touchDeltaMag;

                tr.position = tr.position - (PositionInfo * deltaMagnitudediff * orthoZoomSpeed * -1);
            }
        }
    }
}
