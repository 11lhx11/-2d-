using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    private int gold = 500;
    private int exp = 0;
    //右下角按钮设置
    public GameObject ReturnButton;
    public GameObject ConfigButton;
    public GameObject Voice_Panel;

    //右上角按钮设置
    public GameObject BigCountDown;
    public GameObject Reward;
    private float BigCountTime = 240f;
    private float BigCountTimer;
    private int bigReward = 500;
    private bool HasGetGold;
    //左上角设置
    public Text level_text;
    int level_value;
    public Text level_name_text;
    private string[] name_num = { "无", "黑铁", "青铜", "白银", "黄金", "铂金", "钻石", "大师", "宗师", "王者" };
    public Slider Exp;

    //左下角设置
    public Text gold_text;
    private Color originalColor;
    public Text smallCountDown_text;
    private float smallCountTime = 5f;
    public float smallCountTimer;
    private int smallReward = 50;
    //特效处理
    public GameObject MoneyEffectPrefab;
    public GameObject LevelUpEffectPrefab;
    public GameObject LevelText;
    //背景设置
    public GameObject bg;
    public Sprite[] bg_num;
    private int bg_index = 0;
    public GameObject wavePrefab;//过场浪花
    public static UIManager Instance { get => instance; set => instance = value; }
    void Awake()
    {
        Instance = this;
        originalColor = gold_text.color;
        ReadStorageDate();
        BigCountDown.GetComponent<Text>().text = ((int)BigCountTimer).ToString() + "s";
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateLeftDown();
        UpdateRightUp();
        UpdateLeftUp();
    }
#region 左上角的更新
    /// <summary>
    /// 更新左上角
    /// 负责等级、经验条、称号的更新显示
    /// 等级无上限，当经验大于升级所需经验，则进行扣去
    /// 称号每10级换一次，100级之后不变
    /// </summary>
    void UpdateLeftUp()
    {
        level_value = int.Parse(level_text.text);//当前等级
        int level_exp = 1000 + 200 * level_value;//当前等级升级所需经验
        while (exp >= level_exp)
        {
            level_value++;
            exp -= level_exp;
            if (level_value % 20 == 0) ChangeBackground();//每过20级换背景
            ShowLevel_Up();
            level_exp = 1000 + 200 * level_value;//升级之后再升级所需的总经验
        }
        Exp.value = (float)exp / level_exp; //设置经验条显示
        level_text.text = level_value.ToString();//设置等级显示
        int name_index = level_value / 10; name_index = (name_index > 9) ? 9 : name_index;
        level_name_text.text = name_num[name_index];//设置称号
    }
    void ChangeBackground()
    {
        bg_index++;
        bg_index = (bg_index >= bg_num.Length) ? bg_num.Length - 1 : bg_index;
        bg.GetComponent<Image>().sprite = bg_num[bg_index];
        Instantiate(wavePrefab);
    }
    void ShowLevel_Up()
    {
        LevelText.SetActive(true);
        LevelText.transform.Find("Text").GetComponent<Text>().text = (int.Parse(level_text.text) + 1).ToString();
        Instantiate(LevelUpEffectPrefab);
        AudioManager.Instance.PlayAudioClip(AudioManager.Instance.LevelupClip);
        Invoke("HideLevel_Up", 3f);
    }
    void HideLevel_Up()
    {
        LevelText.SetActive(false);
    }
#endregion
    #region 左下角的更新
    void UpdateLeftDown()
    {   
        //每隔几秒，游戏系统会赠予玩家一小笔金钱。
        smallCountTimer -= Time.deltaTime;
        if (smallCountTimer <= 0)
        {
            AddGold(smallReward);
            Instantiate(MoneyEffectPrefab);
            AudioManager.Instance.PlayAudioClip(AudioManager.Instance.RewardClip);
            smallCountTimer = smallCountTime;
        }
        smallCountDown_text.text = "0 " + (int)smallCountTimer;
        gold_text.text = "$" + gold;
    }
    #endregion

    #region
    void UpdateRightUp()
    {
        if (BigCountDown.activeSelf == true)
        {
            BigCountTimer -= Time.deltaTime;
            if (BigCountTimer <= 0 && Reward.activeSelf == false)//到时间,显示奖励
            {
                Reward.SetActive(true);
                BigCountDown.SetActive(false);
            }

            BigCountDown.GetComponent<Text>().text = ((int)BigCountTimer).ToString() + "s";
        }
    }
    public void GetCountdownReward()//得到倒计时的金钱
    {
        Reward.SetActive(false);
        BigCountDown.SetActive(true);
        BigCountTimer = BigCountTime;
        AddGold(bigReward);
        Instantiate(MoneyEffectPrefab);
        AudioManager.Instance.PlayAudioClip(AudioManager.Instance.RewardClip);
        BigCountDown.GetComponent<Text>().text = ((int)BigCountTimer).ToString() + "s";
    }
    #endregion

    public void AddGold(int money)
    {
        gold += money;
    }
    public void AddExp(int e)
    {
        exp += e;
    }
    public bool ReduceGold(int money)
    {
        //如果金币不足，则进行变红闪烁效果的显示
        if (money > gold)
        {
            if (gold_text.color != Color.red)
            {
                gold_text.color = Color.red;
                Invoke("ChangeTextColor", 1f);
            }
            return false;
        }
        gold -= money;
        return true;
    }
    private void ChangeTextColor()
    {
        gold_text.color = originalColor;
    }

    /// <summary>
    /// 右下角按钮的相关设置
    /// </summary>
    public void OnConfigButton()
    {
        Voice_Panel.SetActive(true);
    }
    public void OnCloseButton()
    {
        Voice_Panel.SetActive(false);
    }
    public void OnReturnButton()//返回主界面，并保存游戏
    {
        PlayerPrefs.SetInt("Gold",gold);
        PlayerPrefs.SetInt("Exp",exp);
        PlayerPrefs.SetInt("Level",int.Parse(level_text.text));
        PlayerPrefs.SetFloat("BigCountDown",BigCountTimer);
        PlayerPrefs.SetFloat("SmallCountDown", smallCountTimer);
        AudioManager.Instance.SaveMuteState();
        SceneManager.LoadScene(0);
    }
    private void ReadStorageDate()//读取存档的数据
    {
        gold=PlayerPrefs.GetInt("Gold", gold);
        exp=PlayerPrefs.GetInt("Exp", exp);
        level_value=PlayerPrefs.GetInt("Level", 0);
        level_text.text = level_value.ToString();
        BigCountTimer=PlayerPrefs.GetFloat("BigCountDown", BigCountTime);
        smallCountTimer=PlayerPrefs.GetFloat("SmallCountDown", smallCountTime);
       
    }
}
