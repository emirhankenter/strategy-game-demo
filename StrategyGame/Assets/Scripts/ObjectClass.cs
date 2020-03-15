using Scripts;
using UnityEngine;

public class ObjectClass : MonoBehaviour, IObject
{
    protected int _height;
    protected int _width;
    [SerializeField] private Sprite _icon;

    public string Name { get; set; }

    public int Height
    {
        get => _height;
        set => _height = value > 10 ? 1 : value;
    }
    public int Width
    {
        get => _width;
        set => _width = value > 10 ? 1 : value;
    }

    public Sprite Icon
    {
        get => _icon;
        set => _icon = value;
    }

    public virtual string DisplayInfo()
    {
        string infoText = "It is just a object";
        return infoText;
    }
}
