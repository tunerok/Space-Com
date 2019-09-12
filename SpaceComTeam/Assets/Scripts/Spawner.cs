using UnityEngine;
using System.Collections;
using UnityEngine.UI; // The namespace for the UI stuff.

public class Spawner : MonoBehaviour
{

    public Transform []enemy;


    public float timeBeforeEnemy = 5.0f;
    public float timeBetweenEnemy = 2.0f;
    public float timeBetweenWaves = 10.0f;

    public int enemyUpper = 2;
    public int enemyCount = 2;
    private int currentEnemy = 0;
    public int score_of_game = 0;
    private Text score_text_data;

    // Use this for initialization
    void Start()
    {
        GameObject scoreGameObject = GameObject.Find("data_score");

        //Get the Text Component attached to that GameObject named Best
        score_text_data = scoreGameObject.GetComponent<Text>();

        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void KilledEnemy(int enemy_type)
    {
        score_of_game = score_of_game + enemy_type*10;
        score_text_data.text = score_of_game.ToString();
        currentEnemy--;
    }

    public void KilledEnemyNoPoint() {
        currentEnemy--;
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(timeBeforeEnemy);
        while (true)
        {
            if (currentEnemy <= 0)
            {
                float randDistancex;
                float randDistancey;
                int randEnemy;
                enemyCount += enemyUpper;
                for (int i = 0; i < enemyCount; i++)
                {
                    randDistancex = Random.Range(-11, -25);
                    randDistancey = Random.Range(-4.0f, 4.0f);
                    float posX = this.transform.position.x + randDistancex;
                    float posY = this.transform.position.y + randDistancey;
                    // Создаём врага на заданных координатах
                    randEnemy = Random.Range(0, 5);
                    Instantiate(enemy[randEnemy], new Vector3(posX, posY, 0), this.transform.rotation);
                    currentEnemy++;
                    yield return new WaitForSeconds(timeBetweenEnemy);
                }

            }
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}
