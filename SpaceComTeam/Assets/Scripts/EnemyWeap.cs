using UnityEngine;
using System.Collections;

public class EnemyWeap : MonoBehaviour {

    public Transform boom;

    // Как долго существует лазер
    public float lifetime = 20000.0f;

    // Как быстро движется лазер
    public float speed = 30.0f;

    // Как много наносит урона лазер при соприкосновении с врагами
    public int damage = 1;

    // Use this for initialization
    void Start()
    {

        // Уничтожение лазера по окончанию таймера

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = transform.position + Vector3.right * Time.deltaTime * speed;
        if (this.transform.position.x < -8.5 || this.transform.position.x > 9.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D theCollision)
    {

        Instantiate(boom, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

}
