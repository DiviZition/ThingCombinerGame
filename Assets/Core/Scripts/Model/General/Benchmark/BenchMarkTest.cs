using UnityEngine;

public class BenchMarkTest : MonoBehaviour
{

    public Vector3 _moveDirection;
    public Vector2 _directionToParse;
    
    private DefaultInputs _inputs;

    public void PerformTestAction()//Empty action is 38 ms
    {
        if (_inputs == null)
        {
            _inputs = _inputs = new DefaultInputs();
            _inputs.GamePlay.MoveDirection.performed += PerformedSubscriptionTest;
        }
    }

    private void LegacyInputGetAxis()//335 ms
    {
        _moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void ReadValueTest()//58 ms
    {
        _moveDirection = _inputs.GamePlay.MoveDirection.ReadValue<Vector2>();
    }

    private void PerformedSubscriptionTest(UnityEngine.InputSystem.InputAction.CallbackContext obj)//38 ms
    {
        _moveDirection = obj.ReadValue<Vector2>();
    }
}
