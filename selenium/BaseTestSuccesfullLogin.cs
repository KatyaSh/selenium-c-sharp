using NUnit.Framework;
public class BaseTestSuccesfullLogin : BaseTest
{
    [SetUp]
    public void SetUp()
    {
        base.SetUp();
        SuccesfullLogin("standard_user", "secret_sauce");
    }
}

