using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    //显示指向精滤箭头的对象
    public GameObject ShowArrowObj;

    public GameObject ShowArrowYouKou_ChuanDong;

    public GameObject ArrowYouKou_ShouDong;
    
    public GameObject[] buttonCol;

    public Text txt_CurrContent;
    
    public GameObject JingLvModel;

    public GameObject JingLvModel01;

    public GameObject SkipColl;
    //油管部分跳选
    public GameObject SkipColl_01;

    public GameObject ShiBieObjSun;

    public GameObject btn_tuojie;

    //精滤拆装箭头
    public GameObject arr_JingLv;

    public GameObject arr_JingLv01;

    public GameObject NiuJunumber;

    //管道模型集合
    public GameObject[] pipeColl;
    //精滤光晕
    public GameObject JingLvTiShi;
	// Use this for initialization
	void Start () {

        Init();
	}
    void Init() {

        ShowArrowObj.SetActive(false);

        // ShowArrowYouKou.SetActive(false);

        JingLvModel.SetActive(false);
        JingLvTiShi.SetActive(false);

        JingLvModel01.SetActive(false);

        SkipColl.SetActive(false);
        SkipColl_01.SetActive(false);
        // Menu_ToJingLv.SetActive(false);
        HideMenu();
        HideArr_JingLv();
        HideArr_JingLv01();
        HideNiuJu();
        HideArrow_ChuanDong();
        HideArrow_ShouDong();
        HidePipeCol();
        //JingLvTiShi.SetActive(false);

    }

    public void HideNiuJu() {

        NiuJunumber.SetActive(false);

    }

    public void ShowNiuJu() {

        NiuJunumber.SetActive(true);

    }

    //隐藏管道
    public void HidePipeCol() {

        for (int i = 0; i < pipeColl.Length; i++) {

            pipeColl[i].SetActive(false);
        }
    }

    public void ShowPipeF5_1() {

        pipeColl[0].SetActive(true);
        Invoke("ShowPipeF4_1", 1.5f);
    }

    public void ShowPipeF4_1() {

        pipeColl[1].SetActive(true);
        Invoke("ShowPipeF3_1", 1.5f);
    }

    public void ShowPipeF3_1() {

        pipeColl[2].SetActive(true);
        Invoke("ShowPipeF2_1", 1.5f);
    }

    public void ShowPipeF2_1() {

        pipeColl[3].SetActive(true);
        Invoke("ShowPipeF1_1", 1.5f);
    }

    public void ShowPipeF1_1() {

        pipeColl[4].SetActive(true);
        Invoke("ShowPipeCV_1", 1.5f);
    }

    public void ShowPipeCV_1() {

        pipeColl[5].SetActive(true);
        Invoke("ShowPipeP_1", 1.5f);
    }

    public void ShowPipeP_1() {

        pipeColl[6].SetActive(true);
        Invoke("ShowPipeT_1", 1.5f);

    }

    public void ShowPipeT_1() {

        pipeColl[7].SetActive(true);
      
    }
    //隐藏指向传动油口的箭头
    public void HideArrow_ChuanDong() {

        ShowArrowYouKou_ChuanDong.SetActive(false);

    }

    //显示指向传动油口的箭头
    public void ShowArrow_ChuanDong() {

        ShowArrowYouKou_ChuanDong.SetActive(true);

    }
    //隐藏油口手动
    public void HideArrow_ShouDong() {

        ArrowYouKou_ShouDong.SetActive(false);

    }
    //显示油口手动
    public void ShowArrow_ShouDong() {

        ArrowYouKou_ShouDong.SetActive(true);

    }
    //播放管道部分动画
    public void PlayPipeAnimation() {

        Invoke("ShowPipeF5_1", 2f);
    }
	// Update is called once per frame
	void Update () {

        if (txt_CurrContent.gameObject.active == false) {

            return;
        }

        switch (ResManager.Step) {

            case 1:
                txt_CurrContent.text = "虚拟箭头指示精滤位置";
                HideNiuJu();
             
                break;
            case 2:
                txt_CurrContent.text = "显示精滤拆卸";
                HideNiuJu();
                break;
            case 3:
                txt_CurrContent.text = "操作拆卸";
                ShowNiuJu();
                break;
            case 4:
                txt_CurrContent.text = "显示精滤安装";
                HideNiuJu();
                break;
            case 5:
                txt_CurrContent.text = "操作安装";
                ShowNiuJu();
                break;
            case 6:
                txt_CurrContent.text = "虚拟箭头指向传动8个油口";
                break;
            case 7:
                txt_CurrContent.text = "虚拟箭头指向手动8个油口";
                break;
            case 8:
                txt_CurrContent.text = "油管连接手动传动的油口";
                break;

            case 9:
                txt_CurrContent.text = "操作连接";
                break;
            default:
                HideNiuJu();
                break;

        }

        //if (arr_JingLv.active == false)
        //{
            
        //    return;

        //}
        //else {

        //    switch (ResManager.Step) {

        //        case 3:
        //            arr_JingLv.transform.Rotate(0, 60 * Time.deltaTime, 0);
        //            break;
        //        default:
        //            HideArr_JingLv();
        //            break;
                
        //    }

        //}

        //if (arr_JingLv01.active == false)
        //{
        //    return;
        //}
        //else {

        //    switch (ResManager.Step) {

        //        case 5:
        //            arr_JingLv01.transform.Rotate(0, -60 * Time.deltaTime, 0);
        //            break;
        //        default:
        //            HideArr_JingLv01();
        //            break;
        //    }

        //}

       
	}

    public void TuoJieObj() {

        playSound();
        ShiBieObjSun.transform.parent = null;
        GameObject.Find("ARCamera").SetActive(false);
        btn_tuojie.SetActive(false);
    }
    //显示指向精滤的箭头
    public void ShowArrowToJingLv() {

        playSound();

        ResManager.Step = 1;

      //  JingLvTiShi.SetActive(true);
        ShowArrowObj.SetActive(true);

        Invoke("ShowModelAndUI", 5);

    }

    public void ShowArrowToYouKou() {

        playSound();
        ResManager.Step = 6;

        ShowArrow_ChuanDong();

        Invoke("EnterStepOne_1", 5);


    }
    public void playSound() {

        GameObject.Find("AudioSource01").GetComponent<AudioSource>().Play();

    }
    public void ShowModelAndUI() {

        //  Menu_ToJingLv.SetActive(true);
        EnterStepOne();

    }

    public void HideMenu() {

        for (int i = 0; i < buttonCol.Length; i++) {


            buttonCol[i].SetActive(false);
        }

    }

    public void EnterStepOne() {

        HideMenu();
        buttonCol[0].SetActive(true);
        buttonCol[1].SetActive(true);
        JingLvTiShi.SetActive(false);
        JingLvModel.SetActive(false);
        HideArr_JingLv();
        HideArr_JingLv01();
      //  HideNiuJu();
        ResManager.Step = 1;

    }

    public void EnterStepOne_1()
    {

        HideMenu();
        buttonCol[0].SetActive(true);
        buttonCol[1].SetActive(true);
        ResManager.Step = 6;
        ShowArrow_ChuanDong();
        HideArrow_ShouDong();
        HidePipeCol();
    }
    //public void 

    public void EnterStepTwo() {

        HideMenu();
        buttonCol[0].SetActive(true);
        buttonCol[1].SetActive(true);
        buttonCol[2].SetActive(true);
        HideArr_JingLv();
        HideArr_JingLv01();
      //  HideNiuJu();
        ResManager.Step = 2;

    }

    public void EnterStepTwo_2() {

        HideMenu();
        buttonCol[0].SetActive(true);
        buttonCol[1].SetActive(true);
        buttonCol[2].SetActive(true);
        ResManager.Step = 7;
        ShowArrow_ShouDong();
       // HideArrow_ChuanDong();
        HidePipeCol();

    }

    public void EnterStepThree_3() {

        EnterStepTwo_2();
        ResManager.Step = 8;
        HideArrow_ChuanDong();
        HideArrow_ShouDong();
       
    }

   
    public void EnterStepThree() {

        EnterStepTwo();
        ResManager.Step = 3;
        ShowArr_JingLv();
        //ShowNiuJu();
        HideArr_JingLv01();
    }

    public void EnterStepFour() {


        EnterStepTwo();
        ResManager.Step = 4;
        HideArr_JingLv01();
        HideArr_JingLv();
       // HideNiuJu();
    }

    public void EnterStepFour_4() {

        HideMenu();
        buttonCol[1].SetActive(true);
        buttonCol[2].SetActive(true);
        buttonCol[3].SetActive(true);
        ResManager.Step = 9;
        HideArrow_ChuanDong();
        HideArrow_ShouDong();

    }
    public void EnterStepFive() {

        HideMenu();
        buttonCol[1].SetActive(true);
        buttonCol[2].SetActive(true);
        buttonCol[3].SetActive(true);
        ResManager.Step = 5;
        HideArr_JingLv();
        ShowArr_JingLv01();
     //   ShowNiuJu();

    }
    //点击下一步
    public void clickBtn_Next() {

        playSound();
        switch (ResManager.Step) {

            case 1:
                HideArr_JingLv();
                ResManager.Step = 2;
                EnterStepTwo();
                break;
            case 2:
                ResManager.Step = 3;
                EnterStepThree();
                ShowArr_JingLv();
             //   ShowNiuJu();
              
                break;
            case 3:
                HideArr_JingLv();
                ResManager.Step = 4;
                EnterStepFour();
                break;
            case 4:
                ResManager.Step = 5;
                EnterStepFive();
                ShowArr_JingLv01();
              //  ShowNiuJu();
                break;
            case 6:
                HidePipeCol();
                ResManager.Step = 7;
                EnterStepTwo_2();
                ShowArrow_ShouDong();
                break;
            case 7:
                HidePipeCol();
                ResManager.Step = 8;
                EnterStepThree_3();
                PlayPipeAnimation();
                break;

            case 8:
                ResManager.Step = 9;
                EnterStepFour_4();
                break;
            default:
                HideArr_JingLv();
             //   HideNiuJu();
                break;

        }

    }

    //显示精滤拆装箭头提示操作
    public void ShowArr_JingLv() {

        arr_JingLv.SetActive(true);

    }
    //隐藏精滤拆装箭头操作
    public void HideArr_JingLv() {

        arr_JingLv.SetActive(false);

    }

    public void HideArr_JingLv01() {

        arr_JingLv01.SetActive(false);
    }

    public void ShowArr_JingLv01() {

        arr_JingLv01.SetActive(true);

    }
    //下一步按钮点击执行的方法
    public void clickBtn_Skip() {

        playSound();
        switch (ResManager.Step) {

            case 1:
                ShowSkipObj();
                break;
            case 2:
                ShowSkipObj();
                break;
            case 3:
                ShowSkipObj();
                break;
            case 4:
                ShowSkipObj();
                break;
            case 5:
                ShowSkipObj();
                break;
            case 6:
                // 
                ShowSkipObj_01();
                break;
            case 7:
                ShowSkipObj_01();
                break;
            case 8:
                ShowSkipObj_01();
                break;
            case 9:
                ShowSkipObj_01();
                break;
            default:
                break;

        }
    }

    public void clickBtn_Front() {

        playSound();
        switch (ResManager.Step) {

            case 2:
                ResManager.Step = 1;
                EnterStepOne();
                break;
            case 3:
                ResManager.Step = 2;
                EnterStepTwo();
                break;
            case 4:
                ResManager.Step = 3;
                EnterStepThree();
                break;
            case 5:
                ResManager.Step = 4;
                EnterStepFour();
                break;
            case 7:
                ResManager.Step = 6;
                EnterStepOne_1();
                break;
            case 8:
                ResManager.Step = 7;
                EnterStepTwo_2();
                break;
            case 9:
                ResManager.Step = 8;
                EnterStepThree_3();
                break;
            default:
                break;
        }
    }

    public void clickBtn_Com() {

        playSound();
        switch (ResManager.Step) {

            case 5:
                HideArr_JingLv();
                break;
            case 9:
                HideArrow_ChuanDong();
                HideArrow_ShouDong();
                //隐藏管道模型
                break;
            default:
                break;

        }

    }



    public void ShowSkipObj() {

        playSound();
        SkipColl.SetActive(true);

    }
    //油口部分跳选对象显示
    public void ShowSkipObj_01() {

        playSound();
        SkipColl_01.SetActive(true);
    }
    //跳到步骤一
    public void stepToFirst() {
        playSound();
        ResManager.Step = 1;
       
        SkipColl.SetActive(false);
        EnterStepOne();
    }

    public void stepToFirst_1() {

        playSound();
        ResManager.Step = 6;

        SkipColl_01.SetActive(false);
        EnterStepOne_1();
    }


    public void stepToSecond_2() {

        playSound();
        ResManager.Step = 7;
        SkipColl_01.SetActive(false);
        EnterStepTwo_2();
    }

    public void stepToThird_3() {

        playSound();
        ResManager.Step = 8;
        SkipColl_01.SetActive(false);
        EnterStepThree_3();
        ShowPipeF5_1();
    }

    public void stepToFourth_4() {

        playSound();
        ResManager.Step = 9;
        SkipColl_01.SetActive(false);
        EnterStepFour_4();

    }
    //跳到步骤二
    public void stepToSecond() {
        playSound();
        ResManager.Step = 2;
        SkipColl.SetActive(false);

        EnterStepTwo();
    }

    //跳到步骤三
    public void stepToThird() {

        playSound();
        ResManager.Step = 3;
        SkipColl.SetActive(false);
        EnterStepThree();

    }
    //跳到步骤四
    public void stepToFourth() {

        playSound();
        ResManager.Step = 4;
        SkipColl.SetActive(false);
        EnterStepFour();
    }
    //跳到步骤五
    public void stepToFifth() {
        playSound();
        ResManager.Step = 5;
        SkipColl.SetActive(false);
        EnterStepFive();
    }

    //显示精滤模型
    public void ShowJingLvModel() {

        playSound();
        JingLvTiShi.SetActive(true);
        JingLvModel.SetActive(true);
    }
    //播放拆动画
    public void PlayChaiAni() {
        playSound();
        JingLvModel01.SetActive(false);
        JingLvTiShi.SetActive(true);
        JingLvModel.SetActive(true);
        JingLvModel.transform.parent.GetComponent<Animation>().Play("Take 001");
    }
    //播放装动画
    public void PlayZhuangAni() {

        playSound();
        JingLvTiShi.SetActive(true);
        JingLvModel.SetActive(false);
        JingLvModel01.SetActive(true);
        JingLvModel01.GetComponent<Animation>().Play("Take 001");

        
    }

}
