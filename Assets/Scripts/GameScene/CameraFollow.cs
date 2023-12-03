using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
#region
//作者:Saber
#endregion
public class CameraFollow : Singleton<CameraFollow>
{
    public Transform Player;
    private Vector3 Pos;

    void LateUpdate()
    {
        Pos = Player.transform.position - gameObject.transform.position;
        Pos.z = 0;    //摄像机的图层不能变化，所以z一直是0
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, transform.position + Pos / 20,0.3f);
    }
    public void Shake(float duration,float strength=15)
    {
        transform.DOShakeRotation(duration, strength);
    }
    public IEnumerator CameraShakeCo(float _maxTime, float _amount)
    {
        Vector3 originalPos = transform.localPosition;
        float shakeTime = 0.0f;

        while (shakeTime < _maxTime)
        {
            float x = Random.Range(-1f, 1f) * _amount;
            float y = Random.Range(-1f, 1f) * _amount;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            shakeTime += Time.deltaTime;

            yield return new WaitForSeconds(0f);
        }
    }
}
