namespace RaygunTestApp.Client.Services;

public class CounterService
{
    public int CurrentCount { get; private set; } = 0;

    public void IncrementCount()
    {
        this.CurrentCount++;

        if (this.CurrentCount >= 3) throw new Exception("これはテストです");
    }
}
