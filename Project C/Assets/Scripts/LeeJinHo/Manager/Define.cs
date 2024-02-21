using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Util
{
    public class Define : MonoBehaviour
    {
        public static class Path
        {
            public const string PREFAB    = "Assets/Resource/Prefab/";
            //public const string SOUND   = "Assets/Resource/Sound/";
            //public const string PLAYER  = "Assets/Resource/Player/";
            //public const string SPRITE  = "Assets/Resource/Sprite/";
            public const string AUDIOCLIP = "Assets/Resource/AudioClip/";
            public const string ANIM      = "Assets/Resource/Anim/";

        }

        public static class Layer
        {
            public const int PLAYER = 6;
            public const int PLAYERBULLET = 7;
            public const int OBSTACLE = 8;
            public const int ENEMY = 9;
            public const int FLYENEMY = 10;
            public const int CAMERATRUE = 11;
            public const int CAMERAFALSE = 12;
            public const int ENEMYBULLET = 13;
        }

        public static class Tag
        {
            public const string PLAYERBULLET = "PlayerBullet";
            public const string WALL = "Wall";
            public const string ENEMY = "Enemy";
            public const string DOOR = "Door";
            public const string MINIMAP = "MiniMap";
            public const string ENEMYBULLET = "EnemyBullet";
            public const string OBSTACLE = "Obstacle";
            public const string BOX = "Box"; //�ʿ� ��������?

        }
    }
}