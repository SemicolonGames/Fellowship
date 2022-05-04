using System;
using System.Collections;
using System.Collections.Generic;
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

    [System.Serializable]
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
        foreach (Quest quest in Resources.LoadAll<Quest>(""))
        {
            if (quest.name == questName)
            {
                return quest;
            }
        }

        return null;
    }

    public bool GetIsCollectItemQuest(Predicate<Objective> objective)
    {
        return objectives.Find(objective).isCollectItemQuest;
    }

    public bool? GetIsCollectItemQuest(string objectiveRef)
    {
        foreach (Objective objective in objectives)
        {
            if (objective.reference == objectiveRef)
                return objective.isCollectItemQuest;
        }

        return null;
    }

    public bool GetIsKillEnemyQuest(Predicate<Objective> objective)
    {
        return objectives.Find(objective).isKillEnemyQuest;
    }

    public bool? GetIsKillEnemyQuest(string objectiveRef)
    {
        foreach (Objective objective in objectives)
        {
            if (objective.reference == objectiveRef)
                return objective.isKillEnemyQuest;
        }

        return null;
    }

    public int GetCollectQuantity(Predicate<Objective> objective)
    {
        return objectives.Find(objective).quantity;
    }

    public int? GetCollectQuantity(string objectiveRef)
    {
        foreach (Objective objective in objectives)
        {
            if (objective.reference == objectiveRef)
                return objective.quantity;
        }

        return null;
    }

    public int GetKillNumber(Predicate<Objective> objective)
    {
        return objectives.Find(objective).number;
    }

    public int? GetKillNumber(string objectiveRef)
    {
        foreach (Objective objective in objectives)
        {
            if (objective.reference == objectiveRef)
                return objective.number;
        }

        return null;
    }

    public InventoryItem GetItemToCollect(Predicate<Objective> objective)
    {
        return objectives.Find(objective).itemToCollect;
    }

    public InventoryItem GetItemToCollect(string objectiveRef)
    {
        foreach (Objective objective in objectives)
        {
            if (objective.reference == objectiveRef)
                return objective.itemToCollect;
        }

        return null;
    }

    public GameObject GetEnemy(Predicate<Objective> objective)
    {
        return objectives.Find(objective).enemy;
    }

    public GameObject GetEnemy(string objectiveRef)
    {
        foreach (Objective objective in objectives)
        {
            if (objective.reference == objectiveRef)
                return objective.enemy;
        }

        return null;
    }
}