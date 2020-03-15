using UnityEngine;
namespace Scripts
{
    public interface IObject
    {
        string Name { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        Sprite Icon { get; set; }
        string DisplayInfo();
    }
}