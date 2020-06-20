using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerControl : MonoBehaviour
{
    Animator anim;
    public GameObject runner;
    public SkinnedMeshRenderer skinnedMesh;
    public GameObject root;

    public float speed_Horizontal = 3f;
    public float speed_Run = 8f;
    public float speed_Jump = 4f;
    public float gravity = 9.8f;
    bool canIRun = true;
    bool canIControl = true;

    CharacterController cc;
    Vector3 v3;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.Play("RunForward1");
        cc = this.GetComponent<CharacterController>();
        v3 = new Vector3(0, 0, speed_Run);
    }

    // Update is called once per frame
    void Update()
    {
        if (canIControl)
        {
            if (Input.GetAxis("Horizontal") != 0)
            {
                v3.x = Input.GetAxis("Horizontal") * speed_Horizontal;
            }
            if (cc.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("w"))
                {
                    v3.y = speed_Jump;
                }
            }
        }
        else 
        {
            v3.x = 0;
        }
        v3.y -= gravity * Time.deltaTime;
        v3.z = speed_Run;
        //if(runner.transform.localPosition.z == -20)
        //    canIRun = true;
        if (canIRun)
        {
            cc.Move(v3 * Time.deltaTime);
        }
    }

    IEnumerator WaitOneFrame() {
        yield return new WaitForSeconds(Time.deltaTime);
        canIRun = true;
    }

    IEnumerator Wait3S()
    {
        yield return new WaitForSeconds(3);
        canIRun = true;
    }

    public delegate void resetAll();
    public event resetAll ResetAllEvent;
    public void Reset() 
    {
        canIRun = false;
        runner.transform.localPosition = new Vector3(runner.transform.localPosition.x, runner.transform.localPosition.y, -20);
        if (ResetAllEvent != null)
            ResetAllEvent();
        StartCoroutine("WaitOneFrame");
    }

    public void ReStrat()
    {
        canIRun = false;
        speed_Run = 8f;
        runner.transform.localPosition = new Vector3(0, 2.115f, -24);
        v3 = new Vector3(0, 0, speed_Run);
        StartCoroutine("Wait3S");
    }

    public void SlowDown(float slowSpeed) {
        speed_Run -= slowSpeed;
        StopCoroutine("AddSpeed");
        StartCoroutine("AddSpeed");
    }

    IEnumerator AddSpeed() {
        while (speed_Run < 8f) {
            speed_Run += 1f;
            if (speed_Run > 8f)
                speed_Run = 8f;
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BAndM" && runner.tag == "runner") {
            //碰到障碍/怪物，触发无敌
            StartCoroutine("BeInvincible");
            StartCoroutine(Flashing(4));
        } 
        else if (other.tag == "GameItem")
        {
            if (other.gameObject.name == "Heart")
            {
                //加血
                runner.GetComponent<RunnerAttribute>().Cure(3);
            }
            else if (other.gameObject.name == "Star")
            {
                //无敌+加速
                StopCoroutine("EatStar");
                StopCoroutine("BeInvincible");
                StartCoroutine("EatStar");
                StartCoroutine(Flashing(8));
            } 
            else if (other.gameObject.name == "Diamondo")
            {
                //加分
                GameObject.Find("GameControl").GetComponent<GameControl>().GetDiamondo();
            }
        }
    }

    IEnumerator EatStar() {
        runner.tag = "invincibleRunner";
        canIControl = true;
        StopCoroutine("AddSpeed");
        speed_Run = 13f;
        yield return new WaitForSeconds(8);
        speed_Run = 8f;
        runner.tag = "runner";
    }

/// <summary>
/// 无敌的闪烁 time为持续时间
/// </summary>
/// <param name="time"></param>
/// <returns></returns>
    IEnumerator Flashing(float time) {
        float flashingTime = 0;
        while (true) {
            flashingTime += Time.deltaTime*10;
            if (flashingTime >= time)
                break;
            if (skinnedMesh.enabled)
            {
                skinnedMesh.enabled = false;
                root.SetActive(false);
            }
            else 
            {
                skinnedMesh.enabled = true;
                root.SetActive(true);
            }
            yield return new WaitForSeconds(Time.deltaTime * 10);
        }
        skinnedMesh.enabled = true;
        root.SetActive(true);
    }



    /// <summary>
    /// 进入无敌
    /// </summary>
    /// <returns></returns>
    IEnumerator BeInvincible() {
        canIControl = false;
        //进入无敌
        runner.tag = "invincibleRunner";
        //倒地动画
        anim.Play("GetCrash");

        //后退
        speed_Run = -4f;
        yield return new WaitForSeconds(0.5f);

        speed_Run = 0f;
        yield return new WaitForSeconds(1);

        speed_Run = 2f;
        canIControl = true;
        StopCoroutine("AddSpeed");
        StartCoroutine("AddSpeed");

        yield return new WaitForSeconds(2.5f);

        //取消无敌
        runner.tag = "runner";
        print("取消无敌");
    }
}
