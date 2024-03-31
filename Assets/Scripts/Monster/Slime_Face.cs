using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Face", order = 1)]
public class Slime_Face : ScriptableObject
{
    [SerializeField]
    public Texture IdleFace_1, IdleFace_2, IdleFace_3, DeathFace;
}
