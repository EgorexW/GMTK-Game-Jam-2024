public class CleaningAction : PaidAction
{
    protected override void Activate(Store store)
    {
        store.GetDirt().SetValue(0);
    }
}