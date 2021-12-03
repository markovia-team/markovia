using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgentController
{
   void moveTo(Vector3 to);
   void moveTo(GameObject to);
   void runTo(Vector3 to);
   void drink();
   void eat();
   void sleep();

   void reproduce(); 
   // No seria void
   void seeAround();

   void ResetCoroutines();
   void BeginSolvingState();
   bool IsSolving();
   bool IsGoing();
   void Going();
   void IsThere();
   bool IsHere(Vector3 to);

   Species GetSpecies();

   GameObject getBestWaterPosition(); 
   GameObject getBestFoodPosition();
   Agent findMate();
}
