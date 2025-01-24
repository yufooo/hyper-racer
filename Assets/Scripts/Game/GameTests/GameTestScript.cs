using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class GameTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void GameTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator GameTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        // 씬 로드하기
        SceneManager.LoadScene("Scenes/Game", LoadSceneMode.Single);
        yield return waitForSceneLoad();
 
        // 필수 오브젝트 확인
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Assert.IsNotNull(gameManager, "GameManager is Null");

        var startButton = GameObject.Find("Start Button");
        Assert.IsNotNull(startButton, "Start Button is Null");
        
        // 게임 실행
        startButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        
        // 게임 제어 관련 버튼 확인
        var leftMoveButton = GameObject.Find("LeftMoveButton");
        Assert.IsNotNull(leftMoveButton, "Left Move Button is Null");
        var rightMoveButton = GameObject.Find("RightMoveButton");
        Assert.IsNotNull(rightMoveButton, "Right Move Button is Null");
        
        // 가스의 등장 위치 파악하기
        Vector3 leftPosition = new Vector3(-1f, 0.2f, -3f);
        Vector3 rightPosition = new Vector3(1f, 0.2f, -3f);
        Vector3 centerPosition = new Vector3(0f, 0.2f, -3f);
        
        float rayDistance = 10f;
        Vector3 rayDirection = Vector3.forward;
        
        // 반복
        while (gameManager.GameState == GameManager.State.Play)
        {
            RaycastHit hit;
            if (Physics.Raycast(leftPosition, rayDirection, out hit, rayDistance, 
                    LayerMask.GetMask("Gas")))
            {
                Debug.Log("left");
            }
            else if (Physics.Raycast(rightPosition, rayDirection, out hit, rayDistance,
                         LayerMask.GetMask("Gas")))
            {
                Debug.Log("right");
            }
            else if (Physics.Raycast(centerPosition, rayDirection, out hit, rayDistance,
                         LayerMask.GetMask("Gas")))
            {
                Debug.Log("center");
            }
            else
            {
                Debug.Log("none");
            }
            
            Debug.DrawRay(leftPosition, rayDirection * rayDistance, Color.red);
            Debug.DrawRay(rightPosition, rayDirection * rayDistance, Color.green);
            Debug.DrawRay(centerPosition, rayDirection * rayDistance, Color.blue);
            
            
            yield return null;
        }
        
    }

    private IEnumerator waitForSceneLoad()
    {
        while (SceneManager.GetActiveScene().buildIndex > 0)
        {
            yield return null;
        }
    }
}
