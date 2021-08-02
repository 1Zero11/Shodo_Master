using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*
 *  foreach (myOwnPath p in paths)
        {
            List<Vector2> roadToPath = new List<Vector2>();
            roadToPath.Add(p.position);
            foreach (CCommand cc in p.curves)
            {
                roadToPath.Add(p.position + cc.startTangent);
                roadToPath.Add(p.position + cc.endPoint);
                roadToPath.Add(p.position + cc.endPoint);
            }
            GameObject stroke = Instantiate(PC);
            stroke.GetComponent<PathCreator>().path = new Path(roadToPath);
        }
 */
[System.Serializable]
public class Objective
{
    public int[] kanji;
    public bool isOn = false;
    //public int[] kanji;
    public int[] side;
    public int mastery = 0;
    public string introduction = "";
    public string shortIntroduction = "";
}



public class Enemy
{
    public int hp;
    public string name;
    public int[] items;
    public float[] percenteges;
}


public class MainFightingScript : MonoBehaviour
{

    public static MainFightingScript MFS;

    public Slider enemyHealthSlider;
    //private Sprite nextImage;

    //public int hp = 10;
    public int numberOfKanji = 0;
    public int times = 0;
    //public string[] studyKanji;
    //public string[] names;
    public Kanji kanjiAsPaths;
    public string currentKanjiString;
    public Color[] colors;
    public bool ableToDraw = false;


    public Text kanjiText;
    public Text meaningText;
    public Text readingsText;

    public List<Objective> objectives = new List<Objective>();
    public List<Enemy> enemies = new List<Enemy>();
    public Enemy currentEnemy;
    public int numberOfEnemy = 0;
    //public Objective currentObjective { get { return GameControl.control.currentObjectives[GameControl.control.currentObjectiveNumber]; } }

    public GameObject inventory;
    public Text masteryLevelText;
    public GameObject scroll;
    //private List<int> fallen;
    private bool isFailed = false;
    //private bool secondCircle = false;

    public SwipeTrail swipeTrail;
    public NewKanjiLearned newKanjiLearnedScript;
    public ShopScrollList shopScrollList;
    public GameObject objectiveCompleteScreen;
    public NavigationMainScript navigationMainScript;
    public ChooseColor chooseColorScript;
    public RadicalButtons radicalButtonsScript;
    public TileManager tileManagerScript;

    public string[] radicals;
    public bool isSeingRadical = false;


    //PRIVATE
    public string oldKanji;



    public void Awake()
    {
        MFS = this;

    }


    // Use this for initialization
    void Start()
    {


        StartBattle();
        //kanjiText.text = "";
        //meaningText.text = "Choose objective";
        //masteryLevelText.text = "Mastery Level: " + GameControl.control.masteryLevel.ToString();


    }

    private void StartBattle()
    {
        LoadNewEnemy(enemies[Random.Range(0, enemies.Count)]);
        LoadNewKanji(GenerateNextKanji());
        ableToDraw = true;
    }


    public void Escape()
    {
        if (isSeingRadical)
        {
            isFailed = false;
            ableToDraw = true;
            isSeingRadical = false;
            LoadNewKanji(oldKanji);
        }
    }


    private void LoadNewKanji(string kanji)
    {
        //kanji = "氏";
        tileManagerScript.DeleteAllTiles();
        currentKanjiString = kanji;//GameControl.control.AllKanji[currentObjective.kanji[number]];
        kanjiAsPaths = new Kanji(GameControl.control.TestWriteKanji(currentKanjiString));
        colors = GameControl.control.ColorDetermine(currentKanjiString);
        kanjiText.text = "";

        string[] result = GameControl.control.FindMeaning(currentKanjiString);
        meaningText.text = result[0];
        readingsText.text = result[1] + "\n" + result[2];

        swipeTrail.failedLines = 0;
        gameObject.GetComponent<Drawer>().DeleteAllLines();

        ColoredKanji CK = GameControl.control.colors.Find(x => x.kanji.Equals(currentKanjiString));
        Color c;
        if (CK != null)
        {
            c = CK.color;
        }
        else
            c = Color.black;

        chooseColorScript.ChangeButtonColor(c);

        radicals = GameControl.control.FindRadical(currentKanjiString);
    }

    public void ShowRadicals()
    {
        if (!ableToDraw)
        {
            tileManagerScript.Show(radicals);
            if (!isSeingRadical)
                oldKanji = currentKanjiString;

        }
    }

