using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarView : MonoBehaviour
{
    Vector3 basePosition = new Vector3(-0.15f, 2.16f, -6.57f);
    bool isPhone = false, isFirst = true;
    float zRot = 0, yRot = 0;
    Vector3 startPos;
    Transform camTrans;
    private void OnEnable()
    {
        camTrans = transform.GetChild(0);
        //camTrans.localPosition = basePosition;
        if (SystemInfo.deviceType == DeviceType.Handheld)
            isPhone = true;
        isFirst = true;
    }
    void Update()
    {
        if (isPhone)
        {
            if (Input.touchCount > 0)
            {
                if (isFirst)
                {
                    isFirst = false;
                    startPos = Input.GetTouch(0).position;
                    return;
                }
                else
                {
                    zRot += (Input.GetTouch(0).position.y - startPos.y) * 0.09f;
                    zRot = Mathf.Clamp(zRot, -25, 45);
                    yRot += (Input.GetTouch(0).position.x - startPos.x) * 0.25f;
                    //yRot = Mathf.Clamp(yRot, -75, 75);
                    startPos = Input.GetTouch(0).position;
                }
                ScaleUp();
            }
            else
            {
                isFirst = true;
                zRot = Mathf.Lerp(zRot, 0, 0.08f);
                yRot = Mathf.Lerp(yRot, 0, 0.08f);
                ScaleDown();
            }
            transform.eulerAngles = new Vector3(5 + zRot, -65 + yRot, -3);
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (isFirst)
                {
                    isFirst = false;
                    startPos = Input.mousePosition;
                    return;
                }
                else
                {
                    zRot += (Input.mousePosition.y - startPos.y) * 0.1f;
                    zRot = Mathf.Clamp(zRot, -25, 45);
                    yRot += (Input.mousePosition.x - startPos.x) * 0.3f;
                    //yRot = Mathf.Clamp(yRot, -55, 55);
                    startPos = Input.mousePosition;
                }
                ScaleUp();
            }
            else
            {
                isFirst = true;
                zRot = Mathf.Lerp(zRot, 0, 0.2f);
                yRot = Mathf.Lerp(yRot, 0, 0.2f);
                ScaleDown();
            }
            transform.eulerAngles = new Vector3(5 + zRot, -65 + yRot, -3);
        }
    }
    void ScaleUp()
    {
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, new Vector3(-0.35f,2.16f,-4.77f), 0.075f);
    }
    void ScaleDown()
    {
        camTrans.localPosition = Vector3.Lerp(camTrans.localPosition, basePosition, 0.075f);
    }
}
