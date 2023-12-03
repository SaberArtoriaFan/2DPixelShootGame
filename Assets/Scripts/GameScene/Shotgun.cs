using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ISlashGun
{
    float SlashStrength { get; }
}
public class Shotgun : Gun,ISlashGun
{
    public int bulletNum = 3;
    public float bulletAngle = 15;
    public float bulletFar = 1.5f;

    public float recoilSize = 1;

    float recoilTime = 0;
    Vector2 recoilForce;

    public float SlashStrength => 3;

    protected override void Fire()
    {
        //给予后坐力
        parent.canInput = false;
        recoilForce = (mousePos-(Vector2)transform.position).normalized * -1 * recoilSize;
        parent.Rb.velocity += recoilForce;
        CameraFollow.Instance.Shake(interval * 2 / 3,3);
        TimerManager.Instance.AddTimer(() =>
        {
            parent.canInput = true;
        },interval);

        animator.SetTrigger("Shoot");

        int median = bulletNum / 2;
        for (int i = 0; i < bulletNum; i++)
        {
            GameObject bullet = ObjectPool.Instance.GetObject(bulletPrefab);
            bullet.transform.position = muzzlePos.position;

            if (bulletNum % 2 == 1)
            {
                bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - median), Vector3.forward) * direction*bulletFar);
            }
            else
            {
                bullet.GetComponent<Bullet>().SetSpeed(Quaternion.AngleAxis(bulletAngle * (i - median) + bulletAngle / 2, Vector3.forward) * direction*bulletFar);
            }
            bullet.AddComponent<Slash>().Init(parent);
        }
        GameObject shell = ObjectPool.Instance.GetObject(shellPrefab);
        shell.transform.position = shellPos.position;
        shell.transform.rotation = shellPos.rotation;
    }

    //IEnumerator Recoil()
    //{
    //    recoilTime += Time.deltaTime;
    //    var lerp = recoilTime / interval;
    //    //var recoilForce = transform.forward.normalized * -1*recoilSize;
    //    //recoilForce.z = 0;
    //    recoilForce = Vector3.Lerp(recoilForce, Vector3.zero, lerp);
    //    parent.Rb.AddForce(recoilForce);
    //    yield return new WaitForFixedUpdate();
    //}
}
