using UnityEngine;
using System.Collections;

public class ObjectOnField : MonoBehaviour {

    public Renderer rend;
    public bool objectOnMouse = false;
    public Color myColor;
    void Start()
    {
        rend = GetComponent<Renderer>();
        myColor = rend.material.color;
    }

    void Update()
    {
        GetMaterials();
    }

    void OnMouseEnter()
    {
        rend.material.color = Color.gray;
        objectOnMouse = true;
    }
    void OnMouseExit()
    {
        rend.material.color = myColor;
        objectOnMouse = false;
    }


    void GetMaterials()
    {
        if (objectOnMouse)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(this.name + " 오브젝트를 클릭했다.");
            }
        }
    }


    public IEnumerator StartAction()
    {
        yield return new WaitForSeconds(0.1f);
    }
    
}
