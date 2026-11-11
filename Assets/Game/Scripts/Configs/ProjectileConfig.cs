using Game.Scripts.ProjectileContext;
using Game.Scripts.ProjectileContext.Type;
using UnityEngine;

namespace Game.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Source/Config/Projectile", fileName = "Projectile", order = 11)]
    public class ProjectileConfig : ScriptableObject
    {
        [field: SerializeField] public ProjectileType Type { get; private set; }
        [field: SerializeField] public Projectile View { get; private set; }
    }
}