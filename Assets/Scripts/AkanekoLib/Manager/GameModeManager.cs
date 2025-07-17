using UnityEngine;
using UnityEngine.Events;
using AkanekoLib;

/// <summary>
/// ゲームモードマネージャー。
/// 
/// </summary>
public enum GameMode
{
    None,
    Main 
}
public enum InGameState
{
    None = 0,
    Idle = 1000,
    Main = 3000,
    Result = 4000 
}
public class GameModeManager : Singleton<GameModeManager>, IInitializer
{
    // ========== 定数 ===============================
    // ========== ゲームオブジェクト参照変数 =============
    // ========== プレハブ ============================
    // ========== シリアライズフィールド ================
    // ========== フィールドとプロパティ =================
    private UnityAction<GameMode> _onChangeGameMode = null;
    public event UnityAction<GameMode> OnChangeGameMode
    { 
        add{ 
            _onChangeGameMode += value;
            if(InitializerBaseClass.IsInitialize)
                value(_gameMode);
        }
        remove{ _onChangeGameMode -= value; }
    }
    public event UnityAction<InGameState> OnChangeInGameState;
    // ゲームモード
    private GameMode _gameMode = GameMode.None;
    public GameMode GameMode{ get{ return _gameMode; } set{ SetGameMode(value); } }
    private InGameState _inGameState = InGameState.None;
    public InGameState InGameState{ get{ return _inGameState; } set{ SetInGameState(value); } }
    [field: SerializeField] public InitializerBase InitializerBaseClass { get; set; }
    // ========== インスタンス変数 =====================
    public IInitializer IInitializer{ get{ return this; } }
    // ========== Unity標準イベント関数 ================
    protected override void Awake()
    {
        base.Awake();
        InitializerBaseClass.Init(this, this);
        GameMainManager.Instance.IInitializer.OnInitialize += InitializerBaseClass.Initialize;
    }
    // ========== Public関数 ========================
    // ========== protected関数 =====================
    // ========== Private関数 =======================
    public void InitializeUnique()
    {
        Debug.Log("ゲームモード初期化");
        _onChangeGameMode?.Invoke(_gameMode);
    }
    // ゲームモード変更時の処理
    private void SetGameMode(GameMode gameMode)
    {
        if(_gameMode == gameMode)
            return;
        _gameMode = gameMode;

        switch(_gameMode)
        {
            default:
                break;
        }
        if(InitializerBaseClass.IsInitialize)
            _onChangeGameMode?.Invoke(_gameMode);
    }
    private void SetInGameState(InGameState inGameState)
    {
        if(_inGameState == inGameState)
            return;
        _inGameState = inGameState;

        switch(_inGameState)
        {
            default:
                break;
        }

        if(InitializerBaseClass.IsInitialize)
            OnChangeInGameState?.Invoke(_inGameState);
    }
}
