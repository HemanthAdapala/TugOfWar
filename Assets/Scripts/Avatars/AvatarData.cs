using UnityEngine;

[CreateAssetMenu(fileName = "New Avatar", menuName = "Avatar")]
public class AvatarData : ScriptableObject
{
    public string avatarName;
    public GameObject avatarPrefab;

    public void OnValidate()
    {
        if(string.IsNullOrEmpty(avatarName) || avatarName != this.name){
            avatarName = this.name;
        }
    }

}
