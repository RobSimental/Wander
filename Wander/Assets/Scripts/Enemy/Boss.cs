using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private int heatUpPoint;
    bool heatUp = false;
    protected MusicManager music;
    public LevelChanger levelChanger;

    BoxCollider2D laser1, laser2;

    void Start() {
        GameObject GO = GameObject.Find("Music");
        music = (MusicManager)GO.GetComponent<MusicManager>();
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();

        heatUpPoint = getHealth() / 2;
        laser1 = transform.Find("Laser1").GetComponent<BoxCollider2D>();
        laser2 = transform.Find("Laser2").GetComponent<BoxCollider2D>();
        laser1.enabled = laser2.enabled = false;
    }

    void FixedUpdate()
    {
        if (death && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            music.bossMusic(2);
            levelChanger.FadeToLevel(1);
            Destroy(gameObject);
        }

        if (isHit && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            if ((getHealth() <= heatUpPoint) && (!heatUp)){
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Character/Monster/Boss/Charge Up", gameObject);
                music.bossMusic(1);
                heatUp = true;
            }
            isHit = false;
            animator.SetBool("isHit", isHit);
        }

        if (attacking && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            attacking = hitbox.enabled = false;
            animator.SetBool("basicAttack", attacking);
            animator.SetBool("laser1", attacking);
            animator.SetBool("laser2", attacking);
            animator.SetBool("attacking", attacking);
        }
    }

    public new void Attack()
    {
        if (!death && attackCoolDownTime <= Time.time)
        {
            attackCoolDownTime = Time.time + attackCoolDown;
            attacking = true;
            int attack = Random.Range(0, 3);
            if (attack == 0)
                animator.SetBool("basicAttack", attacking);
            else if (attack == 1)
                animator.SetBool("laser1", attacking);
            else
                animator.SetBool("laser2", attacking);
            animator.SetBool("attacking", attacking);
        }
    }

    public void EnableLaser1Hitbox()
    {
        laser1.enabled = true;
    }

    public void DisableLaser1Hitbox()
    {
        laser1.enabled = false;
    }

    public void EnableLaser2Hitbox()
    {
        laser2.enabled = true;
    }

    public void DisableLaser2Hitbox()
    {
        laser2.enabled = false;
    }
}
