/*
 *  # Programmer: Vasyl Onufriyev 
 *  # Date: 8-20-18
 *  # Purpose: Controls AI destinations to attempt to surround the player.
 *  
 */

using System.Collections.Generic;
using UnityEngine;

public class gm_Surround : MonoBehaviour
{
    public List<Transform> positions = new List<Transform>();
    public List<AITargetController> targets = new List<AITargetController>();

    void Start()
    {
        foreach (Transform transform in positions)
        {
            targets.Add(new AITargetController(transform));
        }
    }

    public Transform RequestAIDestination()
    {
        if (AITargetController.GetTaken() >= positions.Count)
            return targets[Random.Range(0, targets.Count - 1)]._position;
        else
            foreach (AITargetController AIpos in targets)
            {
                if (AIpos.MarkTaken())
                    return AIpos._position;
            }
        return null;
    }
}

public partial class AITargetController
{
    public Transform _position;
    private bool _taken;
    private static int _positionsTaken;

    public AITargetController(Transform t)
    {
        _position = t;
    }

    public bool MarkTaken()
    {
        if (!_taken)
        {
            _taken = true;
            _positionsTaken++;
            return true;
        }
        else
            return false;
    }

    public void MarkCleared()
    {
        _taken = false;
        _positionsTaken--;
    }

    public static int GetTaken()
    {
        return _positionsTaken;
    }
}
