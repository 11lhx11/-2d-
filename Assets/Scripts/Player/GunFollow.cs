using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GunFollow : MonoBehaviour
{
    public Camera mainCamera;
    public RectTransform gameCanvas;
    private  Vector3 mousePos;
    float angel = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 temp;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(gameCanvas,new Vector2(
            Input.mousePosition.x,Input.mousePosition.y),mainCamera,out temp);
        mousePos.x = temp.x;mousePos.y = temp.y;
        
        if (mousePos.x <= transform.localPosition.x)
        {
            
            angel=Vector3.Angle(Vector3.up,mousePos- transform.localPosition);
        }
        else
        {

            angel = -Vector3.Angle(Vector3.up, mousePos-transform.localPosition);
        }
        gameObject.transform.localRotation=Quaternion.Euler(0,0,angel);
    }
}
