﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BotLight
{
    public class GameManager : MonoBehaviour
    {

        public int startingAnimals;    // Think to use a scriptableobject to param instead
        public GameObject[] animalPrefabs;        // prefabs
        public GameObject[] foodPrefabs;
        public GameObject groundPrefab;
        [HideInInspector]
        public List<GameObject> foodInstance;
        public CameraControl cameraControl;       // Reference to the CameraControl script for control during different phases.
        [HideInInspector]
        public List<SphereManager> spheres;     // A collection of managers for enabling and disabling different aspects of the spheres.
        public List<Transform> wayPointsForAI;

        // Use this for initialization
        void Start()
        {
            Instantiate(groundPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            SpawnAllAnimals();
            
            InvokeRepeating("SpawnFood", 5, 10);
            InvokeRepeating("DeleteFood", 62, 60);
        }

        public void SpawnAllAnimals()
        {
            for (int i = 0; i < startingAnimals; i++)
            {
                SphereManager sphere = new SphereManager();
                sphere.instance = Instantiate(animalPrefabs[Random.Range(0, animalPrefabs.Length)], new Vector3(i * 80, 50, i * 80), new Quaternion(0, 0, 0, 0)) as GameObject;
                sphere.sphereNumber = i + 1;
                sphere.SetupAI(wayPointsForAI);
                spheres.Add(sphere);

            }
        }

        public void DeleteFood()
        {
            Destroy(foodInstance[0]);
            foodInstance.RemoveAt(0);
        }

        public void SpawnFood()
        {
            int whichPrefab = Random.Range(0, foodPrefabs.Length);

            Vector3 spawnPosition = new Vector3(Random.Range(0, groundPrefab.transform.localScale.x),
                Random.Range(1, groundPrefab.transform.localScale.y),
                Random.Range(0, groundPrefab.transform.localScale.z));


            foodInstance.Add(Instantiate(foodPrefabs[whichPrefab], 
                GetEmptyFoodSpace(spawnPosition, whichPrefab), 
                new Quaternion(0, 0, 0, 0)) as GameObject);
        }

        private Vector3 GetEmptyFoodSpace(Vector3 initialSpawnPosition, int whichPrefab)
        {
            foreach(GameObject GO in foodInstance){
                if (GO.transform.localScale == initialSpawnPosition) { 
                    initialSpawnPosition.x += foodPrefabs[whichPrefab].transform.localScale.x;
                    initialSpawnPosition.y += foodPrefabs[whichPrefab].transform.localScale.y;
                    initialSpawnPosition.z += foodPrefabs[whichPrefab].transform.localScale.z;
                    return initialSpawnPosition;
                }
            }
            return initialSpawnPosition;


        }

        private void SetCameraTargets()
        {
            // Create a collection of transforms the same size as the number of tanks.
            List<Transform> targets = new List<Transform>();

            // For each of these transforms...
            for (int i = 0; i < targets.Count; i++)
            {
                // ... set it to the appropriate tank transform.
                targets[i] = spheres[i].instance.transform;
            }

            // These are the targets the camera should follow.
            cameraControl.targets = targets;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}