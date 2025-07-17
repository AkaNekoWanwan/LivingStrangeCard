using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // 新しい Input System 用
using AkanekoLib;


/// <summary>
/// ユーザーの入力管理マネージャー
/// </summary>
public class UserInputManager : Singleton<UserInputManager>
{
    #region Fields and Properties
    private bool _isTouch = false;
    public bool IsTouch{ get{ return _isTouch; } }  // 押し続けてるか
    private bool _isTapIn = false;
    public bool IsTapIn{ get{ return _isTapIn; } }  // 押した瞬間か
    private bool _isUITouch = false;
    public bool IsTouchUi{ get{ return _isTouch && _isUITouch; } }
    private bool _isTouchedPlayerHuman = false;
    public bool IsTouchedPlayerHuman{ get{ return _isTouchedPlayerHuman; } }
    private bool _isPulledPlayerHuman = false;
    public bool IsPulledPlayerHuman{ get{ return _isPulledPlayerHuman; } set{ if(value == false) _isPulledPlayerHuman = false; } }
    private Vector2 _tapInPos = default;
    public Vector2 TapInPos{ get{ return _tapInPos; } }
    private Vector2 _currentTapPos = default;
    public Vector2 CurrentTapPos{ get{ return _currentTapPos; } }
    private Vector3 _currentTapWorldPos = default;
    public Vector3 CurrentTapWorldPos{ get{ return _currentTapWorldPos; } }
    public Vector2 SwipeVec{ get{ return _currentTapPos - _tapInPos; } }
    private float _distance = 0f;
    public float Distance{ get{ return _distance; } }
    #endregion

    #region BuiltIn Methods
    #endregion

    #region Custom public Methods
    public void Initialize()
    {
        _tapInPos = new Vector2();
        _currentTapPos = new Vector2();
        _isPulledPlayerHuman = false;
    }
    public void UpdateUserInput()
    {
        // UIをクリックしたか
        if(EventSystem.current.IsPointerOverGameObject())
        {
            _isUITouch = true;
        }
        else
        {
            _isUITouch = false;
        }

        // マウスの左クリック (PC用)
        bool isMouseClicked = Mouse.current != null && Mouse.current.leftButton.isPressed;

        // スマホのタッチ (スマホ用)
        bool isTouching = Touchscreen.current != null && Touchscreen.current.press.isPressed;

        // マウスの位置 (PC)
        if (Mouse.current != null)
        {
            _currentTapPos = Mouse.current.position.ReadValue();
        }

        // タッチの位置 (スマホ)
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
        {
            _currentTapPos = Touchscreen.current.primaryTouch.position.ReadValue();
        }

        // タッチ(クリック)したか
        if(isMouseClicked || isTouching)
        {
            if(!_isTouch)
            {
                _tapInPos = _currentTapPos;
                _isTapIn = true;
            }
            else
                _isTapIn = false;
            _isTouch = true;
            Vector3 screenPos = new Vector3(_currentTapPos.x, _currentTapPos.y, _distance);  
            // ワールド座標に変換  
            _currentTapWorldPos = Camera.main.ScreenToWorldPoint(screenPos); 
        }
        else
        {
            _isTouch = false;
        }

        if(_isTapIn)
            Debug.Log("画面クリック！:" + _currentTapPos + ", UI:" + _isUITouch);
    }
    #endregion

    #region Custom private Methods
    #endregion
}
