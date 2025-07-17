using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Firebase;
// using Firebase.Analytics;
// using Firebase.Extensions;
using System;
using System.Threading.Tasks;
using UnityEngine.Events;

// Firebaseマネージャー。イベント発火の関数を定義する。
// FirebaseSDKを入れた後にそれぞれ有効化する
public class FirebaseManager : MonoBehaviour
{
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------
    // ---------- プレハブ ----------
    // ---------- プロパティ ----------
    public int isInit;
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    public static FirebaseManager instance = null;
    private bool _isFirebaseInitialized = false;

    private event UnityAction onInit; 
    // ---------- Unity組込関数 ----------
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else    
            Destroy(this);
    }
    private async void Start()
    {
        await InitializeFirebase();
    }

    private async Task InitializeFirebase()
    {
        // var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
        // if (dependencyStatus == DependencyStatus.Available)
        // {
        //     FirebaseApp app = FirebaseApp.DefaultInstance;
        //     _isFirebaseInitialized = true;
        //     Debug.Log("Firebase Initialized Successfully!");
        //     onInit?.Invoke();
        // }
        // else
        // {
        //     Debug.LogError($"Firebase initialization failed: {dependencyStatus}");
        // }
    }
    // ---------- Public関数 ----------
    
    /// <summary>
    /// 飛んだ後のイベント
    /// </summary>
    /// <param name="Stage">ステージ数	INT型</param>
    /// <param name="Times">合計挑戦回数	INT型</param>
    /// <param name="Record">記録	INT型</param>
    /// <param name="Progress">進捗率	INT型</param>
    /// <param name="SlingLevel">補強レベル	INT型</param>
    /// <param name="PlaneLevel">補強レベル	INT型</param>
    /// <param name="IncomeLevel">補強レベル	INT型</param>
    /// <param name="Double">広告でDouble収入したか	Bool型</param>
    /// <param name="Coin">コインの持ち数	INT型</param>
    /// <param name="EarnCoins">獲得したコイン	INT型</param>
    /// <param name="Angle">飛ばす時の角度	float型</param>
    /// <param name="Pull">飛ばす時の引く強さ	float型</param>
    public void EventFly_Action(int stage, int times, int record, int progress, int slingLevel, int planeLevel, int incomeLevel, bool isDouble, int coin, int earnCoins, float angle, float pull)
    {
        TryLogEvent(()=>{
            // FirebaseAnalytics.LogEvent("Fly_Action", 
            //                 new Parameter("Stage", stage),
            //                 new Parameter("Times", times),
            //                 new Parameter("Record", record),
            //                 new Parameter("Progress", progress),
            //                 new Parameter("SlingLevel", slingLevel),
            //                 new Parameter("PlaneLevel", planeLevel),
            //                 new Parameter("IncomeLevel", incomeLevel),
            //                 // new Parameter("Double", isDouble.ToString()),
            //                 new Parameter("Coin", coin),
            //                 new Parameter("EarnCoins", earnCoins));
            //                 // new Parameter("Angle", angle),
            //                 // new Parameter("Pull", pull));
            // Debug.Log("Fly_Action:" + stage + ", " + times + ", " + record + ", " + progress + ", " + slingLevel + ", " + planeLevel + ", " + incomeLevel + ", " + isDouble + ", " + coin + ", " + earnCoins + ", " + angle + ", " + pull);
        });
    }

    /// <summary>
    /// ステージスタート
    /// </summary>
    /// <param name="Stage">ステージ数	INT型</param>
    /// <param name="SlingLevel">補強レベル	INT型</param>
    /// <param name="PlaneLevel">補強レベル	INT型</param>
    /// <param name="IncomeLevel">補強レベル	INT型</param>
    public void EventStageStart(int stage, int slingLevel, int planeLevel, int incomeLevel)
    {
        TryLogEvent(()=>{
            // FirebaseAnalytics.LogEvent("Stage_Start", 
            //                 new Parameter("Stage", stage),
            //                 new Parameter("SlingLevel", slingLevel),
            //                 new Parameter("PlaneLevel", planeLevel),
            //                 new Parameter("IncomeLevel", incomeLevel));
        });
        Debug.Log("Stage_Start:" + stage + ", " + slingLevel + ", " + planeLevel + ", " + incomeLevel);
    }

    /// <summary>
    /// ステージクリア
    /// </summary>
    /// <param name="Stage">ステージ数	INT型</param>
    /// <param name="Times">合計挑戦回数	INT型	ステージが変わると0に戻る</param>
    /// <param name="SlingLevel">補強レベル	INT型</param>
    /// <param name="PlaneLevel">補強レベル	INT型</param>
    /// <param name="IncomeLevel">補強レベル	INT型</param>
    public void EventStageClear(int stage, int times, int slingLevel, int planeLevel, int incomeLevel)
    {
        TryLogEvent(()=>{
            // FirebaseAnalytics.LogEvent("Stage_Clear", 
            //                 new Parameter("Stage", stage),
            //                 new Parameter("Times", times),
            //                 new Parameter("SlingLevel", slingLevel),
            //                 new Parameter("PlaneLevel", planeLevel),
            //                 new Parameter("IncomeLevel", incomeLevel));
        });
        Debug.Log("Stage_Clear:" + stage + ", " + times + ", " + slingLevel + ", " + planeLevel + ", " + incomeLevel);
    }

    /// <summary>
    /// レベルアップ
    /// </summary>
    /// <param name="Stage">ステージ数	INT型</param>
    /// <param name="Times">合計挑戦回数	INT型	ステージが変わると0に戻る</param>
    /// <param name="Type">SlingLevel,PlaneLevel,IncomeLevel	String型</param>
    /// <param name="UpgradeLevel">補強レベル	INT型	補強後のレベル</param>
    /// <param name="Coin">コインの持ち数	INT型	コインを払うまえの持ち数</param>
    /// <param name="Cost">かかった費用	INT型</param>
    /// <param name="SlingLevel">補強レベル	INT型</param>
    /// <param name="PlaneLevel">補強レベル	INT型</param>
    /// <param name="IncomeLevel">補強レベル	INT型</param>
    public void EventGrowth_Action(int stage, int times, string type, int upgradeLevel, int coin, int cost, int slingLevel, int planeLevel, int incomeLevel)
    {
        TryLogEvent(()=>{
            // FirebaseAnalytics.LogEvent("Growth_Action", 
            //                 new Parameter("Stage", stage),
            //                 new Parameter("Times", times),
            //                 new Parameter("Type", type),
            //                 new Parameter("UpgradeLevel", upgradeLevel),
            //                 new Parameter("Coin", coin),
            //                 new Parameter("Cost", cost),
            //                 new Parameter("SlingLevel", slingLevel),
            //                 new Parameter("PlaneLevel", planeLevel),
            //                 new Parameter("IncomeLevel", incomeLevel));
        });
        Debug.Log("Growth_Action:" + stage + ", " + times + ", " + type + ", " + upgradeLevel + ", " + coin + ", " + cost + ", " + slingLevel + ", " + planeLevel + ", " + incomeLevel);
    }

    /// <summary>
    /// インステ
    /// </summary>
    /// <param name="CanWatch">true or false	String型</param>
    /// <param name="Stage">ステージ数	INT型</param>
    /// <param name="Times">合計挑戦回数	INT型</param>
    /// <param name="SlingLevel">補強レベル	INT型</param>
    /// <param name="PlaneLevel">補強レベル	INT型</param>
    /// <param name="IncomeLevel">補強レベル	INT型</param>
    public void EventWatchInste(bool canWatch, int stage, int times, int slingLevel, int planeLevel, int incomeLevel)
    {
        TryLogEvent(()=>{
        //    FirebaseAnalytics.LogEvent("Watch_Inste", 
        //                     new Parameter("CanWatch", canWatch.ToString()),
        //                     new Parameter("Stage", stage),
        //                     new Parameter("Times", times),
        //                     new Parameter("SlingLevel", slingLevel),
        //                     new Parameter("PlaneLevel", planeLevel),
        //                     new Parameter("IncomeLevel", incomeLevel));
        });
        
        Debug.Log("Watch_Inste:" + canWatch + ", " + times + ", " + stage + ", " + slingLevel + ", " + planeLevel + ", " + incomeLevel);
    }
    public void EventWatchBanner(bool isWatch)
    {
    //     FirebaseAnalytics.LogEvent("Watch_Banner", 
    //                         new Parameter("Stage", SaveDataManager.GetCurrentStage() + 1),
    //                         new Parameter("CanWatch", isWatch.ToString()));
    }

    /// <summary>
    /// リワード
    /// </summary>
    /// <param name="CanWatch">true or false	String型</param>
    /// <param name="Stage">ステージ数	INT型</param>
    /// <param name="Times">合計挑戦回数	INT型	ステージが変わると0に戻る</param>
    /// <param name="Type">SlingLevel,PlaneLevel,IncomeLevel	String型</param>
    /// <param name="UpgradeLevel">補強レベル	INT型	補強後のレベル</param>
    /// <param name="SlingLevel">補強レベル	INT型</param>
    /// <param name="PlaneLevel">補強レベル	INT型</param>
    /// <param name="IncomeLevel">補強レベル	INT型</param>
    public void EventWatchReward(bool canWatch, int stage, int times, string type, int upgradeLevel, int slingLevel, int planeLevel, int incomeLevel)
    {
        TryLogEvent(()=>{
            // FirebaseAnalytics.LogEvent("Watch_Reward", 
            //                 new Parameter("CanWatch", canWatch.ToString()),
            //                 new Parameter("Stage", stage),
            //                 new Parameter("Times", times),
            //                 new Parameter("Type", type),
            //                 new Parameter("UpgradeLevel", upgradeLevel),
            //                 new Parameter("SlingLevel", slingLevel),
            //                 new Parameter("PlaneLevel", planeLevel),
            //                 new Parameter("IncomeLevel", incomeLevel));
        });
        Debug.Log("Watch_Reward:" + canWatch + ", " + stage + ", " + times + ", " + type + ", " + upgradeLevel + ", " + slingLevel + ", " + planeLevel + ", " + incomeLevel);
    }
    // ---------- Private関数 ----------

    // firebaseログイベントの実行。まだFirebaseの初期化が終わってないなら、初期化が終わるまで待機する
    private void TryLogEvent(UnityAction logEvent)
    {
        if (!_isFirebaseInitialized)
        {
            Debug.Log( "Firebase is not Initialized" );
            onInit += logEvent;
            return;
        }
        logEvent?.Invoke();
    }
}
