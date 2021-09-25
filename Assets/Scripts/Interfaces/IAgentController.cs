using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentController
{
   void moveTo(Vector3 to);
   void runTo(Vector3 to);
   void drink();
   void eat();
   void sleep();

   void reproduce(); 
   // No seria void
   void seeAround();


   // void Foo1();
   // void Foo2(); 
}
