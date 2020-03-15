namespace Scripts
{
    public class PowerPlant : Construction
    {
        public PowerPlant(string name, int height, int width)
        {
            Name = name;
            Height = height;
            Width = width;
        }
        public PowerPlant(string name)
        {
            Name = name;
            Height = 2;
            Width = 3;
        }
        public PowerPlant()
        {
            Name = "Power Plant";
            Height = 2;
            Width = 3;
        }
    }
}
