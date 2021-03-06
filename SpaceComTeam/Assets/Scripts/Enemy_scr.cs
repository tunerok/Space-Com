﻿using UnityEngine;
using System.Collections;

public class Enemy_scr : MonoBehaviour {


    public Transform L_EN_weapon;
    public float weaponCD = 2.0f;
    public float reload = 0.0f;


    private float speed = 1.5f;
    private float speedR = 1.5f;

    public float moveSpeed = 0.0f;
    public float roatSpeed = 0.0f;

    public bool isBoat = false;

    public int enemyHP = 2;
    public Transform explosion;
	
	private int enemy_type; //типы врагов. 1 - астероиды, 2 - корабли красные


    public float weaponDistance = 0.9f;

    void Start() {//задаем случайные значения ротации, скорости и т.д.
        if (isBoat) {
            enemy_type = 2;
            this.transform.Rotate(0, 0, -90, 0);
    }
    else{
        enemy_type = 1;
    }
        speed = Random.Range(0.1f, 2.5f);
        speedR = Random.Range(-20.0f, 20.0f);
    }

    void Update() {
    if(this.transform.position.x>10) {//если враг улетел, то мы за него ничего не получаем
            
            Spawner controller = GameObject.FindGameObjectWithTag("GameController").GetComponent("Spawner") as Spawner;
            controller.KilledEnemyNoPoint();
			Destroy(gameObject);
        }
       moveSpeed = speed * Time.deltaTime;
        this.transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y);

		switch (enemy_type){
		
        case 1://поведение астероидов
            roatSpeed = speedR * Time.deltaTime;
            this.transform.Rotate(0, 0, roatSpeed, 0);
            this.transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y );
			break;
        case 2://поведение кораблей врага
			if(reload<0) {
                ShootLaser();
                reload = weaponCD;
				}
            if (reload >= 0)
                reload -= Time.deltaTime;
			

            this.transform.position = new Vector3(transform.position.x + moveSpeed,  Mathf.Abs(4.5f - speed)*Mathf.Sin(transform.position.x));
            break;
        default:
			break;
		}
        

    }
	
    void OnTriggerEnter2D(Collider2D theCollision) {//коллайдер соударений лазера и врага

    if(theCollision.gameObject.name.Contains("laser(Clone)")) {

            LaserScript laser = theCollision.gameObject.GetComponent("LaserScript") as LaserScript;
            enemyHP -= laser.damage;
           // hited.transform.position = this.transform.position;


           // Destroy(theCollision.gameObject);
            

        }
    if(enemyHP <= 0) {
            
            if (explosion)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;
                Destroy(exploder, 2.0f);
            }

            Spawner controller = GameObject.FindGameObjectWithTag("GameController").GetComponent("Spawner") as Spawner;
            controller.KilledEnemy(enemy_type);
			Destroy(this.gameObject);
        }


    }

    void ShootLaser()
    {
        // Высчитываем позицию корабля
        float posX = this.transform.position.x  + weaponDistance;
        float posY = this.transform.position.y ;
        // Создаём лазер на этой позиции
        Instantiate(L_EN_weapon, new Vector3(posX, posY, 0), this.transform.rotation);
    }
}
