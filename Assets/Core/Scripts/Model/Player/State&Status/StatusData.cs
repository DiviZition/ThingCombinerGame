using R3;
using System.Collections.Generic;

public class StatusData
{
    private HashSet<StatusType> _statusList = new HashSet<StatusType>(8);

    public readonly Subject<StatusType> OnStatusAdded = new Subject<StatusType>();
    public readonly Subject<StatusType> OnStatusRemoved = new Subject<StatusType>();

    public bool IsStatusActive(StatusType status)
    {
        return _statusList.Contains(status);
    }

    public void AddStatus(StatusType status)
    {
        if (IsStatusActive(status) == false)
        {
            _statusList.Add(status);
            OnStatusAdded.OnNext(status);
        }
    }

    public void RemoveStatus(StatusType status)
    {
        if (IsStatusActive(status) == true)
        {
            _statusList.Remove(status);
            OnStatusRemoved.OnNext(status);
        }
    }

    public enum StatusType
    {
        Fighting,
        Starving,
        Swimming,
        Diving,
        Dialoguing,
    }
}
