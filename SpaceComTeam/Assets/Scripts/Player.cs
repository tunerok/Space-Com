using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

   // public Transform L_weapon;
    public GameObject L_weapon;
    public GameObject bullet2;
    public float weaponDistance = 0.2f;
    public float BetweenweaponDistance = 0.1f;
    public float weaponCD = 0.3f;
    public float reload = 0.0f;

    public float currentSpeed = 0.0f;
    public float PlayerSpeed = 0.0f;
    public float HP = 100.0f;

    public List<KeyCode> shootBtn;
    public Vector3 position = new Vector3(8.0f,0.0f);


    // Update is called once per frame
    void Update() {

        

        CmdMovement();

        foreach(KeyCode element in shootBtn) {//реагируем на кнопки выстрела
         if (Input.GetKey(element) && reload <0) {
                reload = weaponCD;
                CmdShootLaser();
                break;
        }
        }
        if(reload >= 0)
        reload -= Time.deltaTime; 

    }

    [Command]
    void CmdMovement() {

        Vector3 mouse = Input.mousePosition; //гет мышь


        mouse = Camera.main.ScreenToWorldPoint(mouse);// перевод координат
        if (mouse.y > 4.7f || mouse.y < -4.7f || mouse.x <=-4.0f || mouse.x>8.0f)
        {
            return;
        }
        else {
            position = mouse;
            this.transform.position = new Vector3(position.x- 1.5f, position.y);
        }
    }
    [Command]
    void CmdShootLaser()
    {
        // Высчитываем позицию корабля
        float posX = this.transform.position.x + (Mathf.Cos((transform.localEulerAngles.z - 90) * Mathf.Deg2Rad) * -weaponDistance);
        float posY = this.transform.position.y + (Mathf.Sin((transform.localEulerAngles.z - 90) * Mathf.Deg2Rad) * -weaponDistance);
        // Создаём лазер на этой позиции
        //var bullet1 = (GameObject)Instantiate(L_weapon, new Vector3(posX, posY-BetweenweaponDistance, 0), this.transform.rotation);

        var bullet2 = (GameObject)Instantiate(L_weapon, new Vector3(posX, posY+BetweenweaponDistance, 0), this.transform.rotation);
       // NetworkServer.Spawn(bullet1);
        NetworkServer.Spawn(bullet2);
    }

    void OnTriggerEnter2D(Collider2D theCollision)
    {

        if (theCollision.gameObject.name.Contains("laser(Clone)"))
        {
			//чекаем сколько отнимает данная пушка и отнимаем хп
            LaserScript laser = theCollision.gameObject.GetComponent("LaserScript") as LaserScript;
            HP -= laser.damage;
            // hited.transform.position = this.transform.position;


            // Destroy(theCollision.gameObject);


        }
    }
    }
