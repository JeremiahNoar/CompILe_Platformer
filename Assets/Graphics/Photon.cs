using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photon : MonoBehaviour
{
  float x1;
  float y1;
  float x2;
  float y2;
  float m;
  float b;
  float direction;
  float currentX;
  float t;
  float speed = 0.5f;
  int count = 0;
  
  float currentY;
  Vector2 movement = new Vector2(0.0f,0.0f);

  public void createPhoton(float posX, float posY, Collider2D target, int angle){
   t = angle;
   x1 = posX;
   y1 = posY;
   x2 = target.transform.position.x;
   y2 = target.transform.position.y;
   m = (y2-y1)/(x2-x1);
   b = -(m * x1) + y1;
   currentX = x1;
          transform.Rotate(0,0, t);
  }
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           changePosition();
             if(transform.position.magnitude > 100.0f){
                 Destroy(gameObject);
             }         
         }


   void changePosition(){
    

      if(x1> x2){
          direction = -1;

      }else{
          direction =1;
        }

     if(m != null){
        currentX = currentX + (speed * direction );
         currentY = (m*currentX) + b ;

  movement.x = currentX;
  movement.y = currentY;
   
  
transform.position = movement;
     
         }


}

public void setSpeed(float newSpeed){
   speed = newSpeed;
 }


void OnTriggerEnter2D(Collider2D other){
  PlayerController controller = other.gameObject.GetComponent<PlayerController>();

   if(controller != null){
      controller.changeHealth(-1);
   }
}


   
}
