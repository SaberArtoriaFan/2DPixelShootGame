using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

}
