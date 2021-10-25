using System.Collections;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public class EnemyWalk : State 
{
    private StateManager _stateManager;
    private NativeList<PathPart> resultPath;

    public override void EnterState(StateManager stateManager) {
        resultPath = new NativeList<PathPart>(Allocator.Persistent);
        _stateManager = stateManager;
        _stateManager.currentState.DoState();
    }
    
    public override void DoState() {
		// If no path exists, calculate one
        if (resultPath.Length <= 0)
            CalculatePath();
		

        _stateManager.currentState.LeaveState();

        StartCoroutine(Move());
    }

    public override void LeaveState() {
        resultPath.Dispose();
    }

    private void CalculatePath() {
		Vector3 enemyPos = gameObject.transform.position;
        int2 startPos = new int2((int) Mathf.Floor(enemyPos.x), ((int) Mathf.Floor(enemyPos.y)));
        Pathfinding pathfinding = new Pathfinding(startPos, new int2(254,254), resultPath);
	}

    IEnumerator Move() {
		yield return new WaitForSeconds(3.0f);
        // gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(resultPath[10].x, resultPath[10].y,0), 10f * Time.deltaTime);
        // _stateManager.currentState = _stateManager.availableStates[(int) EnemyAI.states.enemyWalk];
        _stateManager.currentState = _stateManager.GetComponent<EnemyWalk>();
        _stateManager.currentState.EnterState(_stateManager);
	}
}
