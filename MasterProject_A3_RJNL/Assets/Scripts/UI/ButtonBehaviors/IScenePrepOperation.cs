using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScenePrepOperation
{
    bool IsComplete { get; set; }

    void StartPrep();

    YieldInstruction? Update();

    void FinishPrep();
}
