using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurrentControl : MonoBehaviour
{

  public GameObject head;
  public GameObject photon;
  Collider2D target;
  public bool activated;
   
    public int delayValue = 30;
    public float photonSpeed = 0.5f;
     int delay;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        delay = delayValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(activated){
          

       if(delay >  0){
          delay = delay - 1;
         }else{
          fire(target);
           delay = delayValue;
         }

        }
    }

  void OnTriggerEnter2D(Collider2D other){
            target = other;
            activated = true;  
       Debug.Log("entered");
   }
  void OnTriggerExit2D(Collider2D other){
        activated = false;
       Debug.Log("Exited");
    }

    void fire(Collider2D other){
      
    int angle = getAngle(other);
   // Debug.Log("Laser class angle: " + angle);
   //  head.transform.Rotate(0,0, angle);
     head.transform.eulerAngles = new Vector3(0, 0, angle);
   GameObject photonProjectile = Instantiate(photon); 

  Photon photonControl = photonProjectile.GetComponent<Photon>();
   
    photonControl.setSpeed(photonSpeed);
    photonControl.createPhoton(transform.position.x, transform.position.y, other, angle);
  }



   public int getAngle(Collider2D other){
  float x1 = transform.position.x;
  float y1 = transform.position.y;
  float x2 = other.transform.position.x;
  float y2 = other.transform.position.y;

  float h = Mathf.Sqrt(Mathf.Pow((x2-x1), 2) + Mathf.Pow((y2-y1), 2));
  float t = (Mathf.Asin(Mathf.Abs((y1-y2)) / h))*100;
  // Debug.Log("the angle: " + t);
  int It = (int)(t);
  
 int check = Mathf.Abs(It + findQuadrent(other.gameObject));
   Debug.Log(check);
     return check;
  }

int findQuadrent(GameObject target){
   
   int value = 0;
   Vector2 pos = target.transform.position;
   Vector2 thisP = transform.position;
   if(pos.x > thisP.x && pos.y > thisP.y){
      value = 0;
   } else if(pos.x < thisP.x && pos.y > thisP.y){
      value = -180;
     }  else if(pos.x < thisP.x && pos.y < thisP.y){
         value = 180;
     } else if (pos.x > thisP.x && pos.y < thisP.y){
        value = -360;
      }
 //  Debug.Log("quadrent " + value);


  return value;
}

}
