using UnityEngine;

public class NetworkGUID : MonoBehaviour
{
    [SerializeField]
    private string uniqueID;

    public string UniqueID
    {
        get { return uniqueID; }
        private set { uniqueID = value; }
    }

    public void SetGUID(string guid)
    {
        uniqueID = guid;
    }
}