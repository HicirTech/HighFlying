using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using InterSceneCommunication;

public class VariableContainerTest {

    [Test]
    public void DestroyYoungestObjectLeavesOnlyOneSurvivingContainer()
    {

        VariableContainer container0 = new VariableContainer();
        VariableContainer container1 = new VariableContainer();
        VariableContainer container2 = new VariableContainer();

        VariableContainer[] containerArray = new VariableContainer[3];

        containerArray[0] = container0;
        containerArray[1] = container1;
        containerArray[2] = container2;

        //Call Method Here 

        Assert.True(containerArray.Length == 1);

    }

    [Test]
    public void DestoryYoungestObjectDestrysYoungestObject()
    {
        VariableContainer container0 = new VariableContainer();
        VariableContainer container1 = new VariableContainer();
        VariableContainer container2 = new VariableContainer();

        VariableContainer[] containerArray = new VariableContainer[3];

        containerArray[0] = container0;
        containerArray[1] = container1;
        containerArray[2] = container2;

        Assert.True(container0);
    }

}
