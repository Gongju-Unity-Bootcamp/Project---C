public enum SoundID
{
    None = 5000
}
public class SoundData
{
    public SoundID Id {  get; set; }

    public string AcquireSound { get; set; }
    public string UseSound { get; set; }
    public float Volume { get; set; }
}
