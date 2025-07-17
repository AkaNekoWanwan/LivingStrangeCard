using UnityEngine;
using UnityEngine.Events;
using AkanekoLib;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// ゲームメインマネージャー　ゲーム起動時の設定や、ゲーム全体を通したデータの管理を行う
/// 
/// </summary>
// [DefaultExecutionOrder(-100)]   // デバッグ用クラスの実行順変更。低いほどはやい
public class GameMainManager : Singleton<GameMainManager>, IInitializer
{
    // ========== 定数 =========================================
    // ========== ゲームオブジェクト参照変数 =======================
    // ========== プレハブ ======================================
    // ========== シリアライズフィールド ==========================
    // ========== フィールドとプロパティ ==========================
    [field: SerializeField] public InitializerBase InitializerBaseClass { get; set; }
    private IInitializer _iInitializer;
    public IInitializer IInitializer => _iInitializer ??= this;
    public event UnityAction<UnityAction> OnReloadScene;
    public event UnityAction OnReloadSceneEnd;
    // ========== インスタンス変数 ===============================
    // ========== Unity標準イベント関数 ==========================
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void SetupGame()
    {
        Debug.Log("ゲームの初期設定を適用");
        // QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    protected override void Awake()
    {
        base.Awake();
        InitializerBaseClass.Init(this, this);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        InitializerBaseClass.Initialize();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            ReloadScene();
        }
        UserInputManager.Instance.UpdateUserInput();
    }
    // ========== Public関数 ========================
    public void ReloadScene()
    {
        // フェードインなど、シーン再読み込み演出があるならそれをしてから再読み込みする
        if(OnReloadScene == null)
            StartCoroutine(ReloadSceneCoroutine());
        else
        {
            OnReloadScene.Invoke(()=>{ StartCoroutine(ReloadSceneCoroutine()); });
        }
    }
    // ========== protected関数 =====================
    // ========== Private関数 =======================
    private IEnumerator ReloadSceneCoroutine()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        
        // シーンを非同期ロード
        SceneManager.LoadScene(sceneName);

        // シーンのロード完了を待機
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);

        // 新しいシングルトンの処理
        GameModeManager.Instance.GameMode = GameMode.Main;

        InitializerBaseClass.InvokeInitialize();

        // シーン読み込み終了演出（フェードアウトなど）
        OnReloadSceneEnd?.Invoke();
    }

    // 初期化処理
    public void InitializeUnique()
    {
        Debug.Log("ゲーム初期化");

        GameModeManager.Instance.GameMode = GameMode.Main;
        UserInputManager.Instance.Initialize();
    }
}