using UnityEngine;

[CreateAssetMenu(
    fileName = "MinotaurStompStateData",
    menuName = "Data/State Data/Stomp State"
)]
public class MinotaurStompStateData : ScriptableObject
{
    public GameObject earthBump;
    public Tag earthBumpTag;
    public float damageAmount = 10f;
}
