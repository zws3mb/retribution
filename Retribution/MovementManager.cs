﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Retribution
{
    class MovementManager
    {
        public static Map myMap;
        static MovementManager instance;

        private MovementManager()
        {
            myMap = null;
        }

        public static MovementManager getInstance()
        {
            if (instance == null)
            {
                instance = new MovementManager();
                return instance;
            }
            else
                return instance;
        }

        public void setMap(Map newMap){
            myMap = newMap;
        }

        //  Helper method to calculate normalized vector
        public static Vector2 getNormalizedVector(Vector2 startVector, Vector2 endVector)
        {
            Vector2 moveVector = Vector2.Subtract(endVector, startVector);
            Vector2 normalizedVector = Vector2.Normalize(moveVector);
            return normalizedVector;
        }

        //  Call movement method of all selected objects
        public void moveObjects(List<GameObject> playerUnits, List<GameObject> aiUnits)
        {
            List<GameObject> allUnits = new List<GameObject>();
            allUnits.AddRange(playerUnits);
            allUnits.AddRange(aiUnits);

            List<Tile> newCollisionList = new List<Tile>();

            for (int i = 0; i < allUnits.Count; i++)
            {

                if (allUnits[i].GetType().BaseType == typeof(Mobile) && ((Mobile)allUnits[i]).isMoving == true)
                {
                    //  If a unit's collision list contains it's destination tile, stop it:
                    if (((Mobile)allUnits[i]).collisionList.Contains(myMap.GetTile(((Mobile)allUnits[i]).destination)))
                        {
                            ((Mobile)allUnits[i]).isMoving = false;
                            ((Mobile)allUnits[i]).isPaused = false;
                            break;
                        }

                    // If the next tile in a units path is on its collision list, pause it:
                    //if (((Mobile)allUnits[i]).collisionList.Contains(((Mobile)allUnits[i]).pathList[0]))
                    //{
                     //   ((Mobile)allUnits[i]).isPaused = true;
                    //    break;
                    //}
                     

                    // Iterate through all units again
                    for (int j = 0; j < allUnits.Count; j++)
                        {
                            //  If i collides with j and i and j are not the same
                            if (allUnits[i].collidesWith(allUnits[j]) && i != j)
                           {
                                //  If j is not moving, paused, or a tower, don't pause
                               if (((allUnits[j].GetType().BaseType == typeof(Mobile) && (((Mobile)allUnits[j]).isMoving == false || ((Mobile)allUnits[j]).isPaused == true)))
                                   || (allUnits[j].GetType() == typeof(Tower))
                                   )
                               {
                                   newCollisionList.Add(myMap.GetContainingTile(allUnits[j]));
                                   ((Mobile)allUnits[i]).isPaused = false;
                               }

                               //  If j is moving, pause i and end loop for this i
                               else
                               {
                                   newCollisionList.Add(myMap.GetContainingTile(allUnits[j]));
                                   ((Mobile)allUnits[i]).isPaused = true;
                                   break;
                               }
                            }
                        }

                        //  If there were no collisions and unit is paused, unpause it
                        if (newCollisionList.Count == 0 && ((Mobile)allUnits[i]).isPaused == true)
                        {
                            ((Mobile)allUnits[i]).isPaused = false;
                            ((Mobile)allUnits[i]).collisionList.Clear();
                            break;
                        }

                        // Else if there was a new collision and the unit isn't paused, get a new path:
                        else if (CompareLists(newCollisionList, ((Mobile)allUnits[i]).collisionList) == false && ((Mobile)allUnits[i]).isPaused == false)
                        {
                            ((Mobile)allUnits[i]).collisionList.Clear();
                            ((Mobile)allUnits[i]).collisionList.AddRange(newCollisionList);

                            ((Mobile)allUnits[i]).pathList.Clear();
                            Vector2 startPoint = new Vector2(allUnits[i].Bounds.Center.X, allUnits[i].Bounds.Center.Y);
                            ((Mobile)allUnits[i]).pathList.AddRange(myMap.GetPath(startPoint, ((Mobile)allUnits[i]).destination, newCollisionList));
                        }

                        //  If unit i isn't paused and is moving, move it
                        if (((Mobile)allUnits[i]).isPaused == false && ((Mobile)allUnits[i]).isMoving == true)
                        {
                            ((Mobile)allUnits[i]).move();
                        }
                    }

                newCollisionList.Clear();
            }
        }


        public static void changeDestination(List<GameObject> listOfSelectedObjects, Vector2 destination)
        {
            for (int i = 0; i < listOfSelectedObjects.Count; i++)
            {
                if (listOfSelectedObjects[i].GetType().BaseType == typeof(Projectile))
                {
                    ((Projectile)(listOfSelectedObjects[i])).setDestination(
                        getNormalizedVector(listOfSelectedObjects[i].getPosition(),
                        ((Projectile)(listOfSelectedObjects[i])).target.getPosition()),
                        ((Projectile)(listOfSelectedObjects[i])).target.getPosition());
                }
                if (listOfSelectedObjects[i].selected == true && listOfSelectedObjects[i].GetType().BaseType == typeof(Mobile))
                {
                    ((Mobile)(listOfSelectedObjects[i])).isMoving = false;
                    ((Mobile)(listOfSelectedObjects[i])).setDestination(destination);
                    List<Tile> newClosedList = new List<Tile>();
                    //System.Console.WriteLine(listOfSelectedObjects[i].destination.X + ", " + listOfSelectedObjects[i].destination.Y);
                    Vector2 startPoint = new Vector2(listOfSelectedObjects[i].Bounds.Center.X, listOfSelectedObjects[i].Bounds.Center.Y);

                    ((Mobile)(listOfSelectedObjects[i])).pathList = myMap.GetPath(startPoint, ((Mobile)(listOfSelectedObjects[i])).destination, newClosedList);
                    ((Mobile)(listOfSelectedObjects[i])).isMoving = true;
                }
            }
        }

        public Boolean CompareLists(List<Tile> newList, List<Tile> oldList)
        {  
            if (newList.Count == oldList.Count)
            {
                for (int i = 0; i < newList.Count; i++)
                {
                    if (newList[i] != oldList[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
