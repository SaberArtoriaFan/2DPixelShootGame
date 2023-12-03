using UnityEngine;

public class Slash : MonoBehaviour
{
    PlayerController playerController;

    public void Init(PlayerController playerController)
    {
        this.playerController = playerController;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (playerController == null) return;
        if (other.gameObject.tag == "Enemy"&& playerController.GetNowGun() is ISlashGun slashGun)
        {
            Debug.Log("We have Hitted the Enemy!");
            //other.gameObject.GetComponent<Enemy>().TakenDamage(10000);

            #region 击退效果 反方向移动，从角色中心点「当前位置」向敌人位置方向「目标点」移动
            var dir = other.transform.position.x - playerController.transform.position.x > 0;
            var velocity = dir ? Vector2.right : Vector2.left;
            var rb = other.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(velocity.x * slashGun.SlashStrength, rb.velocity.y);
            #endregion
            this.playerController = null;
            GameObject.Destroy(this);
        }
    }
    private void OnDisable()
    {
        if(this.playerController!=null)
            GameObject.Destroy(this);
    }
}
