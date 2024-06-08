
public abstract class PlayerBaseState
{
    public abstract void OnEnterState();
    public abstract void OnUpdateState();
    public abstract void OnExitState();

    void SwitchState(){}
}
