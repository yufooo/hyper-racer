using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelController : MonoBehaviour
{
    public delegate void StartPanelDelegate();
    public event StartPanelDelegate OnStartButtonClick;

    public void OnClickStartButton()
    {
        OnStartButtonClick?.Invoke(); //GameManager에서 ShowStartPanel()에서 추가하고 있어서 not null이 된다
    }
}
