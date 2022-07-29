using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject glassball; // 유리공 프리팹 콘센트 구멍 (프리팹 = 파일로 된 게임 오브젝트)
    public GameObject player;

    public GameObject hearts; // 플레이어의 체력을 보여주는 하트를 그룹화한 게임 오브젝트
    public GameObject gameOver; // 게임 오버 화면을 포함하고 있는 게임 오브젝트

    public Sprite heart_full; // Sprite = 2D 또는 UI에서 사용할 수 있는 이미지 종류, 가득찬 하트 이미지
    public Sprite heart_empty; // 빈 하트 이미지

    public Text text_MaxScore; // 최대 점수, 텍스트 컴포넌트가 붙은 게임 오브젝트
    public Text text_NowScore; // 현재 점수

    public float spawnPeriod = 1f; // 스폰 주기를 결정

    public int Health
    {
        get
        {
            return health;
        }

        private set  // 프로퍼티의 set 부분에 private이 붙으면 이 클래스에서만 해당 프로퍼티의 값을 수정 할 수 있음.
        {
            health = value;
        }
    }

    private int health = 3;

    private int maxScore = 0;
    private int nowScore = 0;

    private float nowPeriod = 0f;  // 다음 스폰까지 얼마나 남았는지 확인하는 변수

    private bool isNewBest = false;  //  이전에 달성한 최고 점수를 갱신했는지 확인하는 변수



    private void Start()
    {
        InitGame();
        
    }

    
    private void Update()
    {
        // 플레이어의 체력이 0 이하일 때
        if (health <= 0)
        {
            // 딱 0이 되었을 때
            if (health == 0)
            {
                // 게임 오버
                // - 게임 오버 된 순간 화면에 존재하는 모든 유리공 삭제
                // - 게임 오버 화면을 띄울 것
                // 체력을 한 번 더 깎음

                GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

                /*for (int i = 0; i < obstacles.Length; i++)
                {
                    Destroy(obstacles[i]);
                }*/

                foreach (GameObject obj in obstacles)
                {
                    Destroy(obj);
                }

                // 게임 오버 화면 띄움
                gameOver.SetActive(true);

                health--;
            }
            // 그 외의 경우
            else
            {
                // 모든 프레임 업데이트 중단;
                return;
            }
        }

        // 스폰 주기 시간보다 마지막 스폰 후 흐른 시간이 더 크다면 유리공 스폰
        if (nowPeriod >= spawnPeriod)
        {
            nowPeriod = 0f;

            float random_x = Random.Range(-11f, 11f);

            Instantiate(glassball, new Vector2(random_x, 6f), Quaternion.identity);
        }

        nowPeriod += Time.deltaTime;

        text_NowScore.text = nowScore.ToString() + "점";

        if (nowScore >= maxScore)
        {
            if (maxScore > 0 && !isNewBest)
            { 
                nowScore += 30;
                isNewBest = true;

                text_NowScore.gameObject.SetActive(false);
            }

            maxScore = nowScore;
            text_MaxScore.text = "최고" + maxScore.ToString() + "점";
        }
    }

    private void InitGame()  // 게임을 초기화하는 메소드
    {
        nowScore = 0;

        // 게임을 처음 시작하게 되면 무조건 해당 점수가 최대 점수가 되기 때문에
        // 최고 점수만 표시를 하고, 2회차부터 현재 점수를 표시
        if (maxScore > 0)
        {
            text_NowScore.gameObject.SetActive(true);
        }

        text_NowScore.text = nowScore.ToString() + "점";
        text_MaxScore.text = "최고" + maxScore.ToString() + "점";


        text_NowScore.text = nowScore.ToString();

        player.GetComponent<SpriteRenderer>().color = Color.white;

        for (int i = 0; i < 3; i++)
        {
            // 빈 하트로 그려진 모든 하트를 가득찬 하트로 변경
            hearts.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = heart_full;

            health = 3;

            isNewBest = false;
        }
    }

    public void OnPlayerAvoid()
    {
        nowScore += 20;
    }

    public void OnPlayerHit()
    {
        if (health <= 0)
        {
            return;
        }
        else
        {
            health--;

            for (int i = 2; i >= 0; i--)
            {
                // 최대 체력의 개수 - 1
                Image temp = hearts.transform.GetChild(i).gameObject.GetComponent<Image>();

                if (temp.sprite == heart_full)
                {
                    temp.sprite = heart_empty;
                    break;
                }    
            }
        }
    }

    public void OnRestartButtonClicked()
    {
        InitGame();
        gameOver.SetActive(false);
    }

}
