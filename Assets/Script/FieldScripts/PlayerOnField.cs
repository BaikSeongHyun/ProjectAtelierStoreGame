using UnityEngine;
using System.Collections;

public class PlayerOnField : MonoBehaviour {

    private Camera _camera;
    private NavMeshAgent nav;
    private GameObject showPoint; //Sprite

    void Awake()
    {
        _camera = Camera.main;
        nav = GetComponent<NavMeshAgent>();
        showPoint = GameObject.Find("showPoint");
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;

            if (Physics.Raycast(ray, out Hit, Mathf.Infinity))
            {
                showPoint.transform.position = Hit.point;
                nav.SetDestination(Hit.point);
                Debug.DrawRay(ray.origin, ray.direction * 20f, Color.green);

            }
        }

        if (transform.position == nav.destination) return;

        float dis = Vector3.Distance(nav.destination, transform.position);

        if (dis < 0.1)
        {
            transform.position = nav.destination;

            //멈추면 채집,채광 시작 애니메이션 및 오브젝트의 HP(?)가 감소
        }



    }
}
