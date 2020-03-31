using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Target : MonoBehaviour
{
    public Transform target;//O que está sendo focado
    public float smoothTime = .15f;//Seguir o alvo
    Vector3 velocity = Vector3.zero;//Velocidadde

    public bool xMinEnabled = false;
    public float xMinValue = 0;
    public bool yMinEnabled = false;
    public float yMinValue = 0;
    public bool xMaxEnabled = false;
    public float xMaxValue = 0;
    public bool yMaxEnabled = false;
    public float yMaxValue = 0;

    private void FixedUpdate()
    {
        Vector3 targetPos = target.position;//Posição do alvo
        if (yMinEnabled && yMaxEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, yMinValue, yMaxValue);
        else if (yMinEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, yMinValue, target.position.y);
        else if (yMaxEnabled)
            targetPos.y = Mathf.Clamp(target.position.y, target.position.y, yMaxValue);
        
        if (xMinEnabled && xMaxEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, xMinValue, xMaxValue);
        else if (xMinEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, xMinValue, target.position.x);
        else if (xMaxEnabled)
            targetPos.x = Mathf.Clamp(target.position.x, target.position.x, xMaxValue);

        targetPos.z = transform.position.z;//Alinha a camera no eixo z
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);//Suavidade com que a camera se move
        if (targetPos == null)
            return;
    }

}
