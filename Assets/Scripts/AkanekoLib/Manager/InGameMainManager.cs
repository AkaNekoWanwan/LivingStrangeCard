using UnityEngine;

/// <summary>
/// インゲームのメインマネージャー
/// </summary>
public class InGameMainManager : MonoBehaviour, IInitializer
{
    // ========== 定数 ===============================
    // ========== ゲームオブジェクト参照変数 =============
    [SerializeField, Tooltip("meow")] private Transform _meow = default;
    // ========== プレハブ ============================
    // ========== シリアライズフィールド ================
    [SerializeField, Tooltip("hoge")] private int _hoge = default;
    // ========== フィールドとプロパティ =================
    private int _huga;
    public int Huga{ get{ return _huga; } set{ _huga = value; } }
    [field: SerializeField] public InitializerBase InitializerBaseClass { get; set; }
    // ========== インスタンス変数 =====================
    // ========== Unity標準イベント関数 ================
    private void Awake()
    {
        InitializerBaseClass.Init(this, this);
        GameModeManager.Instance.OnChangeGameMode += OnChangeGameMode;
        GameModeManager.Instance.OnChangeInGameState += OnChangeInGameState;
    }
    private void Start(){
        
    }

    private void Update(){
        switch(GameModeManager.Instance.InGameState)
        {
            case InGameState.Idle:
                // 強化画面
                IdleUpdate();
                break;
            case InGameState.Main:
                // 飛んでる
                MainUpdate();
                break;
            case InGameState.Result:
                // リザルト
                ResultUpdate();
                break;
            default:
                break;
        }
    }
    // ========== Public関数 ========================
    // ========== protected関数 =====================
    // ========== Private関数 =======================
    public void InitializeUnique()
    {
        Debug.Log("インゲーム初期化");
        GameModeManager.Instance.InGameState = InGameState.Idle;
    }
    // ゲームモードが変わった時の処理
    private void OnChangeGameMode(GameMode gameMode)
    {
        // インゲームになったら
        if(gameMode == GameMode.Main)
        {
            // インゲームを始める処理
            InitializerBaseClass.Initialize();
        }
        // インゲーム終了処理
        else
        {
            // インゲーム終了処理
            FinishInGame();
        }
    }
        // インゲームが終わった時の後片付け
    private void FinishInGame()
    {
        GameModeManager.Instance.InGameState = InGameState.None;
    }

    // インゲーム段階更新時の処理
    private void OnChangeInGameState(InGameState inGameState)
    {

    }

    /// <summary>
    /// 開始前でのUpdate処理
    /// </summary>
    private void IdleUpdate()
    {

    }
    /// <summary>
    /// メインルーチン時のUpdate処理
    /// </summary>
    private void MainUpdate()
    {

    }
    /// <summary>
    /// リザルトでのUpdate処理
    /// </summary>
    private void ResultUpdate()
    {

    }
}
