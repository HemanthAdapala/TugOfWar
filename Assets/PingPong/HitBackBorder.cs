using UnityEngine;

public class HitBackBorder : MonoBehaviour
{
    [SerializeField] private int hitContact;
    public int HitContact
    {
        get { return hitContact; }
        set { hitContact = value; }
    }

}