    public void RadicalPush(int number)
    {
        isSeingRadical = true;
        Debug.Log("Loading " + radicals[number]);
        LoadNewKanji(radicals[number]);
        kanjiText.text = currentKanjiString;
        ableToDraw = true;
    }



    private int[] KanjiToInt(string[] kanji)
    {
        int[] output = new int[kanji.Length];
        for (int b = 0; b < kanji.Length; b++)
        {
            string s = kanji[b];
            for (int i = 0; i < GameControl.control.AllKanji.Length; i++)
            {
                if (GameControl.control.AllKanji[i] == s)
                {
                    output[b] = i;
                    break;
                }

            }
        }
        return output;
    }

    public void Initialize()
    {
        //0
        objectives.Add(new Objective
        {
            introduction = "This scroll contains all kanji for JLPT 5",
            shortIntroduction = "JLPT 5",
            kanji = KanjiToInt(new string[] {
                "一","七","万","三","上","下","中","九","二","五","人","今","休","会","何","先","入","八","六","円","出",
                "分","前","北","十","千","午","半","南","友","口","古","右","名","四","国","土","外","多","大","天","女",
                "子","学","安","小","少","山","川","左","年","店","後","手","新","日","時","書","月","木","本","来","東",
                "校","母","毎","気","水","火","父","生","男","白","百","目","社","空","立","耳","聞","花","行","西","見",
                "言","話","語","読","買","足","車","週","道","金","長","間","雨","電","食","飲","駅","高","魚"}),
            side = new int[] { 0 }
        });
        //1
        objectives.Add(new Objective
        {
            introduction =
           "This scroll contains all kanji for JLPT 4",

            shortIntroduction =
           "Nice N4 set",
            kanji = KanjiToInt(new string[] {
                "不", "世", "主", "乗", "事", "京", "仕", "代", "以", "低", "住", "体", "作", "使", "便", "借", "働", "元",
                "兄", "光", "写", "冬", "切", "別", "力", "勉", "動", "区", "医", "去", "台", "合", "同", "味", "品", "員",
                "問", "回", "図", "地", "堂", "場", "声", "売", "夏", "夕", "夜", "太", "好", "妹", "姉", "始", "字", "室",
                "家", "寒", "屋", "工", "市", "帰", "広", "度", "建", "引", "弟", "弱", "強", "待", "心", "思", "急", "悪",
                "意", "所", "持", "教", "文", "料", "方", "旅", "族", "早", "明", "映", "春", "昼", "暑", "暗", "曜", "有",
                "服", "朝", "村", "林", "森", "業", "楽", "歌", "止", "正", "歩", "死", "民", "池", "注", "洋", "洗", "海",
                "漢", "牛", "物", "特", "犬", "理", "産", "用", "田", "町", "画", "界", "病", "発", "県", "真", "着", "知",
                "短", "研", "私", "秋", "究", "答", "紙", "終", "習", "考", "者", "肉", "自", "色", "英", "茶", "菜", "薬",
                "親", "計", "試", "説", "貸", "質", "赤", "走", "起", "転", "軽", "近", "送", "通", "進", "運", "遠", "都",
                "重", "野", "銀", "門", "開", "院", "集", "青", "音", "頭", "題", "顔", "風", "飯", "館", "首", "験", "鳥",
                "黒" }),
            side = new int[] { 2 }
        });
        //2
        objectives.Add(new Objective
        {
            introduction =
           "This scroll contains all kanji for JLPT 3",

            shortIntroduction =
           "Nice N3 set",
            kanji = KanjiToInt(new string[] {
                "丁","両","丸","予","争","交","他","付","令","仲","伝","位","例","係","信","倉","倍","候","停","健","側","億",
                "兆","児","全","公","共","兵","具","典","内","冷","刀","列","初","利","刷","副","功","加","助","努","労","勇",
                "勝","包","化","卒","協","単","博","印","原","参","反","取","受","史","号","司","各","向","君","告","周","命",
                "和","唱","商","喜","器","囲","固","園","坂","型","塩","士","変","夫","央","失","委","季","孫","守","完","官",
                "定","実","客","宮","害","宿","察","寺","対","局","岩","岸","島","州","巣","差","希","席","帯","帳","平","幸",
                "底","府","庫","庭","康","式","弓","当","形","役","径","徒","得","必","念","息","悲","想","愛","感","成","戦",
                "戸","才","打","投","折","拾","指","挙","改","放","救","敗","散","数","整","旗","昔","星","昨","昭","景","晴",
                "曲","最","望","期","未","末","札","材","束","松","板","果","柱","栄","根","案","梅","械","植","極","様","標",
                "横","橋","機","欠","次","歯","歴","残","殺","毒","毛","氏","氷","求","決","汽","油","治","法","波","泣","泳",
                "活","流","浅","浴","消","深","清","温","港","湖","湯","満","漁","灯","炭","点","無","然","焼","照","熱","牧",
                "玉","王","球","由","申","畑","番","登","的","皮","皿","直","相","省","矢","石","礼","祝","神","票","祭","福",
                "科","秒","種","積","章","童","競","竹","笑","笛","第","筆","等","算","管","箱","節","米","粉","糸","紀","約",
                "級","細","組","結","給","絵","続","緑","線","練","置","羊","美","羽","老","育","胃","脈","腸","臣","航","船",
                "良","芸","芽","苦","草","荷","落","葉","虫","血","街","衣","表","要","覚","観","角","訓","記","詩","課","調",
                "談","議","谷","豆","象","貝","負","貨","貯","費","賞","路","身","軍","輪","辞","農","辺","返","追","速","連",
                "遊","達","選","郡","部","配","酒","里","量","鉄","録","鏡","関","陸","陽","隊","階","雪","雲","静","面","順",
                "願","類","飛","養","馬","鳴","麦","黄","鼻" }),
            side = new int[] { 2 }
        });
        //3
        objectives.Add(new Objective
        {
            shortIntroduction =
             "It is the time for war!",

            introduction =
             "Flags are raised, as troops from each side come to meet each other",

            kanji = KanjiToInt(new string[] {
                "武","戦","闘","敵","攻","伐","殺","撃","討","殴",
                "撃","射","襲","征","兵","刀","矛","剣",
                "盾","甲","侍","忍","偵","弓","矢","銃","砲","将",
                "曹","尉","軍","隊","班","陣","屯","爆","弾","艦" }),
            side = new int[] { 2 }
        });
        //4
        objectives.Add(new Objective
        {
            shortIntroduction =
            "Numbers",

            introduction =
            "Well, this is a lot of numbers",

            kanji = KanjiToInt(new string[] {
                "零","一","二","三","四","五","六","七","八","九","十","百","千","万","億" }),
            side = new int[] { 2 }
        });
        //5
        objectives.Add(new Objective
        {
            shortIntroduction =
            "Counters",

            introduction =
            "Finally learn these counters!!!",

            kanji = KanjiToInt(new string[] {
                "個","台","枚","本","隻","冊","匹","疋","頭","羽","箇","足","札","杯","軒","戸","輪","畳","斤","丁","人" }),
            side = new int[] { 2 }
        });
        //6
        objectives.Add(new Objective
        {
            shortIntroduction =
            "Seasons",

            introduction =
            "Seasons",

            kanji = KanjiToInt(new string[] {
                "季","候","節","春","夏","秋","冬" }),
            side = new int[] { 2 }
        });
        //7
        objectives.Add(new Objective
        {
            shortIntroduction =
            "Animals",

            introduction =
            "Dogs, cats, livestock - that kind of stuff",

            kanji = KanjiToInt(new string[] {
                "犬","猫","馬","牛","羊",
                "豚","獣","畜","象",
                "猿","鯨","虫","昆","蚊","蚕",
                "蛍","蛇","竜" }),
            side = new int[] { 2 }
        });
        //8
        objectives.Add(new Objective
        {
            shortIntroduction =
            "Counters",

            introduction =
            "Finally learn these counters!!!",

            kanji = KanjiToInt(new string[] {
                "個","台","枚","本","隻","冊","匹","疋","頭","羽","箇","足","札","杯","軒","戸","輪","畳","斤","丁","人" }),
            side = new int[] { 2 }
        });






        enemies.Add(new Enemy { hp = 10, name = "Michael Jackson" ,items = new int[] {1},percenteges = new float[] { 0.5f} });
        enemies.Add(new Enemy { hp = 5, name = "Overpriced boots", items = new int[] {1,3}, percenteges = new float[] { 0.5f,1f } });
        enemies.Add(new Enemy { hp = 15, name = "Vengeful spirit",items = new int[] {1},percenteges = new float[] { 0.5f} } );
        enemies.Add(new Enemy { hp = 20, name = "Iron Turtle" ,items = new int[] {1},percenteges = new float[] { 0.5f} });
        enemies.Add(new Enemy { hp = 13, name = "Pontiff Sulyvahn" ,items = new int[] {1},percenteges = new float[] { 0.5f} });
        enemies.Add(new Enemy { hp = 30, name = "Susano", items = new int[] { 1,2,3,4,5 }, percenteges = new float[] { 1f,1f,1f,1f,1f } });
        enemies.Add(new Enemy { hp = 10, name = "Kitsune", items = new int[] { 6,7 }, percenteges = new float[] { 1f, 1f } });


    }






