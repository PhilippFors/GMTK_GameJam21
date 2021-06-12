using System;
using System.Collections.Generic;
using DataContaner.RuntimeSets;
using General.Utilities;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace LevelManager
{
    public class LevelManager: SingletonBehaviour<LevelManager>
    {
        public List<Room> roomList;
        public Room currentRoom;
        public int currentRoomNumber;

        private void Start()
        {
            //currentRoomNumber = 0;
            //currentRoom = Instantiate(roomList[currentRoomNumber].gameObject, transform).GetComponent<Room>();

        }

        private void Update()
        {
            if (currentRoom.roomFinished)
            {
                currentRoom.roomFinished = false;
                //currentRoomNumber++;
                //currentRoom = Instantiate(roomList[currentRoomNumber].gameObject, transform).GetComponent<Room>();
            }
        }
    }
}