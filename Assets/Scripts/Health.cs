using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class Health : MonoBehaviour {
        public int curHealth = 0;
        public int maxHealth = 100;
        public HealthBar healthBar;
        private AudioSource audioSource;
        void Start(){
            curHealth = maxHealth;
            audioSource = GetComponent<AudioSource>();
        }
        void Update(){
            if( Input.GetKeyDown( KeyCode.B )){DamagePlayer(10);}
        }
        public void DamagePlayer(int damage ){
            curHealth -= damage;
        healthBar.SetHealth( curHealth );
        if (curHealth<=0){
            print("this is when player died, the end");
        }
        }
        public void HealPlayer(int heal){
            if (curHealth+heal>maxHealth){
                curHealth=maxHealth;
                healthBar.SetHealth(curHealth);
            } else{
            curHealth+=heal;
        healthBar.SetHealth(curHealth);
            }
            audioSource.Play();
        }



    }