    public void ChangeProbability(int kanji, bool succed)
    {

        if (succed)
        {
            GameControl.control.probabilities[kanji] /= 2;
            if (GameControl.control.probabilities[kanji] < 1)
                GameControl.control.probabilities[kanji] = 1;
        }
        else
            GameControl.control.probabilities[kanji] = 32;


    }




    public void Failed(int nol)
    {

        kanjiText.text = currentKanjiString;
        if (nol < kanjiAsPaths.paths.Length - 1 && times == 0)
            gameObject.GetComponent<Drawer>().DrawPath(kanjiAsPaths.paths[nol + 1], new Color(0.5250534f, 0.5930807f, 0.6509434f), true);


        isFailed = true;
    }


    public void EndOfLine(int nol, Color color)
    {
        if (isFailed && nol < kanjiAsPaths.paths.Length - 1 && times == 0)
            gameObject.GetComponent<Drawer>().DrawPath(kanjiAsPaths.paths[nol + 1], new Color(0.5250534f, 0.5930807f, 0.6509434f), true);

        //if (times!=1)
        gameObject.GetComponent<Drawer>().DrawPath(kanjiAsPaths.paths[nol], color);
        if (nol >= kanjiAsPaths.paths.Length - 1)
        {
            ableToDraw = false;
            //Invoke("KanjiComplete",1f);
        }
    }


