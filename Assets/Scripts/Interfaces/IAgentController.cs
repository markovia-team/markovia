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

   void FinishedSolvingState();
   void BeginSolvingState();
   bool IsSolving();
   bool IsGoing();
   void Going();
   void IsThere();
   bool IsHere(Vector3 to);

   Vector3 getBestWaterPosition(); 
   Vector3 getBestFoodPosition(); 
}
