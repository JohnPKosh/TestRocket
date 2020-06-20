namespace ProxyLogic.Models
{
  /* Just putting this Property Proxy code sample here for later disection */


  public class Property<T> where T : new()
  {
    private T value;

    private readonly string name;

    public T Value
    {
      get => value;
      set
      {
        if (Equals(this.value, value)) return;
        this.value = value;
      }
    }

    public Property() : this(default(T)) { }
    public Property(T value, string name = "")
    {
      this.value = value;
      this.name = name;
    }

    public static implicit operator T(Property<T> property)
    {
      return property.Value; // int n = p_int;
    }

    public static implicit operator Property<T>(T value)
    {
      return new Property<T>(value); // Property<int> p = 123;
    }
  }

  public class Creature
  {
    private Property<int> agility = new Property<int>(10, nameof(agility));
    public int Agility
    {
      get => agility.Value;
      set => agility.Value = value;
    }
  }

}