    private void LoadNewEnemy(Enemy enemy)
    {
        currentEnemy = new Enemy { name = enemy.name, hp = enemy.hp, items = enemy.items, percenteges = enemy.percenteges };
        float sum = 0f;
        foreach(float n in currentEnemy.percenteges)
        {
            sum += n;
        }
        Color color = Color.white;
        if (sum >= 1f)
            color = Color.magenta;
        masteryLevelText.color = color;
        masteryLevelText.text = enemy.name;
        enemyHealthSlider.maxValue = enemy.hp;
        enemyHealthSlider.value = enemy.hp;
    }

    public void Cheat()
    {
        times = 0;
        KanjiComplete();
    }

    public void KanjiComplete()
    {

        ableToDraw = true;
        times++;
        gameObject.GetComponent<Drawer>().DeleteAllLines();
        if (times == 1 || (times == 0 && isFailed != true))
        {
            //swipeTrail.free = true;
        }
        if (times == 2 || (times == 1 && isFailed != true))
        {
            ChangeProbability(numberOfKanji, !isFailed);
            swipeTrail.free = false;
            times = 0;
            isFailed = false;
            StartCoroutine(SlowHpDecrease(kanjiAsPaths.paths.Length));
            LoadNewKanji(GenerateNextKanji());

            
            //enemyHealthSlider.value = currentEnemy.hp;









            //LoadNewKanji(GameControl.control.AllKanji[currentObjective.kanji[numberOfKanji]]);


            //MASTERY
            GameControl.control.masteryLevel++;
            //masteryLevelText.text = "Mastery Level: " + GameControl.control.masteryLevel.ToString();

            Debug.Log("Number of word is " + numberOfKanji);

        }
    }

    public void ChangeObjective(int newObjectiveNumber, bool value)
    {
        //objectiveCompleteScreen.SetActive(true);
        //objectiveCompleteScreen.GetComponentInChildren<Text>().text = GameControl.control.currentObjectives[newObjectiveNumber].introduction;
        if (GameControl.control.currentObjectives[newObjectiveNumber].mastery <= GameControl.control.masteryLevel)
        {
            
            GameControl.control.currentObjectives[newObjectiveNumber].isOn = value;
            Debug.Log("Changed " + newObjectiveNumber + " to " + GameControl.control.currentObjectives[newObjectiveNumber].isOn);
            //GameControl.control.currentObjectiveNumber = newObjectiveNumber;
            //LoadNewKanji(GenerateNextKanji());

            //numberOfEnemy = 0;
            //int test = currentObjective.enemies[numberOfEnemy];
            //LoadNewEnemy(enemies[test]);
            //navigationMainScript.ButtonPressed(1);
            //ableToDraw = true;
        }
    }

