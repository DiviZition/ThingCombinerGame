public interface IControllable
{
    public bool IsMoveing { get; }
    public bool IsSprinting { get; }

    public void OnEnter();

    public void Perform();

    public void OnExit();
}
