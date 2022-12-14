using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class OnClickButton : MonoBehaviour
{
    public GameObject mMenu;
    public GameObject mOptionBtn;
    public GameObject mGameOver;
    public GameObject mGameClear;

    [Header("?? ??ȯ?? ????")]
    public Image mImage;
    public TMP_Text mTMP;

    [Header("Ʃ?丮??(????ǥ) UI")]
    public GameObject[] mTutorialUI;
    private int mTutorialCnt = 0;

    [Header("?? Ʃ?丮?? UI")]
    public GameObject mMiniMapTutorial;

    private void Start()
    {
        mImage.CrossFadeAlpha(0f, 0f, true);
        mTMP.CrossFadeAlpha(0f, 0f, true);
    }

    public void StartGame()
    {
        LoadingSceneImage();
        mMenu.SetActive(false);

        StartCoroutine(LoadSceneCoroutine());
        
    }
    public void GoToOptions()
    {
        mOptionBtn.SetActive(true);
        mMenu.SetActive(false);
    }
    public void GoToMenu()
    {
        mMenu.SetActive(true);
        mOptionBtn.SetActive(false);
    }
    public void GetOutGameScene()
    {
        mMenu.SetActive(false);
        mGameOver.SetActive(false);
        mGameClear.SetActive(false);
        SceneManager.LoadSceneAsync("MainOpen");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void QuitMenu()
    {
        mMenu.SetActive(false);
    }
    public void GoTutorial()
    {
        mMenu.SetActive(false);
        mTutorialUI[0].SetActive(true);
    }
    public void TutorialGotoMenu()
    {
        for (int i = 0; i < mTutorialUI.Length; i++)
        {
            mTutorialUI[i].SetActive(false);
        }
        mMenu.SetActive(true);
    }
    public void TutorialGoRight()
    {
        //Debug.Log("aaa");
        if(mTutorialCnt<mTutorialUI.Length)
        {
            mTutorialUI[mTutorialCnt].SetActive(false);
            ++mTutorialCnt;
            if (mTutorialCnt == mTutorialUI.Length)
            {
                mTutorialCnt = 0;
            }
            mTutorialUI[mTutorialCnt].SetActive(true);
        }
    }
    public void TutorialGoLeft()
    {
        if (mTutorialCnt < mTutorialUI.Length)
        { 
            mTutorialUI[mTutorialCnt].SetActive(false);
            //Debug.Log($"???̳ʽ? ??{ mTutorialCnt}");
            --mTutorialCnt;
            //Debug.Log($"???̳ʽ? ??{ mTutorialCnt}");
            if (mTutorialCnt < 0)
            {
                mTutorialCnt = mTutorialUI.Length-1;
            }
            mTutorialUI[mTutorialCnt].SetActive(true);
        }
    }
    public void TutorialGoOut()
    {
        mMiniMapTutorial.SetActive(false);
    }
    private void LoadingSceneImage()
    {
        mImage.CrossFadeAlpha(1f, 2f, false);
        mTMP.CrossFadeAlpha(1f, 2f, false);
    }

    private IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadSceneAsync("SW_TestScene");
    }
}