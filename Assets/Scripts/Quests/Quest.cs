using System;
using System.Collections;
using System.Collections.Generic;
using CompassNavigatorPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest", order = 0)]
public class Quest : ScriptableObject
{
    [SerializeField] private List<Objective> objectives = new List<Objective>();
    [SerializeField] private List<Reward> rewards = new List<Reward>();

    [System.Serializable]
    public class Reward
    {
        [Min(1)] public int number;
        public InventoryItem item;
    }

    [Serializable]
    public class Objective
    {
        public string reference;
        public string description;
        public bool isCollectItemQuest;
        public InventoryItem itemToCollect;
        public int quantity;
        public bool isKillEnemyQuest;
        public GameObject enemy;
        public int number;
        public GameObject compassProPOILocation;
        public GameObject compassProPOINpc;
    }

    public string GetTitle()
    {
        return name;
    }

    public int GetObjectiveCount()
    {
        return objectives.Count;
    }

    public IEnumerable<Objective> GetObjectives()
    {
        return objectives;
    }

    public IEnumerable<Reward> GetRewards()
    {
        return rewards;
    }

    public bool HasObjective(string objectiveRef)
    {
        foreach (var objective in objectives)
        {
            if (objective.reference == objectiveRef)
            {
                return true;
            }
        }

        return false;
    }

    public static Quest GetByName(string questName)
    {
        foreach (var quest in Resources.LoadAll<Quest>(""))
        {
            if (quest.name == questName)
            {
                return quest;
            }
        }

        return null;
    }
}