using Game.Scripts.WeaponContext;
using Game.Scripts.WeaponContext.Type;

namespace Game.Scripts.Provider
{
    public class WeaponProvider
    {
        public WeaponType Type = WeaponType.Pistol; // тест потом убрать
        public Weapon Model { get; private set; }
        public WeaponView View { get; private set; }
    
        public void Set(Weapon model, WeaponView view)
        {
            Model = model;
            View = view;
        }
    }
}