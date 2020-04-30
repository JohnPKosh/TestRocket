using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MissionControlDbTests
{
  [TestClass()]
  public class LaunchCenterTests : SqlDatabaseTestClass
  {

    public LaunchCenterTests()
    {
      InitializeComponent();
    }

    [TestInitialize()]
    public void TestInitialize()
    {
      base.InitializeTest();
    }
    [TestCleanup()]
    public void TestCleanup()
    {
      base.CleanupTest();
    }

    [TestMethod()]
    public void CanGetAllLaunchCenters()
    {
      SqlDatabaseTestActions testActions = this.CanGetAllLaunchCentersData;
      // Execute the pre-test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
      SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
      // Execute the test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
      SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
      // Execute the post-test script
      // 
      System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
      SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
    }

    #region Designer support code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction CanGetAllLaunchCenters_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LaunchCenterTests));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition launchCentersGetAllHasResults;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExecutionTimeCondition willTakeLessThan30Seconds;
            this.CanGetAllLaunchCentersData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            CanGetAllLaunchCenters_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            launchCentersGetAllHasResults = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.NotEmptyResultSetCondition();
            willTakeLessThan30Seconds = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ExecutionTimeCondition();
            // 
            // CanGetAllLaunchCenters_TestAction
            // 
            CanGetAllLaunchCenters_TestAction.Conditions.Add(launchCentersGetAllHasResults);
            CanGetAllLaunchCenters_TestAction.Conditions.Add(willTakeLessThan30Seconds);
            resources.ApplyResources(CanGetAllLaunchCenters_TestAction, "CanGetAllLaunchCenters_TestAction");
            // 
            // launchCentersGetAllHasResults
            // 
            launchCentersGetAllHasResults.Enabled = true;
            launchCentersGetAllHasResults.Name = "launchCentersGetAllHasResults";
            launchCentersGetAllHasResults.ResultSet = 1;
            // 
            // CanGetAllLaunchCentersData
            // 
            this.CanGetAllLaunchCentersData.PosttestAction = null;
            this.CanGetAllLaunchCentersData.PretestAction = null;
            this.CanGetAllLaunchCentersData.TestAction = CanGetAllLaunchCenters_TestAction;
            // 
            // willTakeLessThan30Seconds
            // 
            willTakeLessThan30Seconds.Enabled = true;
            willTakeLessThan30Seconds.ExecutionTime = System.TimeSpan.Parse("00:00:30");
            willTakeLessThan30Seconds.Name = "willTakeLessThan30Seconds";
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        private SqlDatabaseTestActions CanGetAllLaunchCentersData;
  }
}
