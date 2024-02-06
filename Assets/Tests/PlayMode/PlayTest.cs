using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayTest
{

    [UnityTest]//Check PlayMode enviroment is set up correctly
    public IEnumerator CheckTest()
    {
        var gameObject = new GameObject();
        Assert.AreEqual(0, 0);
        yield return null;
    }
}