    private string GenerateNextKanji()
    {
        string output = "";

        List<int> ar = new List<int>();
        foreach (Objective objective in GameControl.control.currentObjectives)
        {
            if (objective.isOn)
                ar.AddRange(objective.kanji);
        }


        int sum = 0;
        List<int> carvedProbabilities = new List<int>();
        foreach (int i in ar)
        {
            if (GameControl.control.probabilities[i] != 0)
            {
                carvedProbabilities.Add(GameControl.control.probabilities[i]);
                sum += GameControl.control.probabilities[i];
            }
            else
            {
                carvedProbabilities.Add(2);
                sum += 2;
            }

        }


        int random = Random.Range(0, sum);

        for (int i = 0; i < carvedProbabilities.Count; i++)
        {
            random -= carvedProbabilities[i];
            if (random <= 0&& numberOfKanji!= ar[i])
            {
                numberOfKanji = ar[i];
                output = GameControl.control.AllKanji[ar[i]];
                break;
            }
        }

        return output;
    }

    /*
    private Objective[] NextObjective(Objective currentObj)
    {
        List<int> least = new List<int>();


        for (int i = 0; i < 10; i++)
        {
            least.Add(Random.Range(0, enemies.Count));
        }


        List<Objective> obj = new List<Objective>();

        if (currentObj.side.Length > 0)
        {
            for (int i = 0; i < currentObj.side.Length; i++)
            {
                obj.Add(objectives[currentObj.side[i]]);
            }
        }

        if (currentObj.shortIntroduction == "Contract")
        {
            Objective o = new Objective();
            o.shortIntroduction = "Contract";
            o.introduction = "You've got a new contract";
            //o.ending = "Contract completed";
            //o.enemies = least.ToArray();
            o.side = new int[0];
            obj.Add(o);
        }

        return obj.ToArray();
        
    }
    */
    /*
    private void CompleteObjective()
    {

        kanjiText.text = "";
        meaningText.text = "Choose objective";
        GameControl.control.currentObjectives.AddRange(NextObjective(currentObjective));
        GameControl.control.currentObjectives.RemoveAt(GameControl.control.currentObjectiveNumber);
        GameControl.control.currentObjectiveNumber = 0;
        numberOfKanji = 0;
        ableToDraw = false;
    }
    */
    public void CloseObjectiveScreen()
    {
        objectiveCompleteScreen.SetActive(false);
    }

    public void Show()
    {
        inventory.SetActive(!inventory.activeSelf);
    }

    private IEnumerator SlowHpDecrease(int damage)
    {
        float time = 0f;
        while (time < 0.5f)
        {
            enemyHealthSlider.value = currentEnemy.hp - damage * time*2;
            time += Time.deltaTime;
            if (enemyHealthSlider.value <= 0)
                break;
            yield return null;
        }
        Debug.Log("Damage " + currentEnemy.hp + " - " + damage);
        currentEnemy.hp -= damage;
        enemyHealthSlider.value = currentEnemy.hp;



        if (currentEnemy.hp <= 0)
        {
            for (int i = 0; i < currentEnemy.items.Length; i++)
            {
                if(Random.Range(0f,1f) < currentEnemy.percenteges[i])
                {
                    bool t = true;
                    foreach(Objective objective in GameControl.control.currentObjectives)
                    {
                        if (objective.shortIntroduction == objectives[currentEnemy.items[i]].shortIntroduction)
                            t = false;
                    }
                    if(t)
                        GameControl.control.currentObjectives.Add(objectives[currentEnemy.items[i]]);
                }
            }
            numberOfEnemy++;
            LoadNewEnemy(enemies[Random.Range(0, enemies.Count)]);
            /*
            if (numberOfEnemy >= currentObjective.enemies.Length)
            {
                Debug.Log("Kanji Ended");
                CompleteObjective();

            }
            else
                LoadNewEnemy(enemies[currentObjective.enemies[numberOfEnemy]]);
            */
        }
    }

}
